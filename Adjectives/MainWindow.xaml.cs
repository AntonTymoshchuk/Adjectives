using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Adjectives
{
    public partial class MainWindow : Window
    {
        private string[] gerWords;
        private string[] ukrWords;
        private SpeechRecognitionEngine speechRecognitionEngine;
        private CultureInfo listeningCultureInfo = new CultureInfo("de-de");

        public MainWindow()
        {
            InitializeComponent();

            InitializeWords();
            InitializeSpeechRecognitionEngine();
        }

        private void InitializeSpeechRecognitionEngine()
        {
            speechRecognitionEngine = new SpeechRecognitionEngine(listeningCultureInfo);
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.SpeechRecognized += SpeechRecognitionEngine_SpeechRecognized;

            Choices choices = new Choices(gerWords);

            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = listeningCultureInfo;
            grammarBuilder.Append(choices);

            Grammar grammar = new Grammar(grammarBuilder);
            speechRecognitionEngine.LoadGrammar(grammar);

            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void InitializeWords()
        {
            gerWords = new string[]
            {
                "Ängstlich",
                "Aufgeregt",
                "Bedrückt",
                "Begeistert",
                "Beleidigt",
                "Blöd",
                "Einsam",
                "Ekelhaft",
                "Enttäuscht",
                "Ernst",
                "Erschrocken",
                "Fremd",
                "Froh",
                "Furchtbar",
                "Geborgen",
                "Gelangweilt",
                "Müde",
                "Mürrisch",
                "Ratlos",
                "Sauer",
                "Schlimm",
                "Übermütig",
                "Unzufrieden",
                "Verlegen",
                "Wütend",
                "Zornig",
                "Zufrieden"
            };

            ukrWords = new string[]
            {
                "Боязкий",
                "Схвильований",
                "Пригнічений",
                "Захоплений",
                "Скривжений",
                "Дурний",
                "Самотній",
                "Огидний",
                "Розчарований",
                "Серйозний",
                "Зляканий",
                "Відчужений",
                "Радісний",
                "Жахливий",
                "Захищений",
                "Нудний",
                "Втомлений",
                "Буркотливий",
                "Безпорадний",
                "Сумний",
                "Поганий",
                "Жвавий",
                "Незадоволений",
                "Збентежений",
                "Розлючений",
                "Злий",
                "Задоволений"
            };
        }

        private void SpeechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence * 100 >= 50)
            {
                gerWordTextBlock.Text = e.Result.Text;
                for (int i = 0; i < gerWords.Length; i++)
                {
                    if (gerWords[i].Equals(e.Result.Text))
                    {
                        ukrWordTextBlock.Text = ukrWords[i];
                        break;
                    }
                }
            }
        }
    }
}
