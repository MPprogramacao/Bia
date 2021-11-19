using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace Bia
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine engine;


        public Form1()
        {
            InitializeComponent();
        }

        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();
                engine.SetInputToDefaultAudioDevice();

                Choices c_commandsOfSytem = new Choices();
                c_commandsOfSytem.Add(GrammarRules.WhatTimeIS.ToArray());

                GrammarBuilder gb_commandsOfSytem = new GrammarBuilder();
                gb_commandsOfSytem.Append(c_commandsOfSytem);

                Grammar g_commandsOfSytem = new Grammar(gb_commandsOfSytem);
                g_commandsOfSytem.Name = "sys";

                engine.LoadGrammar(g_commandsOfSytem);

                //Carregar a gramatica
                //engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audiolevel);

                engine.RecognizeAsync(RecognizeMode.Multiple);

                Speaker.Speak("Estou carregando os arquivos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro no LoadSpeech():" + ex.Message);
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSpeech();
        }

        //Método de reconhecimento
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            float conf = e.Result.Confidence;

            if (conf > 0.25f)
            {
                switch(e.Result.Grammar.Name)
                {
                    case "sys":                        
                        if(GrammarRules.WhatTimeIS.Any(x => x == speech))
                        {
                            Runner.WhatTimeIs();
                        }
                        break;
                }
            }
        }

        private void audiolevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }
    }
}
