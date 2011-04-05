#ifndef OUTPUTSTREAM_H_
#define OUTPUTSTREAM_H_

using namespace FractalBlaster::Universe;

#include "Windows.h"

//typedef char* (__stdcall *LOAD_BUFFER_FUNCTION)(bool);  
//typedef int (__stdcall *LOAD_BUFFER_SIZE)();  

ref class OutputStream
{
	public:
		OutputStream(FractalBlaster::Universe::BufferHandler^, FractalBlaster::Universe::BufferSizeHandler^, int);
		~OutputStream();

		char* LoadNextFrameSet(void);

		bool isStreamGood(void);
		bool isDeviceBuffered(void);
		char* getBufferData(void);
		int getBufferSize(void);
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