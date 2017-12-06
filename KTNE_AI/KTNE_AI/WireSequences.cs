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
                return "\nReady for Wire Sequences.";
            }

            return "";
        }

        private void initOccurrenceArrays()
        {
            _redOccurrences = new string[]{"C", "B", ""};
        }
    }
}
