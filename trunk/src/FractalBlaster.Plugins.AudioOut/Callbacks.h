#ifndef CALLBACKS_H_
#define CALLBACKS_H_

#include "Windows.h"

/// <summary>
/// Windows API Call Overload for returned audio buffer block
/// </summary>
/// <param name="hwo">HWAVEOUT - Ignored Parameter</param>
/// <param name="uMsg">MM_RESULT stating the action performed</param>
/// <param name="dwInstance">DWORD - Ignored Parameter</param>
/// <param name="dwParam1">DWORD - Ignored Parameter</param>
/// <param name="dwParam2">DWORD - Ignored Parameter</param>
/// <returns> Nothing </returns>
void CALLBACK waveOutProc(HWAVEOUT hwo, UINT uMsg, DWORD dwInstance, DWORD dwParam1, DWORD dwParam2);

/// <summary>
/// Notifies the singleton controller that a block has completed
/// </summary>
/// <returns> 0 as required by WinAPI specification </returns>
unsigned int CallbackBlockCompleted();

#endif