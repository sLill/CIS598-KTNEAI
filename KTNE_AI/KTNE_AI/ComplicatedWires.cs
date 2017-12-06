using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class ComplicatedWires
    {
        private bool _readyPrompt = false;
        private bool? _lastDigitEven = null;
        private bool? _parallelPort = null;
        private bool? _twoOrMoreBatteries = null;

        private Wire _currentWire;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        private enum Code
        {
            C, // Cut
            D, // Do not cut
            S, // Cut if last digit of the serial number is even
            P, // Cut if there's a parallel port
            B // Cut if there are two or more batteries
        }

        public ComplicatedWires()
        {
            Complete = false;
            _currentWire = new Wire();
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Complicated Wires.\n\nIs the last digit of the serial number even?";
            }

            if (audioStr == "Finished")
            {
                Complete = true;
                return "";
            }

            // Poll the user for the basic information needed to proceed
            if (_twoOrMoreBatteries == null)
                return initialResponses(audioStr);
            else
            {
                return determineWireProperties(audioStr);
            }
        }

        private string initialResponses (string audioStr)
        {
            if (_lastDigitEven == null)
            {
                if (audioStr == "Yes")
                {
                    _lastDigitEven = true;
                    return "\nIs there a parallel port?";
                }
                else if (audioStr == "No")
                {
                    _lastDigitEven = false;
                    return "\nIs there a parallel port?";
                }
                else
                {
                    return "\nI don't understand. Please repeat your answer.";
                }
            }
            else if (_parallelPort == null)
            {
                if (audioStr == "Yes")
                {
                    _parallelPort = true;
                    return "\nIs there two or more batteries on the side of the bomb?";
                }
                else if (audioStr == "No")
                {
                    _parallelPort = false;
                    return "\nIs there two or more batteries on the side of the bomb?";
                }
                else
                {
                    return "\nI don't understand. Please repeat your answer.";
                }
            }
            else
            {
                if (audioStr == "Yes")
                {
                    _twoOrMoreBatteries = true;
                    return "\nBegin describing the first wire. Say 'Complete' when you're finished";
                }
                else if (audioStr == "No")
                {
                    _twoOrMoreBatteries = false;
                    return "\nBegin describing the first wire. Say 'Complete' when you're finished";
                }
                else
                {
                    return "\nI don't understand. Please repeat your answer.";
                }
            }
        }

        private string determineWireProperties (string audioStr)
        {
            if (audioStr == "Complete")
            {
                string action = determineAction() + "\n\nReady for the next wire.";
                _currentWire = new Wire();

                return action;
            }

            if (audioStr == "Blue")
            {
                _currentWire.isBlue = true;
                return "\nBlue";
            }
            else if (audioStr == "Red")
            {
                _currentWire.isRed = true;
                return "\nRed";
            }
            else if (audioStr == "Star")
            {
                _currentWire.hasStar = true;
                return "\nStar";
            }
            else if (audioStr == "L E D" || audioStr == "Light")
            {
                _currentWire.lightIsOn = true; 
                return audioStr;
            }
            else return "\nI don't understand. Could you repeat that?";
        }

        private string determineAction()
        {
            switch (determineCode())
            {
                case Code.C:
                    return "\nCut the wire.";
                case Code.D:
                    return "\nDo not cut the wire.";
                case Code.S:
                    if (_lastDigitEven == true)
                        return "\nCut the wire.";
                    else
                        return "\nDo not cut the wire.";
                case Code.P:
                    if (_parallelPort == true)
                        return "\nCut the wire.";
                    else
                        return "\nDo not cut the wire.";
                case Code.B:
                    if (_twoOrMoreBatteries == true)
                        return "\nCut the wire.";
                    else
                        return "\nDo not cut the wire.";
                default:
                    return "Error";
            }
        }

        private Code determineCode()
        {
            if (_currentWire.isBlue) // Blue
            {
                if (_currentWire.isRed) // Blue Red
                {
                    if (_currentWire.hasStar) // Blue Red Star
                    {
                        if (_currentWire.lightIsOn) // Blue Red Star LED
                        {
                            return Code.D;
                        }
                        else
                        {
                            return Code.C;
                        }
                    }
                    else if (_currentWire.lightIsOn) // Blue Red LED
                    {
                        return Code.S;
                    }
                    else // Just Blue Red
                    {
                        return Code.S;
                    }
                }
                else if (_currentWire.hasStar) // Blue Star
                {
                    if (_currentWire.lightIsOn) // Blue Star LED
                    {
                        return Code.P;
                    }
                    else // Just Blue Star
                    {
                        return Code.D;
                    }
                }
                else if (_currentWire.lightIsOn) // Blue LED
                {
                    return Code.P;
                }
                else // Just Blue
                {
                    return Code.S;
                }
            }
            else if (_currentWire.isRed) // Red
            {
                if (_currentWire.hasStar) // Red Star
                {
                    if (_currentWire.lightIsOn) // Red Star LED
                    {
                        return Code.B;
                    }
                    else // Just Red Star
                    {
                        return Code.C;
                    }
                }
                else if (_currentWire.lightIsOn) // Red LED
                {
                    return Code.B;
                }
                else // Just Red 
                {
                    return Code.S;
                }
            }
            else if (_currentWire.hasStar) // Star
            {
                if (_currentWire.lightIsOn) // Star LED
                {
                    return Code.B;
                }
                else // Just Star
                {
                    return Code.C;
                }
            }
            else if (_currentWire.lightIsOn) // LED
            {
                return Code.D;
            }
            else
            {
                return Code.C; // None
            }
        }

        private class Wire
        {
           public bool isBlue = false;
           public bool isRed = false;
           public bool hasStar = false;
           public bool lightIsOn = false;
        }
    }
}
