#include "Callbacks.h"
#include "WaveInterface.h"
#include "process.h"

unsigned int CallbackBlockCompleted()
{
	WaveInterface::instance->BlockCompleted();
	return 0;
}

void CALLBACK waveOutProc(HWAVEOUT hwo, UINT uMsg, DWORD dwInstance, DWORD dwParam1, DWORD dwParam2)
{
	// Do callback stuff
	if (uMsg == MM_WOM_DONE)
	{
		unsigned int threadID;
		_beginthreadex(NULL, 0, (unsigned int (__stdcall*)(void*))CallbackBlockCompleted, NULL, 0, &threadID);
	}
	else if(uMsg == MM_WOM_OPEN)
	{
		std::cout << "Output Device Opened" << std::endl;
	}
}