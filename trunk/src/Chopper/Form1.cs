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

namespace Chopper
{
    public partial class Form1 : Form
    {
        long bytesReceived;
        Thread windowThread;
        Thread textThread;
        MemoryStream myStream;
        bool bufferlock;
        String windowText;
        Receiver rec;

        public Form1()
        {
            bytesReceived = 0;
            InitializeComponent();
            myStream = new MemoryStream();
            windowThread = new Thread(new ThreadStart(updateWindow));
            windowThread.Start();
            textThread = new Thread(new ThreadStart(updateText));
            textThread.Start();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        public void setReciever(Receiver r)
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
            bufferlock = true;
        }

        public void unlockBuffer()
        {
            bufferlock = false;
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
            if (bufferlock) return null;
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
            return s;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            rec.setEffect(trackBar1.Value);
        }
    }
}
