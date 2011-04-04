#include "OutputStream.h"
#include "WaveInterface.h"

namespace FractalBlaster
{
	namespace Plugins
	{
		namespace AudioOut
		{
			[FractalBlaster::Universe::PluginAttribute(Name="AudioOut", Description="C++ Audio Output Plugin")]
			
			/// <summary>
			/// Wrapper class to conform to plugin interface specifications
			/// </summary>
			public ref class Wrapper : public FractalBlaster::Universe::IOutputPlugin
			{
				public:

					/// <summary>
					/// Constructs a new isntance of this wrapper
					/// </summary>
					Wrapper();

					#pragma region [ IOutputPlugin ]

					/// <summary>
					/// Property indicating the playback status (wether the audio stream is playing)
					/// </summary>
					virtual property System::Boolean IsPlaying { System::Boolean get(); private: void set(System::Boolean);}
					
					/// <summary>
					/// Property indicating the playback status (wether the audio stream is paused)
					/// </summary>
					virtual property System::Boolean IsPaused { System::Boolean get(); private: void set(System::Boolean); }
					
					/// <summary>
					/// Property indicating the current volume
					/// </summary>
					virtual property System::Int32 Volume { System::Int32 get(); void set(System::Int32); }
					
					/// <summary>
					/// Starts playback
					/// </summary>
					virtual void Play(void);

					/// <summary>
					/// Stops playback
					/// </summary>
					virtual void Stop(void);

					/// <summary>
					/// Pauses playback
					/// </summary>
					virtual void Pause(void);

					/// <summary>
					/// Resumes playback
					/// </summary>
					virtual void Resume(void);

					#pragma endregion

					#pragma region [ IPlugin]
					
					/// <summary>
					/// Plugin Interface Requirement, initializes the plugin setup
					/// </summary>
					/// <param name="appContext">AppContext allowing the plugin to see other loaded DLLS</param>
					virtual void Initialize(FractalBlaster::Universe::AppContext^ appContext);

					#pragma endregion

					static FractalBlaster::Universe::AppContext^ appContext;

				private:

					/// <summary>
					/// Function to get a pointer to the PCM buffer 
					/// </summary>
					/// <param name="cleanup">bool indicates wether garbage collection is to be performed</param>
					/// <returns> IntPtr indicating the location of the PCM data </returns>
					System::IntPtr GetBufferPointer(bool cleanup);

					/// <summary>
					/// Function to get the PCM buffer size
					/// </summary>
					/// <returns> Int32 indicating size of the PCM buffer </returns>
					System::Int32 GetBufferSize();

					System::IO::MemoryStream^ PCM;
					System::Collections::Generic::List< System::Collections::Generic::KeyValuePair<System::Runtime::InteropServices::GCHandle, System::IntPtr> >^ NativeData;

					FractalBlaster::Universe::BufferSizeHandler^ BuffSizeDel;
					FractalBlaster::Universe::BufferHandler^ BuffPtrDel;

					OutputStream^ currentStream;

					System::Boolean playing;
					System::Boolean paused;

					System::Int32 volume;
			};
		}
	}
}