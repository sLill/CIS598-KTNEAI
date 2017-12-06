using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class Memory
    {
        private bool _readyPrompt = false;
        private bool _waitingOnNumberPressed = false;
        private bool _waitingOnPositionPressed = false;
        private int _stage = 1;
        private List<int> _previousNumbers = new List<int>();
        private List<int> _previousPositions = new List<int>();

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public Memory ()
        {
            Complete = false;
        }

        public string Update(string audioStr)
        {
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Memory. What number is being displayed?";
            }

            if (_waitingOnNumberPressed || _waitingOnPositionPressed)
                return recordNumberOrPositionPressed(audioStr);
            else
                return determineNumber(audioStr);
                
        }

        private string determineNumber(string audioStr)
        {
            switch (_stage)
            {
                case 1:
                    switch (audioStr)
                    {
                        case "One":
                            _stage++;
                            _waitingOnNumberPressed = true;
                            _waitingOnPositionPressed = true;
                            return "\nPress the button in the second position and tell me what number and position was pressed.";
                        case "Two":
                            _stage++;
                            _waitingOnNumberPressed = true;
                            _waitingOnPositionPressed = true;
                            return "\nPress the button in the second position and tell me what number and position was pressed.";
                        case "Three":
                            _stage++;
                            _waitingOnNumberPressed = true;
                            _waitingOnPositionPressed = true;
                            return "\nPress the button in the third position and tell me what number and position was pressed.";
                        case "Four":
                            _stage++;
                            _waitingOnNumberPressed = true;
                            _waitingOnPositionPressed = true;
                            return "\nPress the button in the fourth position and tell me what number and position was pressed.";
                        default:
                            return "\nI don't understand. Please repeat your response.";
                    }

                case 2:
                    switch (audioStr)
                    {
                        case "One":
                            _stage++;
                            _previousNumbers.Add(4);
                            _waitingOnPositionPressed = true;
                            return "\nPress the button labeled 4 and tell me what position it was in.";
                        case "Two":
                            _stage++;
                            _previousPositions.Add(_previousPositions[0]);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in position " + _previousPositions[0] + " and tell me what number was pressed.";
                        case "Three":
                            _stage++;
                            _previousPositions.Add(1);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in the first position and tell me what number was pressed.";
                        case "Four":
                            _stage++;
                            _previousPositions.Add(_previousPositions[0]);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in position " + _previousPositions[0] + " and tell me what number was pressed.";
                        default:
                            return "\nI don't understand. Please repeat your response.";
                    }

                case 3:
                    switch (audioStr)
                    {
                        case "One":
                            _stage++;
                            _previousNumbers.Add(_previousNumbers[1]);
                            _waitingOnPositionPressed = true;
                            return "\nPress the button labeled " + _previousNumbers[1] + " and tell me what position it was in.";
                        case "Two":
                            _stage++;
                            _previousNumbers.Add(_previousNumbers[0]);
                            _waitingOnPositionPressed = true;
                            return "\nPress the button labeled " + _previousNumbers[0] + " and tell me what position it was in.";
                        case "Three":
                            _stage++;
                            _previousPositions.Add(3);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in the third position and tell me what number was pressed.";
                        case "Four":
                            _stage++;
                            _previousNumbers.Add(4);
                            _waitingOnPositionPressed = true;
                            return "\nPress the button labeled 4 and tell me what position it was in.";
                        default:
                            return "\nI don't understand. Please repeat your response.";
                    }

                case 4:
                    switch (audioStr)
                    {
                        case "One":
                            _stage++;
                            _previousPositions.Add(_previousPositions[0]);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in position " + _previousPositions[0] + " and tell me what number was pressed.";
                        case "Two":
                            _stage++;
                            _previousPositions.Add(1);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in the first position and tell me what number was pressed.";
                        case "Three":
                            _stage++;
                            _previousPositions.Add(_previousPositions[1]);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in position " + _previousPositions[1] + " and tell me what number was pressed.";                            
                        case "Four":
                            _stage++;
                            _previousPositions.Add(_previousPositions[1]);
                            _waitingOnNumberPressed = true;
                            return "\nPress the button in position " + _previousPositions[1] + " and tell me what number was pressed.";
                        default:
                            return "\nI don't understand. Please repeat your response.";
                    }

                case 5:
                    Complete = true;
                    switch (audioStr)
                    {
                        case "One":
                            _stage++;
                            return "\nPress the button labeled " + _previousNumbers[0];
                        case "Two":
                            _stage++;
                            return "\nPress the button labeled " + _previousNumbers[1];
                        case "Three":
                            _stage++;
                            return "\nPress the button labeled " + _previousNumbers[3];
                        case "Four":
                            _stage++;
                            return "\nPress the button labeled " + _previousNumbers[2];
                        default:
                            return "\nI don't understand. Please repeat your response.";
                    }
            }

            // Should never reach this line
            return "Error";
        }

        private string recordNumberOrPositionPressed(string audioStr)
        {
            string returnStr = "";

            switch (audioStr)
            {
                // Numbers
                case "Number one":
                    _waitingOnNumberPressed = false;                
                    _previousNumbers.Add(1);
                    returnStr += "\nGot it.";
                    break;
                case "Number two":
                    _waitingOnNumberPressed = false;
                    _previousNumbers.Add(2);
                    returnStr += "\nGot it.";
                    break;
                case "Number three":
                    _waitingOnNumberPressed = false;
                    _previousNumbers.Add(3);
                    returnStr += "\nGot it.";
                    break;
                case "Number four":
                    _waitingOnNumberPressed = false;
                    _previousNumbers.Add(4);
                    returnStr += "\nGot it.";
                    break;

                // Positions
                case "Position one":
                    _waitingOnPositionPressed = false;
                    _previousPositions.Add(1);
                    returnStr += "\nGot it.";
                    break;
                case "Position two":
                    _waitingOnPositionPressed = false;
                    _previousPositions.Add(2);
                    returnStr += "\nGot it.";
                    break;
                case "Position three":
                    _waitingOnPositionPressed = false;
                    _previousPositions.Add(3);
                    returnStr += "\nGot it.";
                    break;
                case "Position four":
                    _waitingOnPositionPressed = false;
                    _previousPositions.Add(4);
                    returnStr += "\nGot it.";
                    break;

                default:
                    return "\nI don't understand. Please repeat your response.";
            }

            if (!_waitingOnNumberPressed && !_waitingOnPositionPressed)
            {
                returnStr += "\n\nWhat is the next number shown?";
            }

            return returnStr;
        }

    }
}
