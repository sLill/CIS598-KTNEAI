using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class NeedyKnob
    {
        private bool _readyPrompt = false;
        private Direction _direction;
        private List<int> _litPositions;
        private List<Tuple<string, List<int>>> _possibleOutcomes;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        private enum Direction
        {
            NoDirection,
            Up,
            Down,
            Left,
            Right
        }

        public NeedyKnob()
        {
            init();
        }

        private void init()
        {
            _direction = Direction.NoDirection;
            _litPositions = new List<int>();

            _possibleOutcomes = new List<Tuple<string, List<int>>>();
            _possibleOutcomes.Add(new Tuple<string, List<int>>("UP", new List<int> { 3,5,6,7,8,9,10,12 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("UP", new List<int> { 1,3,5,8,9,11,12 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("DOWN", new List<int> { 2,3,6,7,8,9,10,12 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("DOWN", new List<int> { 1,3,5,8,12 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("RIGHT", new List<int> { 1,3,4,5,6,7,8,9,11 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("RIGHT", new List<int> { 1,3,4,7,8,9,11 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("LEFT", new List<int> { 5,7,10,11,12 }));
            _possibleOutcomes.Add(new Tuple<string, List<int>>("LEFT", new List<int> { 5,10,11 }));        
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nDescribe what L E D's are lit from left to right.";
            }

            if (audioStr == "Nope" || audioStr == "No")
            {
                if (_litPositions.Count > 0)
                {
                    int removedNum = _litPositions[_litPositions.Count - 1];
                    _litPositions.RemoveAt(_litPositions.Count - 1);
                    return removedNum.ToString();
                }
                else
                {
                    return "Nothing to remove yet.";
                }
            }

            // Add the position to the list of positions and check to see if there's an answer yet
            return addLED(audioStr);
        }

        private string addLED(string position)
        {
            switch (position)
            {
                case "One":
                    _litPositions.Add(1);
                        return checkForAnswer(1);
                case "Two":
                    _litPositions.Add(2);
                        return checkForAnswer(2);
                case "Three":
                    _litPositions.Add(3);
                        return checkForAnswer(3);
                case "Four":
                    _litPositions.Add(4);
                        return checkForAnswer(4);
                case "Five":
                    _litPositions.Add(5);
                        return checkForAnswer(5);
                case "Six":
                    _litPositions.Add(6);
                        return checkForAnswer(6);
                case "Seven":
                    _litPositions.Add(7);
                        return checkForAnswer(7);
                case "Eight":
                    _litPositions.Add(8);
                        return checkForAnswer(8);
                case "Nine":
                    _litPositions.Add(9);
                        return checkForAnswer(9);
                case "Ten":
                    _litPositions.Add(10);
                        return checkForAnswer(10);
                case "Eleven":
                    _litPositions.Add(11);
                        return checkForAnswer(11);
                case "Twelve":
                    _litPositions.Add(12);
                        return checkForAnswer(12);
                default:
                    return "\nI didn't quite get that.";
            }
        }

        private string checkForAnswer(int position)
        {
            // Compare the list of remaining positions with all know/unknown postions and remove any that don't match
            for (int i = 0; i < _possibleOutcomes.Count; i++)
            {
                bool actionPerformed = false;

                // Known positions
                for (int j = 0; j < _litPositions.Count; j++)
                {
                    if (!_possibleOutcomes[i].Item2.Contains(_litPositions[j]))
                    {
                        _possibleOutcomes.Remove(_possibleOutcomes[i]);
                        actionPerformed = true;
                        i--;
                        break;
                    }
                }

                if (!actionPerformed)
                {
                    // Unknown positions up until the most recent 
                    for (int j = 1; j < position; j++)
                    {
                        if (_possibleOutcomes[i].Item2.Contains(j) && !_litPositions.Contains(j))
                        {
                            _possibleOutcomes.Remove(_possibleOutcomes[i]);
                            i--;
                            break;
                        }
                    }
                }
            }

            if (_possibleOutcomes.Count == 0)
            {
                Complete = true;
                return "\nI'm all out of possibilities.\nTry starting over.";
            }
            else if (_possibleOutcomes.Count == 1)
            {
                Complete = true;
                return "\nTurn the knob " + _possibleOutcomes[0].Item1 + " relative to the 'Up' label.";
            }
            else
                return position.ToString();
        }
    }
}
