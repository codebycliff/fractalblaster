#ifndef OUTPUTSTREAM_H_
#define OUTPUTSTREAM_H_

#include "Windows.h"

typedef char* (__stdcall *LOAD_BUFFER_FUNCTION)(bool);  
typedef int (__stdcall *LOAD_BUFFER_SIZE)();  

class OutputStream
{
	public:
		OutputStream(LOAD_BUFFER_FUNCTION, LOAD_BUFFER_SIZE, int);
		~OutputStream();

		char* LoadNextFrameSet(void);
		static char* DeadStream(void); // Needed?

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

		LOAD_BUFFER_FUNCTION loadfnc;
		LOAD_BUFFER_SIZE loadsize;
		
};

#endif