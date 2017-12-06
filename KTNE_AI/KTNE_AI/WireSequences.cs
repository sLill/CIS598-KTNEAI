using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class WireSequences
    {
        private bool _readyPrompt = false;
        private string[] _redOccurrences;
        private string[] _blueOccurrences;
        private string[] _blackOccurrences;

        private int _redIndex = 0;
        private int _blueIndex = 0;
        private int _blackIndex = 0;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public WireSequences()
        {
            Complete = false;
            initOccurrenceArrays();
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Wire Sequences. What color is the first wire?";
            }

            string result = "";
            if (audioStr == "Red")
            {
                result = "\nCut if connected to " + _redOccurrences[_redIndex] + ".\n\nNext wire.";
                if (_redIndex < 8)
                    _redIndex++;

                return result;
            }
            else if (audioStr == "Blue")
            {
                result =  "\nCut if connected to " + _blueOccurrences[_blueIndex] + ".\n\nNext wire.";
                if (_blueIndex < 8)
                    _blueIndex++;

                return result;
            }
            else if (audioStr == "Black")
            {
                result = "\nCut if connected to " + _blackOccurrences[_blackIndex] + ".\n\nNext wire.";
                if (_blackIndex < 8)
                    _blackIndex++;

                return result;
            }
            else if (audioStr == "Finished")
            {
                Complete = true;
                return "";
            }
            else
                return "\nI don't understand. Please repeat your response.";
        }

        private void initOccurrenceArrays()
        {
            _redOccurrences = new string[]{"'C'", "'B'", "'A'", "'A' or 'C'", "'B'", "'A' or 'C'", "'A', 'B' or 'C'", "'A' or 'B'", "'B'"};
            _blueOccurrences = new string[] { "'B'", "'A' or 'C'", "'B'", "'A'", "'B'", "'B' or 'C'", "'C'", "'A' or 'C'", "'A'" };
            _blackOccurrences = new string[] { "'A', 'B' or 'C'", "'A' or 'C'", "'B'", "'A' or 'C'", "'B'", "'B' or 'C'", "'A' or 'B'", "'C'", "'C'" };
        }
    }
}
