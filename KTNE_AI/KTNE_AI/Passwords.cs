using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class Passwords
    {
        private bool _readyPrompt = false;
        private int _letterCount = 0;
        private int _currColumn = 0;

        private List<List<char>> _letters;
        private List<string> _wordBank;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public Passwords()
        {
            Init();
        }

        private void Init()
        {
            _wordBank = new List<string> {"ABOUT", "AFTER", "AGAIN", "BELOW", "COULD", "EVERY", "FIRST", "FOUND", "GREAT", "HOUSE",
                "LARGE", "LEARN", "NEVER", "OTHER", "PLACE", "PLANT", "POINT", "RIGHT", "SMALL", "SOUND", "SPELL", "STILL", "STUDY",
                "THEIR", "THERE", "THESE", "THING", "THINK", "THREE", "WATER", "WHERE", "WHICH", "WORLD", "WOULD", "WRITE"};

            _letters = new List<List<char>>();

            // Columns 1-5
            _letters.Add(new List<char>());
            _letters.Add(new List<char>());
            _letters.Add(new List<char>());
            _letters.Add(new List<char>());
            _letters.Add(new List<char>());

            Complete = false;
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Passwords. Begin by telling me each of the letters in column " + (_currColumn + 1); 
            }

            if (_letterCount == 5 && getLetter(audioStr) != ' ')
            {
                _letters[_currColumn][_letterCount] = getLetter(audioStr);

                _letterCount = 0;

                if (_currColumn != _letters.Count - 1)
                         _currColumn++;

                return checkForAnswer();
            }
            else if (getLetter(audioStr) != ' ')
            {
                _letters[_currColumn][_letterCount] = getLetter(audioStr);
            }
            else
            {
                return "\nI didn't catch that.\nPlease repeat the previous letter."; 
            }

            return "";
        }

        private char getLetter(string letter)
        {
            switch (letter)
            {
                case "A":
                    return 'A';
                case "B":
                    return 'B';
                case "C":
                    return 'C';
                case "D":
                    return 'D';
                case "E":
                    return 'E';
                case "F":
                    return 'F';
                case "G":
                    return 'G';
                case "H":
                    return 'H';
                case "I":
                    return 'I';
                case "J":
                    return 'J';
                case "K":
                    return 'K';
                case "L":
                    return 'L';
                case "M":
                    return 'M';
                case "N":
                    return 'N';
                case "O":
                    return 'O';
                case "P":
                    return 'P';
                case "Q":
                    return 'Q';
                case "R":
                    return 'R';
                case "S":
                    return 'S';
                case "T":
                    return 'T';
                case "U":
                    return 'U';
                case "V":
                    return 'V';
                case "W":
                    return 'W';
                case "X":
                    return 'X';
                case "Y":
                    return 'Y';
                case "Z":
                    return 'Z';
                default: // Return an empty char is unrecognized
                    return ' ';
            }
        }

        private string checkForAnswer()
        {
            return "";
        }
    }
}
