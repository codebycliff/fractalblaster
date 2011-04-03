#ifndef CALLBACKS_H_
#define CALLBACKS_H_

#include "Windows.h"

void CALLBACK waveOutProc(HWAVEOUT hwo, UINT uMsg, DWORD dwInstance, DWORD dwParam1, DWORD dwParam2);
unsigned int CallbackBlockCompleted();

#endif