using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using FractalBlaster.Universe;

namespace FractalBlaster.Plugins.Decoder.FFMPEG {

    public enum SeekOrigin { 
        Begin = 0, 
        Current = 1, 
        End = 2 
    };

    [PluginAttribute(Name="Audio DecoderInterop", Description="Decodes audio streams")]
    public class DecoderInterop : IInputPlugin {

        public AppContext Context { get; private set; }

        public Int32 SampleSize { get; private set; }

        public Int32 CurrentFrameNumber { get; private set; }

        public String FilePath { get; private set; }

        /// <summary>
        /// Initializes 3rd party libraries that contain copyrighted and patent protected codecs
        /// </summary>
        static DecoderInterop() {
            // FractalBlaster.Plugins.Decoder.FFMPEG Register All Encoders/Decoders
            FFMPEG.av_register_all();
        }
        
        /// <summary>
        /// Creates an instance of a decoder. Initializes all values need for interoperation with 3rd party library and all information used in metrics
        /// </summary>
        public DecoderInterop() {
            audioCodecContext = new FFMPEG.AVCodecContext();
            formatContext = new FFMPEG.AVFormatContext();

            pAudioStream = IntPtr.Zero;
            pAudioCodec = IntPtr.Zero;
            pAudioCodecContext = IntPtr.Zero;
            pFormatContext = IntPtr.Zero;

            audioStartIndex = -1;

            CurrentFrameNumber = -1;
            SampleSize = -1;
        }

        /// <summary>
        /// Plugin Interface Initialization
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(AppContext context) {
            Context = context;
        }

        /// <summary>
        /// Frees all static information in 3rd party libraries
        /// </summary>
        ~DecoderInterop() {
            //FractalBlaster.Plugins.Decoder.FFMPEG.av_free_static();
        }

