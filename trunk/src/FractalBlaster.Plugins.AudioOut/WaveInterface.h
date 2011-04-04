#ifndef WAVEINTERFACE_H_
#define WAVEINTERFACE_H_

#include "Windows.h"
#include "MMSystem.h"
#include "OutputStream.h"
#include <iostream>

enum PlaybackState {Playing, Paused, Stopped};

/// <summary>
/// State machine for controlling operations with the Windows Waveform 
/// </summary>
ref class WaveInterface
{
	
	public:
		/// <summary>
		/// Singleton of the waveform function manager
		/// </summary>
		/// <returns> WaveInterface Singleton </returns>
		static initonly WaveInterface^ instance = gcnew WaveInterface();

		/// <summary>
		/// Changes the audio file stream associated with the output
		/// </summary>
		/// <param name="s">OutputStream representing the new stream to be output</param>
		void ChangeStream(OutputStream^ s);

		/// <summary>
		/// Indicates completion of an audio block by the output device
		/// </summary>
		void BlockCompleted(void);

		/// <summary>
		/// Pauses the audio output
		/// </summary>
		void Pause(void);

		/// <summary>
		/// Returns the audio device to playing if paused
		/// </summary>
		void UnPause(void);

		/// <summary>
		/// Stops the audio device, returns all audio blocks in the queue
		/// </summary>
		void Stop(void);

		/// <summary>
		/// Sets the output device volume
		/// </summary>
		/// <param name="vol">int - New Volume Limit</param>
		void SetVolume(int vol);


	private:
		/// <summary>
		/// Private Constructor binding itself to the default Windows Output device
		/// </summary>
		WaveInterface(void);

		/// <summary>
		/// Establish connection to the audio device
		/// </summary>
		/// <returns> Boolean representing the success of the connection </returns>
		bool ConnectAudioEndpoints(void);

		/// <summary>
		/// Adds the next audio block to the queue for playback
		/// </summary>
		void PushNextBlock(void);
		 
		LPHWAVEOUT StereoOutputDevice;
		LPHWAVEOUT MonoOutputDevice;

		OutputStream^ currentStream;

		WAVEHDR* Playing;
		WAVEHDR* Queued;

		PlaybackState state;

};

#endif