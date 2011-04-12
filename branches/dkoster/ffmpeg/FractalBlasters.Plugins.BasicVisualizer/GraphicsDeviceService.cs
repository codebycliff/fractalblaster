using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FractalBlaster.Plugins.BasicVisualizer
{
    class GraphicsDeviceService : IGraphicsDeviceService
    {
        PresentationParameters _parameters;
        GraphicsDevice _graphicsDevice;

        static int _referenceCount;

        static GraphicsDeviceService _singletonInstance;

        GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            _parameters = new PresentationParameters();

            _parameters.BackBufferWidth = Math.Max(width, 1);
            _parameters.BackBufferHeight = Math.Max(height, 1);
            _parameters.BackBufferFormat = SurfaceFormat.Color;
            _parameters.DepthStencilFormat = DepthFormat.Depth24;
            _parameters.DeviceWindowHandle = windowHandle;
            _parameters.PresentationInterval = PresentInterval.Immediate;
            _parameters.IsFullScreen = false;

            _graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter,
                                                GraphicsProfile.HiDef,
                                                _parameters);
        }

        /// <summary>
        /// Gets a reference to the singleton instance.
        /// </summary>
        public static GraphicsDeviceService AddRef(IntPtr windowHandle,
                                                   int width, int height)
        {
            // Increment the "how many controls sharing the device" reference count.
            if (Interlocked.Increment(ref _referenceCount) == 1)
            {
                // If this is the first control to start using the
                // device, we must create the singleton instance.
                _singletonInstance = new GraphicsDeviceService(windowHandle,
                                                              width, height);
            }

            return _singletonInstance;
        }

        /// <summary>
        /// Releases a reference to the singleton instance.
        /// </summary>
        public void Release(bool disposing)
        {
            // Decrement the "how many controls sharing the device" reference count.
            if (Interlocked.Decrement(ref _referenceCount) == 0)
            {
                // If this is the last control to finish using the
                // device, we should dispose the singleton instance.
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    _graphicsDevice.Dispose();
                }

                _graphicsDevice = null;
            }
        }


        /// <summary>
        /// Resets the graphics device to whichever is bigger out of the specified
        /// resolution or its current size. This behavior means the device will
        /// demand-grow to the largest of all its GraphicsDeviceControl clients.
        /// </summary>
        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            _parameters.BackBufferWidth = Math.Max(_parameters.BackBufferWidth, width);
            _parameters.BackBufferHeight = Math.Max(_parameters.BackBufferHeight, height);

            _graphicsDevice.Reset(_parameters);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        public GraphicsDevice GraphicsDevice
        {
            get { return _graphicsDevice; }
        }
    }
}
