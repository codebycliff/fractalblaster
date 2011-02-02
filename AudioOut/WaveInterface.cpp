#include "WaveInterface.h"
#include "process.h"

void CALLBACK waveOutProc(HWAVEOUT hwo, UINT uMsg, DWORD dwInstance, DWORD dwParam1, DWORD dwParam2);
unsigned int CallbackBlockCompleted();

WaveInterface::WaveInterface()
{
	ConnectAudioEndpoints();
	state = PlaybackState::Stopped;
}

WaveInterface* WaveInterface::WaveInterfaceInstance()
{
	static WaveInterface* instance = new WaveInterface();
	return instance;
}

bool WaveInterface::ConnectAudioEndpoints()
{
	bool rval = false;

	WAVEFORMATEX* wf2 = new WAVEFORMATEX();
	WAVEFORMATEX* wf1 = new WAVEFORMATEX();

	Playing = new WAVEHDR();
	Queued = new WAVEHDR();

	currentStream = NULL;

	wf2->wFormatTag = WAVE_FORMAT_PCM;
	wf2->nChannels = 2;
	wf2->nSamplesPerSec = 44100;
	wf2->wBitsPerSample = 16;
	wf2->nBlockAlign = wf2->nChannels * wf2->wBitsPerSample/8;
	wf2->nAvgBytesPerSec =  wf2->nBlockAlign * wf2->nSamplesPerSec;
	wf2->cbSize = 0;

	wf1->wFormatTag = WAVE_FORMAT_PCM;
	wf1->nChannels = 2; // Subject to change?
	wf1->nSamplesPerSec = 44100;
	wf1->wBitsPerSample = 16;
	wf1->nBlockAlign = wf1->nChannels * wf1->wBitsPerSample/8;
	wf1->nAvgBytesPerSec =  wf1->nBlockAlign * wf1->nSamplesPerSec;
	wf1->cbSize = 0;

	StereoOutputDevice = new HWAVEOUT();
	MonoOutputDevice = new HWAVEOUT();

	MMRESULT result2 = waveOutOpen(StereoOutputDevice, WAVE_MAPPER, wf2, (DWORD)&waveOutProc, (DWORD)this, CALLBACK_FUNCTION);
	MMRESULT result1 = waveOutOpen(MonoOutputDevice, WAVE_MAPPER, wf1, (DWORD)&waveOutProc, (DWORD)this, CALLBACK_FUNCTION);

	if(!result2 && !result1)
	{
		rval = true;
	}

	return rval;
}

void WaveInterface::ChangeStream(OutputStream* s)
{
	if(currentStream != NULL)
	{
		delete currentStream;
	}
	currentStream = s;

	delete Playing;
	delete Queued;
	Playing = NULL;
	Queued = NULL;

	while(!currentStream->isDeviceBuffered())
	{
		PushNextBlock();
	}

	state = PlaybackState::Playing;
}

void WaveInterface::BlockCompleted()
{
	while(waveOutUnprepareHeader(*StereoOutputDevice, Playing, sizeof(WAVEHDR)) == WAVERR_STILLPLAYING)
	{
	}
	delete Playing;
	Playing = NULL;
	PushNextBlock();
}

void WaveInterface::PushNextBlock()
{
	char* BlockData;
	if(currentStream->getBufferData() == NULL && currentStream->isDeviceBuffered())
	{
		// Out of audio, fill with silence
		//BlockData = OutputStream::DeadStream();

		// Be done!
		return;
	}
	else
	{
		BlockData = currentStream->LoadNextFrameSet();
	}

	WAVEHDR* header;

	if(Playing == NULL && Queued == NULL)
	{
		Playing = new WAVEHDR();
		header = Playing;
	}
	else if(Playing != NULL && Queued == NULL)
	{
		Queued = new WAVEHDR();
		header = Queued;
	}
	else if(Playing == NULL && Queued != NULL)
	{
		Playing = Queued;
		Queued = new WAVEHDR();
		header = Queued;
	}

	ZeroMemory(header, sizeof(WAVEHDR));
	header->dwBufferLength = currentStream->getBufferSize();
	header->lpData = currentStream->getBufferData();

	MMRESULT result = waveOutPrepareHeader(*StereoOutputDevice, header, sizeof(WAVEHDR));
	result = waveOutWrite(*StereoOutputDevice, header, sizeof(WAVEHDR));
}

unsigned int CallbackBlockCompleted()
{
	WaveInterface::WaveInterfaceInstance()->BlockCompleted();
	return 0;
}

void CALLBACK waveOutProc(HWAVEOUT hwo, UINT uMsg, DWORD dwInstance, DWORD dwParam1, DWORD dwParam2)
{
	// Do callback shit
	if (uMsg == MM_WOM_DONE)
	{
		// Going to thread this...

		unsigned int threadID;
		void* (__stdcall *callback)();  
		_beginthreadex(NULL, 0, (unsigned int (__stdcall*)(void*))CallbackBlockCompleted, NULL, 0, &threadID);

		 //WaveInterface::WaveInterfaceInstance()->BlockCompleted(); <- Causes Lag
		std::cout << "Data Block Completed" << std::endl;
	}
	else if(uMsg == MM_WOM_OPEN)
	{
		std::cout << "Output Device Opened" << std::endl;
	}
}

void WaveInterface::Pause()
{
	if(state !=  PlaybackState::Playing)
	{
		return;
	}

	waveOutPause(*StereoOutputDevice);
	state = PlaybackState::Paused;
	
}

void WaveInterface::UnPause()
{
	if(state != PlaybackState::Paused)
	{
		return;
	}

	waveOutRestart(*StereoOutputDevice);
	state = PlaybackState::Playing;
	
}

void WaveInterface::Stop()
{
	waveOutReset(*StereoOutputDevice);	
	state = PlaybackState::Stopped;
}