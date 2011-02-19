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

        static DecoderInterop() {
            // FractalBlaster.Plugins.Decoder.FFMPEG Register All Encoders/Decoders
            FFMPEG.av_register_all();
        }
        
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

        public void Initialize(AppContext context) {
            Context = context;
        }

        ~DecoderInterop() {
            //FractalBlaster.Plugins.Decoder.FFMPEG.av_free_static();
        }

        public Byte[] RetrieveNextFrame() {
            byte[] rval;

            IntPtr pPacket = Marshal.AllocHGlobal(56);
            if (FFMPEG.av_read_frame(pFormatContext, pPacket) < 0) {
                // TODO: Error Log - Failed to retrieve frame
                return null;
            }

            IntPtr pSamples = IntPtr.Zero;
            FFMPEG.AVPacket packet = (FFMPEG.AVPacket)Marshal.PtrToStructure(pPacket, typeof(FFMPEG.AVPacket));

            Marshal.FreeHGlobal(pPacket);

            if (packet.stream_index != audioStartIndex) {
                return null;
            }

            try {
                // TODO: Define arbitrary AUDIO_FRAME_SIZE

                int AUDIO_FRAME_SIZE = 5000;

                int frameSize = 0;

                pSamples = Marshal.AllocHGlobal(AUDIO_FRAME_SIZE);
                int size = FFMPEG.avcodec_decode_audio(pAudioCodecContext, pSamples, out frameSize, packet.data, packet.size);

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

        public void SeekBeginning() {
            int flags = FFMPEG.AVSEEK_FLAG_ANY | FFMPEG.AVSEEK_FLAG_BACKWARD;
            long newTimestamp = (long)(0 * FFMPEG.AV_TIME_BASE);

            if (FFMPEG.av_seek_frame(pFormatContext, -1, newTimestamp, flags) < 0) {
                // Look into replacing with avformat_seek_file to seek to more locations
                // Error?
            }

            FFMPEG.avcodec_flush_buffers(pAudioCodecContext);
        }

        public void OpenMedia(MediaFile media) {

            // Open Input Audio File
            if (FFMPEG.av_open_input_file(out pFormatContext, media.Info.FullName, IntPtr.Zero, 0, IntPtr.Zero) < 0) {
                throw new FileLoadException("");
            }

            // Determine Audio Codec
            if (FFMPEG.av_find_stream_info(pFormatContext) < 0) {
                throw new FileLoadException("Unable to read audio stream");
                // TODO: Error Log
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
                        // TODO: Error Log
                    }

                    FFMPEG.avcodec_open(stream.codec, pAudioCodec);
                }
            }

            if (audioStartIndex == -1) {
                throw new FileLoadException("Unable to find audio stream");
                // TODO: Error Log
            }

            SampleSize = audioCodecContext.frame_size;

        }

        public void CloseMedia() {
            //FractalBlaster.Plugins.Decoder.FFMPEG.av_freep(pAudioStream);
            //FractalBlaster.Plugins.Decoder.FFMPEG.avcodec_close(pAudioCodecContext);

            FFMPEG.av_close_input_file(pFormatContext);
        }

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
