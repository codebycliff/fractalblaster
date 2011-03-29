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
	// Do callback shit
	if (uMsg == MM_WOM_DONE)
	{
		// Going to thread this...

		unsigned int threadID;
		_beginthreadex(NULL, 0, (unsigned int (__stdcall*)(void*))CallbackBlockCompleted, NULL, 0, &threadID);

		 //WaveInterface::WaveInterfaceInstance()->BlockCompleted(); <- Causes Lag
		std::cout << "Data Block Completed" << std::endl;
	}
	else if(uMsg == MM_WOM_OPEN)
	{
		std::cout << "Output Device Opened" << std::endl;
	}
}