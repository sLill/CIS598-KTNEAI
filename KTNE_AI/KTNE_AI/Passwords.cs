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

            // Erase the last letter added
            if (audioStr == "Nope")
            {
                return eraseLastLetter();
            }

            // Begin adding letters and checking for answers
            if (_letterCount == 5 && getLetter(audioStr) != ' ')
            {
                string returnString;

                _letters[_currColumn].Add(getLetter(audioStr));
                returnString = checkForAnswer(audioStr);

                _letterCount = 0;

                if (_currColumn != _letters.Count - 1)
                         _currColumn++;

                // See if we've narrowed the possible answers to 1. If not ask for the letters in the next column
                return returnString;
            }
            else if (getLetter(audioStr) != ' ')
            {
                _letters[_currColumn].Add(getLetter(audioStr));
                _letterCount++;

                return audioStr;
            }
            else
            {
                return "I didn't catch that."; 
            }
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

        private string checkForAnswer(string letter)
        {
            // Loop through all words checking to make sure they contain atleast one of the letters in each column
            for (int i = 0; i < _wordBank.Count; i++)
            {
                    bool letterFound = false;

                    for (int k = 0; k < 6; k++)
                    {
                    if (_wordBank[i][_currColumn].ToString().ToUpper() == _letters[_currColumn][k].ToString())
                        {
                            // Signal that a letter is present in this column and move on to the next
                            letterFound = true;
                            break;
                        }
                    }

                    // If no letter is found remove the word from the list of possible answers
                    if (!letterFound)
                    {
                        _wordBank.RemoveAt(i);
                        i--;
                    }
            }

            if (_wordBank.Count == 0)
            {
                Complete = true;
                return "\nI'm all out of possibilities.\nPerhaps there was a mistake.\nTry starting over.";
            }
            else if (_wordBank.Count == 1)
            {
                string returnString = "\nThe password is ";
                Complete = true;

                for (int i = 0; i < _wordBank[0].Length; i++)
                    returnString += (_wordBank[0][i] + " ");

                return returnString;
            }
            else
                return letter + "\nReady for the letters in column " + (_currColumn + 2);
        }

        private string eraseLastLetter()
        {
            // Make sure there's a letter to erase before we try to get rid of it
            if (_letters[_currColumn].Count == 0)
                return "\nI don't have any letters to remove yet.";
            else
            {
                char letterToRemove = _letters[_currColumn][_letterCount-1];

                _letters[_currColumn].RemoveAt(_letterCount-1);
                _letterCount--;

                return "Removed " + letterToRemove;
            }
        }
    }
}
