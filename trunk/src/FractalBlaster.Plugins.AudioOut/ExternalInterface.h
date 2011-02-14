#ifndef EXTERNALINTERFACE_H_
#define EXTERNALINTERFACE_H_

#define DLLExport   __declspec(dllexport)

typedef char* (__stdcall *LOAD_BUFFER_FUNCTION)(int);  
typedef int (__stdcall *LOAD_BUFFER_SIZE)();  

class WaveInterface;
class OutputStream;

extern "C" DLLExport WaveInterface* WaveInterfaceInstance(void);
extern "C" DLLExport OutputStream* CreateOutputStream(LOAD_BUFFER_FUNCTION, LOAD_BUFFER_SIZE, int);
extern "C" DLLExport void ChangeOutputStream(OutputStream*);

extern "C" DLLExport void Pause();
extern "C" DLLExport void UnPause();
extern "C" DLLExport void Stop();

#endif