using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Collections.Concurrent;

namespace B2SServerDataRecorder
{
     class Recorder 
    {
        ConcurrentQueue<RecorderData> WriteQueue = new ConcurrentQueue<RecorderData>();
        StreamWriter File = null;
        Timer WriteTimer;

        public void Record(string Text)
        {
            WriteQueue.Enqueue(new RecorderData() { Timestamp = DateTime.Now, Text = Text });
        }

        public void Init()
        {
            File = new StreamWriter(".\\B2SEventData.txt", true);
            WriteTimer = new Timer();
            WriteTimer.Interval = 1000;
            WriteTimer.AutoReset = false;
            WriteTimer.Elapsed += new ElapsedEventHandler(WriteTimer_Elapsed);
            WriteTimer.Start();
        }

       public void Finish()
        {
            WriteTimer.Stop();
            WriteTimer.Elapsed -= new ElapsedEventHandler(WriteTimer_Elapsed);
            WriteTimer = null;

            WriteDataToFile();
           
           File.Close();
            File.Dispose();
        }


        void WriteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WriteDataToFile();
            WriteTimer.Start();
        }

        private void WriteDataToFile()
        {
            RecorderData D = null;
            while (WriteQueue.TryDequeue(out D))
            {
                File.WriteLine(string.Format("{0:hh:mm:ss.ffff}\t{1}", D.Timestamp, D.Text));

            }
        }



    }
}
