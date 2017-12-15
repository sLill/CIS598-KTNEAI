using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class MorseCode
    {
        private bool _readyPrompt = false;
        private string _currentLetter = "";
        private List<char> _letterList = new List<char>();
        private List<string> _remainingPossibilities;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public MorseCode()
        {
            Complete = false;
            _remainingPossibilities = new List<string> { "shell", "halls", "slick", "trick", "boxes", "leaks", "strobe", "bistro",
                "flick", "bombs", "break", "brick", "steak", "sting", "vector", "beats"};
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Morse Code.\n\nDecribe using LONGS, SHORTS, and SPACES.\n\nBegin whenever you're ready. ";
            }

            if (audioStr == "Space")
            {
                if (checkForWordMatch() != "No matches yet.")
                {
                    return checkForWordMatch();
                }
                return checkForLetterMatch();
            }
            else
                _currentLetter += (audioStr + ".");          

            if (audioStr == "Short")
                return "*****";
            else
                return "-----";
        }

        private string checkForLetterMatch()
        {
            string letter = _currentLetter;
            _currentLetter = "";

            switch (letter)
            {
                case "Short.Long.":
                    _letterList.Add('a');
                    return "\nGot it.";
                case "Long.Short.Short.Short.":
                    _letterList.Add('b');
                    return "\nGot it.";
                case "Long.Short.Long.Short.":
                    _letterList.Add('c');
                    return "\nGot it.";
                case "Long.Short.Short.":
                    _letterList.Add('d');
                    return "\nGot it.";
                case "Short.":
                    _letterList.Add('e');
                    return "\nGot it.";
                case "Short.Short.Long.Short.":
                    _letterList.Add('f');
                    return "\nGot it.";
                case "Long.Long.Short.":
                    _letterList.Add('g');
                    return "\nGot it."; ;
                case "Short.Short.Short.Short.":
                    _letterList.Add('h');
                    return "\nGot it."; ;
                case "Short.Short.":
                    _letterList.Add('i');
                    return "\nGot it."; ;
                case "Short.Long.Long.Long.":
                    _letterList.Add('j');
                    return "\nGot it."; ;
                case "Long.Short.Long.":
                    _letterList.Add('k');
                    return "\nGot it."; ;
                case "Short.Long.Short.Short.":
                    _letterList.Add('l');
                    return "\nGot it."; ;
                case "Long.Long.":
                    _letterList.Add('m');
                    return "\nGot it."; ;
                case "Long.Short.":
                    _letterList.Add('n');
                    return "\nGot it."; ;
                case "Long.Long.Long.":
                    _letterList.Add('o');
                    return "\nGot it."; ;
                case "Short.Long.Long.Short.":
                    _letterList.Add('p');
                    return "\nGot it."; ;
                case "Long.Long.Short.Long.":
                    _letterList.Add('q');
                    return "\nGot it."; ;
                case "Short.Long.Short.":
                    _letterList.Add('r');
                    return "\nGot it."; ;
                case "Short.Short.Short.":
                    _letterList.Add('s');
                    return "\nGot it."; ;
                case "Long.":
                    _letterList.Add('t');
                    return "\nGot it."; ;
                case "Short.Short.Long.":
                    _letterList.Add('u');
                    return "\nGot it."; ;
                case "Short.Short.Short.Long.":
                    _letterList.Add('v');
                    return "\nGot it."; ;
                case "Short.Long.Long.":
                    _letterList.Add('w');
                    return "\nGot it."; ;
                case "Long.Short.Short.Long.":
                    _letterList.Add('x');
                    return "\nGot it."; ;
                case "Long.Short.Long.Long.":
                    _letterList.Add('y');
                    return "\nGot it."; ;
                case "Long.Long.Short.Short.":
                    _letterList.Add('z');
                    return "\nGot it."; ;
                default:
                    return "I don't have a match for that letter.";
            }
        }

        private string checkForWordMatch()
        {
            for (int i = 0; i < _letterList.Count; i++)
            {
                for (int j = 0; j < _remainingPossibilities.Count; j++)
                {
                    if (!_remainingPossibilities[j].Contains(_letterList[i]))
                    {
                        _remainingPossibilities.Remove(_remainingPossibilities[j]);
                        j--;
                    }
                }
            }                

            if(_remainingPossibilities.Count == 1)
            {
                switch (_remainingPossibilities[0])
                {
                    case "shell":
                        return "\nTurn to 3.505";
                    case "halls":
                        return "\nTurn to 3.515";
                    case "slick":
                        return "\nTurn to 3.522";
                    case "trick":
                        return "\nTurn to 3.532";
                    case "boxes":
                        return "\nTurn to 3.535";
                    case "leaks":
                        return "\nTurn to 3.542";
                    case "strobe":
                        return "\nTurn to 3.545";
                    case "bistro":
                        return "\nTurn to 3.552";
                    case "flick":
                        return "\nTurn to 3.555";
                    case "bombs":
                        return "\nTurn to 3.565";
                    case "break":
                        return "\nTurn to 3.572";
                    case "brick":
                        return "\nTurn to 3.575";
                    case "steak":
                        return "\nTurn to 3.582";
                    case "sting":
                        return "\nTurn to 3.592";
                    case "vector":
                        return "\nTurn to 3.595";
                    case "beats":
                        return "\nTurn to 3.600";
                }
                Complete = true;
            }

            return "No matches yet.";
        }
    }
}
