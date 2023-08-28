using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using B2SServerPluginInterface;
using System.Windows.Forms;

namespace B2SServerDataRecorder
{
    [Export(typeof(IDirectPlugin))]
    public class DataRecorderPlugin : IDirectPlugin,  IDirectPluginPinMame
    {
        private Recorder R;

        public void DataReceive(char TableElementTypeChar, int Number, int Value)
        {
            R.Record(string.Format("{0}\t{1}\t{2}",TableElementTypeChar, Number,Value));
        }

        public string Name
        {
            get
            {
                //Get the version of the assembly
                Version V = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                //Calculate the BuildDate based in the build and revsion number of the project.
                DateTime BuildDate = new DateTime(2000, 1, 1).AddDays(V.Build).AddSeconds(V.Revision * 2);
                //Format and return the name string.
                return string.Format("Data Recorder Plugin C# (V: {0} as of {1})", V.ToString(), BuildDate.ToString("yyyy.MM.dd hh:mm"));
            }
        }

        public void PluginFinish()
        {
            R.Record("--> Plugin Finish");
            R.Finish();
        }

        public void PluginInit(string TableFilename, string RomName = "")
        {
            R = new Recorder();
            R.Init();
            R.Record("--> Plugin Init");
            R.Record("--> TableFile: " + TableFilename);
            R.Record("--> Rom: " + RomName);
        }

        //public void PluginShowFrontend(Form Owner = null)
        //{

        //}

        public void PinMameContinue()
        {
            R.Record("-- > Pinmame Continue");
        }

        public void PinMamePause()
        {
            R.Record("-- > Pinmame Pause");

        }

        public void PinMameRun()
        {
            R.Record("-- > Pinmame Run");

        }

        public void PinMameStop()
        {
            R.Record("-- > Pinmame Stop");

        }
    }
}
