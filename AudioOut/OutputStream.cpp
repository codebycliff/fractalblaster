#include "OutputStream.h"

OutputStream::OutputStream(LOAD_BUFFER_FUNCTION loadfnc, LOAD_BUFFER_SIZE loadsize, int channels)
{
	this->loadfnc = loadfnc;
	this->loadsize = loadsize;
	this->channels = channels;
	this->BlocksLoaded = 0;
	this->BufferedFrameSet = NULL;
	this->BufferedFrameSize = 0;
}
char* OutputStream::LoadNextFrameSet()
{
	BufferedFrameSet = loadfnc(isDeviceBuffered());
	BufferedFrameSize = loadsize();
	BlocksLoaded++;
	return BufferedFrameSet;
}
char* OutputStream::DeadStream()
{
	// This shouldn't leak any memory...
	static char* DeadBuffer = new char[44100*2*2*60];
	ZeroMemory(DeadBuffer, 44100*2*2*60);
	return DeadBuffer;
}

OutputStream::~OutputStream()
{
	// Call into managed code to release any extra frames
	LoadNextFrameSet();
	LoadNextFrameSet();
}

bool OutputStream::isDeviceBuffered()
{
	if(BlocksLoaded < 2)
	{
		return false;
	}
	else
	{
		return true;
	}
}

char* OutputStream::getBufferData()
{
	return BufferedFrameSet;
}

int OutputStream::getBufferSize()
{
	return BufferedFrameSize;
}

int OutputStream::getNumberOfChannels()
{
	return channels;
}

bool OutputStream::isStreamGood()
{
	return (BufferedFrameSet == NULL);
}