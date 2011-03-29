#include "OutputStream.h"
#include "WaveInterface.h"

namespace FractalBlaster
{
	namespace Plugins
	{
		namespace AudioOut
		{
			[FractalBlaster::Universe::PluginAttribute(Name="AudioOut", Description="C++ Audio Output Plugin")]
			public ref class Wrapper : public FractalBlaster::Universe::IOutput
			{
				public:

					Wrapper();

					#pragma region [ IOutputPlugin ]

					virtual property System::Boolean IsPlaying { System::Boolean get(); private: void set(System::Boolean);}
					virtual property System::Boolean IsPaused { System::Boolean get(); private: void set(System::Boolean); }
					virtual property System::Int32 Volume { System::Int32 get(); void set(System::Int32); }
					
					virtual void Play(void);
					virtual void Stop(void);
					virtual void Pause(void);
					virtual void Resume(void);

					virtual property FractalBlaster::Universe::IInput^ input { void set(FractalBlaster::Universe::IInput^); }

					#pragma endregion



				private:

					System::IntPtr GetBufferPointer(bool);
					System::Int32 GetBufferSize();

					System::IO::MemoryStream^ PCM;
					System::Collections::Generic::List< System::Collections::Generic::KeyValuePair<System::Runtime::InteropServices::GCHandle, System::IntPtr> >^ NativeData;

					FractalBlaster::Universe::BufferSizeHandler^ BuffSizeDel;
					FractalBlaster::Universe::BufferHandler^ BuffPtrDel;

					FractalBlaster::Universe::IInput^ inputSource;
					
					OutputStream^ currentStream;

					System::Boolean playing;
					System::Boolean paused;

					System::Int32 volume;
			};
		}
	}
}