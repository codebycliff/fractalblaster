#include "OutputStream.h"

OutputStream::OutputStream(FractalBlaster::Universe::BufferHandler^ loadfnc,FractalBlaster::Universe::BufferSizeHandler^ loadsize, int channels)
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
	BufferedFrameSet = (char*)loadfnc->Invoke(isDeviceBuffered()).ToPointer();
	BufferedFrameSize = loadsize->Invoke();
	BlocksLoaded++;
	return BufferedFrameSet;
}

OutputStream::~OutputStream()
{
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