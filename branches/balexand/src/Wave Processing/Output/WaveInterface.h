#ifndef WAVEINTERFACE_H_
#define WAVEINTERFACE_H_

#include "Windows.h"
#include "MMSystem.h"
#include "OutputStream.h"
#include <iostream>

enum PlaybackState {Playing, Paused, Stopped};

ref class WaveInterface
{
	
	public:
		static initonly WaveInterface^ instance = gcnew WaveInterface();
		void ChangeStream(OutputStream^ s);
		void BlockCompleted(void);

		void Pause(void);
		void UnPause(void);
		void Stop(void);

		void SetVolume(int);


	private:
		WaveInterface(void);
		bool ConnectAudioEndpoints(void);
		void PushNextBlock(void);
		 
		LPHWAVEOUT StereoOutputDevice;
		LPHWAVEOUT MonoOutputDevice;

		OutputStream^ currentStream;

		WAVEHDR* Playing;
		WAVEHDR* Queued;

		PlaybackState state;

};

#endif