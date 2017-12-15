using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using NAudio.Wave;

namespace KTNE_AI
{
    public partial class mainform : Form
    {
        private SpeechRecognitionEngine _speechEngine = new SpeechRecognitionEngine();
        private SpeechSynthesizer _speechSynth = new SpeechSynthesizer();
        private List<string> _manualSegments = new List<string>();
        private State _modState;

        // Module objects. Will be created as needed. No need to have all at once
        private SimpleWires _simpleWires;
        private BigButton _bigButton;
        private Symbols _symbols;
        private SimonSays _simonSays;
        private WordCalculator _wordCalculator;
        private Memory _memory;
        private MorseCode _morseCode;
        private ComplicatedWires _complicatedWires;
        private WireSequences _wireSequences;
        private Passwords _passwords;
        private NeedyKnob _needyKnob;
        private Maze _maze;

        private enum State
        {
            NoState,
            SimpleWires,
            BigButton,
            Symbols,
            SimonSays,
            WordCalculator,
            Memory,
            MorseCode,
            ComplicatedWires,
            WireSequences,
            Passwords,
            NeedyKnob,
            Maze
        };

        public mainform()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the text log and also reads it aloud to the user
        /// </summary>
        public void UpdateLog(string msg)
        {
            PromptBuilder pBuilder = new PromptBuilder();

            pBuilder.StartSentence();
            pBuilder.StartStyle(new PromptStyle(PromptRate.Fast));
            pBuilder.AppendText(msg);
            pBuilder.EndStyle();
            pBuilder.EndSentence();

            //_speechSynth.SetOutputToWaveFile(@".\audioWave.wav");
            _speechSynth.SpeakAsync(pBuilder);
            //_speechSynth.SetOutputToNull();

            //using (WaveFileReader waveReader = new WaveFileReader(@".\audioWave.wav"))
            //{
            //    WaveOut wOut = new WaveOut();
            //    wOut.Init(waveReader);
            //    wOut.Play();
            //}

            rtbLog.Text += ("\n" + msg);
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            InitSpeechSettings();
            //ImportManualSegments();
        }

        /// <summary>
        /// Sets up the engine that will be responsible for all audio coming from the user
        /// </summary>
        private void InitSpeechSettings()
        {
            Choices commands = new Choices();
            commands.Add(File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"../../../Resources/CommandList.txt"), Encoding.UTF8));

            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);

            Grammar grammar = new Grammar(gBuilder);
            //grammar.Priority = 127;

            _speechEngine.LoadGrammarAsync(grammar);
            _speechEngine.SetInputToDefaultAudioDevice();
            _speechEngine.SpeechRecognized += speechEngine_SpeechRecognized;

            _speechSynth.Volume = 50;
            //_speechSynth.SelectVoice
        }

        /// <summary>
        /// Reads in the manual for each module used in the game
        /// </summary>
        private void ImportManualSegments()
        {
            // Simple Wires
            _manualSegments.Add(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"../../../Resources/ManualSegments/SimpleWires.txt"), Encoding.UTF8));
        }

        /// <summary>
        /// Is passed an audio string containing a module name and sets the _modState accordingly
        /// </summary>
        /// <param name="mod"></param>
        private void SetModState(string mod)
        {
            switch(mod)
            {
                case "Simple wires":
                    _simpleWires = new SimpleWires();
                    _modState = State.SimpleWires;
                    UpdateLog(_simpleWires.Update(mod));
                    break;
                case "Big button":
                    _bigButton = new BigButton();
                    _modState = State.BigButton;
                    UpdateLog(_bigButton.Update(mod));
                    break;
                case "Symbols":
                    _symbols = new Symbols();
                    _modState = State.Symbols;
                    UpdateLog(_symbols.Update(mod));
                    break;
                case "Simon says":
                    _simonSays = new SimonSays();
                    _modState = State.SimonSays;
                    UpdateLog(_simonSays.Update(mod));
                    break;
                case "Word calculator":
                    _wordCalculator = new WordCalculator();
                    _modState = State.WordCalculator;
                    UpdateLog(_wordCalculator.Update(mod));
                    break;
                case "Memory":
                    _memory = new Memory();
                    _modState = State.Memory;
                    UpdateLog(_memory.Update(mod));
                    break;
                case "Morse Code":
                    _morseCode = new MorseCode();
                    _modState = State.MorseCode;
                    UpdateLog(_morseCode.Update(mod));
                    break;
                case "Complicated Wires":
                    _complicatedWires = new ComplicatedWires();
                    _modState = State.ComplicatedWires;
                    UpdateLog(_complicatedWires.Update(mod));
                    break;
                case "Wire Sequences":
                    _wireSequences = new WireSequences();
                    _modState = State.WireSequences;
                    UpdateLog(_wireSequences.Update(mod));
                    break;
                case "Passwords":
                    _passwords = new Passwords();
                    _modState = State.Passwords;
                    UpdateLog(_passwords.Update(mod));
                    break;
                case "Needy Knob":
                    _needyKnob = new NeedyKnob();
                    _modState = State.NeedyKnob;
                    UpdateLog(_needyKnob.Update(mod));
                    break;
                case "Maze":
                    _maze = new Maze();
                    _modState = State.Maze;
                    UpdateLog(_maze.Update(mod));
                    break;
                default:
                    UpdateLog("\nI don't recognize that module.");
                    break;
            }
        }

        private void speechEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Hard reset
            if (e.Result.Text == "Reset")
            {
                _modState = State.NoState;
                UpdateLog("\nReady for the next module.");
                return;
            }

            switch (_modState)
            {
                case State.NoState:
                    SetModState(e.Result.Text);
                    break;
                case State.SimpleWires:
                    UpdateLog(_simpleWires.Update(e.Result.Text));
                    if (_simpleWires.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.BigButton:
                    UpdateLog(_bigButton.Update(e.Result.Text));
                    if (_bigButton.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.Symbols:
                    UpdateLog(_symbols.Update(e.Result.Text));
                    if (_symbols.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.SimonSays:
                    UpdateLog(_simonSays.Update(e.Result.Text));
                    if (_simonSays.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.WordCalculator:
                    UpdateLog(_wordCalculator.Update(e.Result.Text));
                    if (_wordCalculator.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.Memory:
                    UpdateLog(_memory.Update(e.Result.Text));
                    if (_memory.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.MorseCode:
                    UpdateLog(_morseCode.Update(e.Result.Text));
                    if (_morseCode.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.ComplicatedWires:
                    UpdateLog(_complicatedWires.Update(e.Result.Text));
                    if(_complicatedWires.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.WireSequences:
                    UpdateLog(_wireSequences.Update(e.Result.Text));
                    if(_wireSequences.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.Passwords:
                    UpdateLog(_passwords.Update(e.Result.Text));
                    if (_passwords.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.NeedyKnob:
                    UpdateLog(_needyKnob.Update(e.Result.Text));
                    if (_needyKnob.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
                case State.Maze:
                    UpdateLog(_maze.Update(e.Result.Text));
                    if (_maze.Complete)
                    {
                        _modState = State.NoState;
                        UpdateLog("\nReady for the next module.");
                    }
                    break;
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            // Begin recording audio
            _speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnBegin.Enabled = false;
            UpdateLog("Ready to go!");
        }
    }
}
