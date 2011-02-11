#include "ExternalInterface.h"
#include "WaveInterface.h"
#include "OutputStream.h"

#include <iostream>

WaveInterface* WaveInterfaceInstance()
{
	return WaveInterface::WaveInterfaceInstance();
}
OutputStream* CreateOutputStream(LOAD_BUFFER_FUNCTION lbf, LOAD_BUFFER_SIZE lbs, int channels)
{
	return new OutputStream(lbf, lbs, channels);
}
void ChangeOutputStream(OutputStream* newStream)
{
	WaveInterface::WaveInterfaceInstance()->ChangeStream(newStream);
}

void Pause()
{
	WaveInterface::WaveInterfaceInstance()->Pause();
}

void UnPause()
{
	WaveInterface::WaveInterfaceInstance()->UnPause();
}

void Stop()
{
	WaveInterface::WaveInterfaceInstance()->Stop();
}