using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace FractalBlaster.FFMPEG
{
    public class DecoderInterop
    {
        private string file;
        private int audioStartIndex = -1;
        private IntPtr pAudioStream;
        private IntPtr pAudioCodec;
        private IntPtr pAudioCodecContext;
        private FFMPEG.AVCodecContext audioCodecContext;
        private IntPtr pFormatContext;
        private FFMPEG.AVFormatContext formatContext;

        private int sampleSize;
        private int currentFrame;

        public int SampleSize
        {
            get { return sampleSize; }
        }

        public int CurrentFrameNumber
        {
            get { return currentFrame; }
        }

        static DecoderInterop()
        {
            // FFMPEG Register All Encoders/Decoders
            FFMPEG.av_register_all();
        }

        public DecoderInterop(string file)
        {
            this.file = file;
            audioCodecContext = new FFMPEG.AVCodecContext();
            formatContext = new FFMPEG.AVFormatContext();

            pAudioStream = IntPtr.Zero;
            pAudioCodec = IntPtr.Zero;
            pAudioCodecContext = IntPtr.Zero;
            pFormatContext = IntPtr.Zero;

            audioStartIndex = -1;

            currentFrame = -1;
            sampleSize = -1;
        }

        ~DecoderInterop()
        {
            FFMPEG.av_free_static();
        }

        public bool OpenAndAnalyze()
        {
            bool success = true;

            // Open Input Audio File
            if (FFMPEG.av_open_input_file(out pFormatContext, file, IntPtr.Zero, 0, IntPtr.Zero) < 0)
            {
                success = false;
                // TODO: Error Log - Input File Not Found / Unable to Open
            }

            // Determine Audio Codec
            if (FFMPEG.av_find_stream_info(pFormatContext) < 0)
            {
                success = false;
                // TODO: Error Log - Unable to read audio stream
            }

            formatContext = (FFMPEG.AVFormatContext)Marshal.PtrToStructure(pFormatContext, typeof(FFMPEG.AVFormatContext));

            for (int i = 0; i < formatContext.nb_streams; ++i)
            {
                FFMPEG.AVStream stream = (FFMPEG.AVStream)Marshal.PtrToStructure(formatContext.streams[i], typeof(FFMPEG.AVStream));

                FFMPEG.AVCodecContext codec = (FFMPEG.AVCodecContext)Marshal.PtrToStructure(stream.codec, typeof(FFMPEG.AVCodecContext));

                if (codec.codec_type == FFMPEG.CodecType.CODEC_TYPE_AUDIO && audioStartIndex == -1)
                {
                    pAudioCodecContext = stream.codec;
                    pAudioStream = formatContext.streams[i];
                    audioCodecContext = codec;
                    audioStartIndex = i;

                    pAudioCodec = FFMPEG.avcodec_find_decoder(audioCodecContext.codec_id);
                    if (pAudioCodec == IntPtr.Zero)
                    {
                        // TODO: Error Log - Unable to find codec
                        success = false;
                    }

                    FFMPEG.avcodec_open(stream.codec, pAudioCodec);
                }
            }

            if (audioStartIndex == -1)
            {
                // TODO: Error Log - Unable to find audio stream
                success = false;
            }

            sampleSize = audioCodecContext.frame_size;        
                       
            return success;
        }

        public byte[] RetrieveNextFrame()
        {
            byte[] rval;

            IntPtr pPacket = Marshal.AllocHGlobal(56);
            if (FFMPEG.av_read_frame(pFormatContext, pPacket) < 0)
            {
                // TODO: Error Log - Failed to retrieve frame
                return null;
            }

            IntPtr pSamples = IntPtr.Zero;
            FFMPEG.AVPacket packet = (FFMPEG.AVPacket)Marshal.PtrToStructure(pPacket, typeof(FFMPEG.AVPacket));

            Marshal.FreeHGlobal(pPacket);

            if (packet.stream_index != audioStartIndex)
            {
                return null;
            }

            try 
            {               
                // TODO: Define arbitrary AUDIO_FRAME_SIZE

                int AUDIO_FRAME_SIZE = 5000;
                
                int frameSize = 0;

                pSamples = Marshal.AllocHGlobal(AUDIO_FRAME_SIZE);
                int size = FFMPEG.avcodec_decode_audio(pAudioCodecContext, pSamples, out frameSize, packet.data, packet.size);

                currentFrame++;

                rval = new byte[frameSize];
                Marshal.Copy(pSamples, rval, 0, frameSize);

                return rval;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(pSamples);
            }
        }

        public void Close()
        {
            FFMPEG.av_freep(pAudioStream);

            FFMPEG.avcodec_close(pAudioCodecContext);

            FFMPEG.av_close_input_file(pFormatContext);
        }

        public AudioMetadata RetrieveMetadata()
        {
            AudioMetadata data = new AudioMetadata();

            char[] nullarray = { '\0' };

            data.Album = System.Text.Encoding.ASCII.GetString(formatContext.album);
            data.Album = data.Album.Trim();
            data.Album = data.Album.Trim(nullarray);

            data.Artist = System.Text.Encoding.ASCII.GetString(formatContext.author);
            data.Artist = data.Artist.Trim();
            data.Artist = data.Artist.Trim(nullarray);

            data.Duration = (int)formatContext.duration;
            data.Duration /= 1000000;

            data.Year = formatContext.year;

            data.Title = System.Text.Encoding.ASCII.GetString(formatContext.title);
            data.Title = data.Title.Trim();
            data.Title = data.Title.Trim(nullarray);

            data.Channels = audioCodecContext.channels;

            data.SampleRate = audioCodecContext.sample_rate;

            return data;
        }


    }
}
