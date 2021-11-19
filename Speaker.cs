using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Bia
{
    public class Speaker
    {
        private static SpeechSynthesizer sp = new SpeechSynthesizer();
        public static void Speak(string text)
        {
            //Caso ele esteja falando
            if (sp.State == SynthesizerState.Speaking)
                sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text);
        }
    }
}
