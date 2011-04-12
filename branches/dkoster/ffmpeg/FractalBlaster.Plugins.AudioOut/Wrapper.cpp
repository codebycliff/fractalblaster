#include "Wrapper.h"
#include "WaveInterface.h"
#include <iostream>

namespace FractalBlaster
{
	namespace Plugins
	{
		namespace AudioOut
		{
			Wrapper::Wrapper()
			{
				NativeData = gcnew System::Collections::Generic::List< System::Collections::Generic::KeyValuePair<System::Runtime::InteropServices::GCHandle, System::IntPtr> >();
				BuffSizeDel = gcnew FractalBlaster::Universe::BufferSizeHandler(this, &Wrapper::GetBufferSize);
				BuffPtrDel = gcnew FractalBlaster::Universe::BufferHandler(this, &Wrapper::GetBufferPointer);
				volume = 100;
			}

			#pragma region [ IOutputPlugin ]
			
			System::Boolean Wrapper::IsPlaying::get()
			{
				return playing;
			}
			
			void Wrapper::IsPlaying::set(System::Boolean isPlaying)
			{
				this->playing = isPlaying;
			}
			
			System::Boolean Wrapper::IsPaused::get()
			{
				return paused;
			}
			
			void Wrapper::IsPaused::set(System::Boolean IsPaused)
			{
				this->paused = IsPaused;
			}
			
			System::Int32 Wrapper::Volume::get()
			{
				return volume;
			}
			
			void Wrapper::Volume::set(System::Int32 vol)
			{
				this->volume = vol;
				WaveInterface::instance->SetVolume(vol);
			}

			void Wrapper::Play()
			{
				currentStream = gcnew OutputStream(BuffPtrDel, BuffSizeDel, 2);
				WaveInterface::instance->ChangeStream(currentStream);
				IsPlaying = true;
			}

			void Wrapper::Stop()
			{
				WaveInterface::instance->Stop();
				IsPlaying = false;
				for(int i=0; i<NativeData->Count;i++)
				{
					NativeData[0].Key.Free();
					System::Runtime::InteropServices::Marshal::FreeHGlobal(NativeData[0].Value);
					NativeData->RemoveAt(0);
				}
			}

			void Wrapper::Pause()
			{
				WaveInterface::instance->Pause();
				IsPaused = true;
				IsPlaying = false;
			}

			void Wrapper::Resume()
			{
				WaveInterface::instance->UnPause();
				IsPaused = false;
				IsPlaying = true;
			}

			#pragma endregion

			#pragma region [ IPlugin ]

			void Wrapper::Initialize(FractalBlaster::Universe::AppContext^ appContext)
			{
				Wrapper::appContext = appContext;
			}

			#pragma endregion

			System::IntPtr Wrapper::GetBufferPointer(bool cleanup)
			{
				if(cleanup)
				{
					NativeData[0].Key.Free();
					System::Runtime::InteropServices::Marshal::FreeHGlobal(NativeData[0].Value);
					NativeData->RemoveAt(0);
				}

				PCM = appContext->Engine->InputPlugin->ReadFrames(FractalBlaster::Universe::GlobalVariables::m_BufferSize);				

				if(PCM == nullptr)
				{
					return System::IntPtr::Zero;
				}

				System::Runtime::InteropServices::GCHandle dataHandle = System::Runtime::InteropServices::GCHandle::Alloc(PCM->ToArray(), System::Runtime::InteropServices::GCHandleType::Pinned);
				System::IntPtr ptr = System::Runtime::InteropServices::Marshal::AllocHGlobal((int)PCM->Length);
				System::Runtime::InteropServices::Marshal::Copy(PCM->ToArray(), 0, ptr, (int)PCM->Length);
				NativeData->Add(System::Collections::Generic::KeyValuePair<System::Runtime::InteropServices::GCHandle, System::IntPtr>(dataHandle, ptr));

				return ptr;
			}

			System::Int32 Wrapper::GetBufferSize()
			{
				if(PCM == nullptr)
				{
					return (System::Int32)0;
				}
				else
				{
					return (System::Int32)PCM->Length;
				}
			}
		}
	}
}