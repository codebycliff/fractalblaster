using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace FractalBlaster.Plugins.ChopperEffect
{
    public partial class ChopperEffectUI : Form
    {
        long bytesReceived;
        Thread windowThread;
        Thread textThread;
        MemoryStream myStream;
        String windowText;
        ChopperEffectPlugin rec;
        Mutex bufferlock;

        public ChopperEffectUI()
        {
            bytesReceived = 0;
            InitializeComponent();
            myStream = new MemoryStream();
            windowThread = new Thread(new ThreadStart(updateWindow));
            windowThread.Start();
            textThread = new Thread(new ThreadStart(updateText));
            textThread.Start();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            bufferlock = new Mutex();
        }

        public void setReciever(ChopperEffectPlugin r)
        {
            this.rec = r;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            windowThread.Abort();
            textThread.Abort();
        }

        public void rewindBuffer()
        {
            myStream.Seek(0, SeekOrigin.Begin);
        }

        public void lockBuffer()
        {
            bufferlock.WaitOne();
        }

        public void unlockBuffer()
        {
            bufferlock.ReleaseMutex();
        }

        public void addByte(byte b)
        {
            bytesReceived++;
            myStream.WriteByte(b);
        }



        void updateWindow()
        {
            while (true)
            {
                textBox1.Text = windowText;
                Thread.Sleep(10);
            }
        }

        void updateText()
        {
            while (true)
            {
                double bufferLength = myStream.Length / 44.1;
                windowText = "bytesReceived: " + bytesReceived.ToString() + "\r\n"
                    + "buffer length: " + bufferLength.ToString() + "ms\r\n"
                    + "myStream.length: " + myStream.Length.ToString() + "\r\n"
                    + "myStream: " + getAudioData();
                Thread.Sleep(10);
            }
        }

        private String getAudioData()
        {
            bufferlock.WaitOne();
            byte[] byteArray = new byte[441];
            for (int i = 0; i < 441; i++)
            {
                int readResult = myStream.ReadByte();
                if (readResult == -1)
                {
                    byteArray[i] = 0;
                }
                else
                {
                    byteArray[i] = (byte) readResult;
                }
            }
            string s = new String('0', 0);
            for (int i = 0; i < 441; i++)
            {
                s = s + "\r\n" + byteArray[i].ToString();
            }
            bufferlock.ReleaseMutex();
            return s;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            rec.ChangeEffect(trackBar1.Value);
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
