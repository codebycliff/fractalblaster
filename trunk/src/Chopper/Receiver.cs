using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Engine;

namespace Chopper
{
    public class Receiver : PCMReceiver
    {
        Form1 myForm;
        int effect;

        public Receiver()
        {
            myForm = new Form1();
            myForm.setReciever(this);
            myForm.Show();
        }

        public void setEffect(int i)
        {
            effect = i;
        }

        #region IPCMReceiver Members

        public override void receiveFrames(System.IO.MemoryStream PCM)
        {
            long l = PCM.Length;
            if (effect != 0)
            {
                for (int i = 0; i < l; i++)
                {
                    if ((i % effect) > effect / 2) PCM.WriteByte(0);
                    else PCM.ReadByte();
                }
            }
            PCM.Seek(0, System.IO.SeekOrigin.Begin);
            myForm.lockBuffer();
            myForm.rewindBuffer();
            for (int b = 0; b != -1; b = PCM.ReadByte())
            {
                myForm.addByte((byte)b);
            }
            myForm.rewindBuffer();
            myForm.unlockBuffer();
         }

        #endregion
    }
}
