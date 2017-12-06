using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class SimonSays
    {
        private bool _readyPrompt = false;

        private bool? _serialNumContainsVowel = null;
        private string _outputSequence;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public SimonSays()
        {
            Init();
        }

        private void Init()
        {
            Complete = false;
            _outputSequence = "";
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Simon Says.\nDoes the serial number contain a vowel?";
            }

            if (audioStr == "Finished")
            {
                Complete = true;
                return "";
            }

            if (_serialNumContainsVowel == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _serialNumContainsVowel = true;
                        return "Ready for the first color.";
                    case "No":
                    case "Definitely no":
                        _serialNumContainsVowel = false;
                        return "Ready for the first color.";
                    default:
                        return "\nDoes the serial number contain a vowel?";
                }
            }
            else
                GetSequence(audioStr);

            return _outputSequence;
        }

        private void GetSequence(string color)
        {
            if (_serialNumContainsVowel == true)
            {
                switch (color)
                {
                    case "Blue":
                        _outputSequence += "Red ";
                        break;
                    case "Red":
                        _outputSequence += "Blue ";
                        break;
                    case "Yellow":
                        _outputSequence += "Green ";
                        break;
                    case "Green":
                        _outputSequence += "Yellow ";
                        break;
                }
            }
            else
            {
                switch (color)
                {
                    case "Blue":
                        _outputSequence += "Yellow ";
                        break;
                    case "Red":
                        _outputSequence += "Blue ";
                        break;
                    case "Yellow":
                        _outputSequence += "Red ";
                        break;
                    case "Green":
                        _outputSequence += "Green ";
                        break;
                }
            }
        }
    }
}
