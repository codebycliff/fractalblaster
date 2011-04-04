#ifndef OUTPUTSTREAM_H_
#define OUTPUTSTREAM_H_

using namespace FractalBlaster::Universe;

#include "Windows.h"

/// <summary>
/// Controls an output stream which is bound to an audio stream
/// </summary>
ref class OutputStream
{
	public:
		/// <summary>
		/// Creates an instance bound to the callback handlers for an audio stream
		/// </summary>
		/// <param name="loadfnc">Callback Handler to load audio buffer</param>
		/// <param name="loadfnc">Callback Handler to load audio buffer size</param>
		/// <param name="loadfnc">Number of channels present in audio stream</param>
		OutputStream(FractalBlaster::Universe::BufferHandler^ loadfnc, FractalBlaster::Universe::BufferSizeHandler^ loadsize, int channels);
		
		/// <summary>
		/// Destroys all memory allocated at the class level
		/// </summary>
		~OutputStream();

		/// <summary>
		/// Uses callbacks to retrieve PCM data for audio device
		/// </summary>
		/// <returns> Unmanaged byte stream with PCM data </returns>
		char* LoadNextFrameSet(void);

		/// <summary>
		/// Ensures the PCM data stream isn't empty
		/// </summary>
		/// <returns> boolean representing wether the PCM stream is null </returns>
		bool isStreamGood(void);

		/// <summary>
		/// Determines if 2 blocks are currently in the audio device buffer
		/// </summary>
		/// <returns> boolean representing if 2 blocks are in the audio queue </returns>
		bool isDeviceBuffered(void);

		/// <summary>
		/// Gets the PCM stream
		/// </summary>
		/// <returns> char* to PCM data </returns>
		char* getBufferData(void);

		/// <summary>
		/// Gets the PCM stream size
		/// </summary>
		/// <returns> int of PCM stream size </returns>
		int getBufferSize(void);

		/// <summary>
		/// Returns number of audio channels
		/// </summary>
		/// <returns> int showing number of audio channels currently in use </returns>
		int getNumberOfChannels(void);

	private:
		int channels;
		int BufferedFrameSize;
		int BlocksLoaded;
		char* BufferedFrameSet;

		FractalBlaster::Universe::BufferHandler^ loadfnc;
		FractalBlaster::Universe::BufferSizeHandler^ loadsize;
		
};

#endif