        /// <summary>
        /// Returns a byte array containing the PCM data for the next audio frame.
        /// Retrieves an audio packet from FFMPEG
        /// Extracts audio frame byte data (4068 bytes)
        /// Returns byte array representing PCM data for this frame.
        /// </summary>
        /// <returns></returns>
        public Byte[] RetrieveNextFrame() {
            byte[] rval;
            IntPtr pPacket = IntPtr.Zero;
            try
            {
                pPacket = Marshal.AllocHGlobal(56);
                if (FFMPEG.av_read_frame(pFormatContext, pPacket) < 0)
                {
                    return null;
                }
            }
            catch (AccessViolationException) { return RetrieveNextFrame(); }

            IntPtr pSamples = IntPtr.Zero;
            FFMPEG.AVPacket packet = (FFMPEG.AVPacket)Marshal.PtrToStructure(pPacket, typeof(FFMPEG.AVPacket));

            Marshal.FreeHGlobal(pPacket);

            if (packet.stream_index != audioStartIndex) {
                return null;
            }

            try {
                int AUDIO_FRAME_SIZE = 5000;

                int frameSize = 0;

                try
                {
                    pSamples = Marshal.AllocHGlobal(AUDIO_FRAME_SIZE);
                    int size = FFMPEG.avcodec_decode_audio(pAudioCodecContext, pSamples, out frameSize, packet.data, packet.size);
                }
                catch (Exception e) {/* return RetrieveNextFrame();*/  }

                CurrentFrameNumber++;

                rval = new byte[frameSize];
                Marshal.Copy(pSamples, rval, 0, frameSize);

                return rval;

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
            finally {
                Marshal.FreeHGlobal(pSamples);
            }
        }
        
        #region [ IInputPlugin ]

        /// <summary>
        /// Seeks to the given value within the file within seconds.
        /// The parameter is intended to be within the scope of the file length
        /// </summary>
        /// <param name="seconds"></param>
        public void Seek(int seconds)
        {
            Int64 AVSeekAmt = seconds * FFMPEG.AV_TIME_BASE;
            int flags = FFMPEG.AVSEEK_FLAG_ANY;

            if (FFMPEG.av_seek_frame(pFormatContext, -1, AVSeekAmt, flags) < 0)
            {
                // Error
            }

            FFMPEG.avcodec_flush_buffers(pAudioCodecContext);
        }

        /// <summary>
        /// Opens the file specified and determine the correct codec to use for extracting PCM data
        /// Also determines other information such as sample size and codec type
        /// </summary>
        /// <param name="media"></param>
        public void OpenMedia(MediaFile media) {

            // Open Input Audio File
            if (FFMPEG.av_open_input_file(out pFormatContext, media.Info.FullName, IntPtr.Zero, 0, IntPtr.Zero) < 0) {
                throw new FileLoadException("");
            }

            // Determine Audio Codec
            if (FFMPEG.av_find_stream_info(pFormatContext) < 0) {
                throw new FileLoadException("Unable to read audio stream");
            }

            formatContext = (FFMPEG.AVFormatContext)Marshal.PtrToStructure(pFormatContext, typeof(FFMPEG.AVFormatContext));

            for (int i = 0; i < formatContext.nb_streams; ++i) {
                FFMPEG.AVStream stream = (FFMPEG.AVStream)Marshal.PtrToStructure(formatContext.streams[i], typeof(FFMPEG.AVStream));

                FFMPEG.AVCodecContext codec = (FFMPEG.AVCodecContext)Marshal.PtrToStructure(stream.codec, typeof(FFMPEG.AVCodecContext));

                if (codec.codec_type == FFMPEG.CodecType.CODEC_TYPE_AUDIO && audioStartIndex == -1) {
                    pAudioCodecContext = stream.codec;
                    pAudioStream = formatContext.streams[i];
                    audioCodecContext = codec;
                    audioStartIndex = i;

                    pAudioCodec = FFMPEG.avcodec_find_decoder(audioCodecContext.codec_id);
                    if (pAudioCodec == IntPtr.Zero) {
                        throw new FileLoadException("Unable to find codec");
                    }

                    FFMPEG.avcodec_open(stream.codec, pAudioCodec);
                }
            }

            if (audioStartIndex == -1) {
                throw new FileLoadException("Unable to find audio stream");
            }

            SampleSize = audioCodecContext.frame_size;

        }

        /// <summary>
        /// Closes and purges the audio file from FFMPEG library
        /// </summary>
        public void CloseMedia() {
            FFMPEG.avcodec_close(pAudioCodecContext);
            FFMPEG.av_close_input_file(pFormatContext);

            audioCodecContext = new FFMPEG.AVCodecContext();
            formatContext = new FFMPEG.AVFormatContext();

            pAudioStream = IntPtr.Zero;
            pAudioCodec = IntPtr.Zero;
            pAudioCodecContext = IntPtr.Zero;
            pFormatContext = IntPtr.Zero;

            audioStartIndex = -1;

            CurrentFrameNumber = -1;
            SampleSize = -1;

        }

        /// <summary>
        /// Reads a series of frames and returns a MemoryStream with the PCM data
        /// </summary>
        /// <param name="numFramesToRead"></param>
        /// <returns></returns>
        public MemoryStream ReadFrames(int numFramesToRead) {
            MemoryStream rval = new MemoryStream();
            for (int i = 0; i < numFramesToRead; i++) {
                byte[] vals = RetrieveNextFrame();
                if (vals == null) {
                    if (rval.Length == 0) {
                        return null;
                    }
                    break;
                }
                rval.Write(vals, 0, vals.Length);
            }

        #if UNITTEST
            System.IO.BinaryWriter OriginalData = new System.IO.BinaryWriter(new System.IO.FileStream("OriginalPCM.raw", FileMode.Append, FileAccess.Write));
            OriginalData.Write(rval.ToArray());
            OriginalData.Close();
        #endif

            return rval;
        }
        
        #endregion

        #region [ Private ]

        private int audioStartIndex = -1;
        private IntPtr pAudioStream;
        private IntPtr pAudioCodec;
        private IntPtr pAudioCodecContext;
        private FFMPEG.AVCodecContext audioCodecContext;
        private IntPtr pFormatContext;
        private FFMPEG.AVFormatContext formatContext;
        
        #endregion

    }

}
