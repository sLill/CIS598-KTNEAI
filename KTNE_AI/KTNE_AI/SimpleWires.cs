using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class SimpleWires
    {
        private bool _readyPrompt = false;

        private int _numOfWires = -1;

        // ThreeTree
        private bool _threePrompt = false;
        private bool? _lastWireYellow = null;
        private bool? _anyRedWires = null;
        private bool? _lastWireWhite = null;

        // FourTree
        private bool _fourPrompt = false;
        private bool? _noRedWires = null;
        private bool? _moreThanOneRed = null;
        private bool? _exactlyOneBlue = null;

        // FiveTree
        private bool _fivePrompt = false;
        private bool? _lastWireBlack = null;
        private bool? _exactlyOneRed = null;

        // SixTree
        private bool _sixPrompt = false;
        private bool? _anyYellowWires = null;
        private bool? _exactlyOneYellow = null;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public SimpleWires()
        {
            Complete = false;
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Simple Wires.\nHow many wires?";
            }
            else if (_numOfWires == -1)
            {
                if (!DetermineWireCount(audioStr))
                {
                    return "\nI don't understand. Please repeat the amount.";
                }
            }

            switch (_numOfWires)
            {
                case 3:
                    return ThreeTree(audioStr);
                case 4:
                    return FourTree(audioStr);
                case 5:
                    return FiveTree(audioStr);
                case 6:
                    return SixTree(audioStr);
            }

            // Should never reach this line
            return "Error";
        }

        /// <summary>
        /// Returns false if passed and unrecognized amount
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool DetermineWireCount(string amount)
        {
            switch (amount)
            {
                case "Three":
                case "Definitely three":
                    _numOfWires = 3;
                    return true;
                case "Four":
                case "Definitely four":
                    _numOfWires = 4;
                    return true;
                case "Five":
                case "Definitely five":
                    _numOfWires = 5;
                    return true;
                case "Six":
                case "Definitely six":
                    _numOfWires = 6;
                    return true;
                default:
                    return false;
            }
        }

        // Question tree for 3 wires
        private string ThreeTree(string str)
        {
            if (!_threePrompt && str != "Yes" && str != "No")
            {
                _threePrompt = true;
                return "Are there any red wires?";
            }

            if (_anyRedWires == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _anyRedWires = true;
                        return "Is the last wire white?";
                    case "No":
                    case "Definitely no":
                        Complete = true;
                        return "Cut the second wire";
                }
            }
            else if (_lastWireWhite == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the last wire.";
                    case "No":
                    case "Definitely no":
                        _lastWireWhite = false;
                        return "Is there more than one blue wire?";
                }
            }
            else
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the last blue wire.";
                    case "No":
                    case "Definitely no":
                        Complete = true;
                        return "Cut the last wire";
                }
            }

            // Should never reach this line
            return "Error";
        }

        // Question tree for 4 wires
        private string FourTree(string str)
        {
            if (!_fourPrompt && str != "Yes" && str != "No")
            {
                _fourPrompt = true;
                return "Is there more than one red wire?";
            }

            if (_moreThanOneRed == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _moreThanOneRed = true;
                        return "Is the last digit of the serial number odd?";
                    case "No":
                    case "Definitely no":
                        _moreThanOneRed = false;
                        return "Is the last wire yellow?";
                }
            }
            else if (_moreThanOneRed == true)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the last red wire.";
                    case "No":
                    case "Definitely no":
                        _moreThanOneRed = false;
                        return "Is the last wire yellow?";
                }
            }
            else if (_lastWireYellow == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _lastWireYellow = true;
                        return "Is there any red wires?";
                    case "No":
                    case "Definitely no":
                        _lastWireYellow = false;
                        return "Is there exactly one blue wire?";
                }
            }
            else if (_lastWireYellow == true)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _lastWireYellow = false;
                        return "Is there exactly one blue wire?";
                    case "No":
                    case "Definitely no":
                        Complete = true;
                        return "Cut the first wire";
                }
            }
            else if (_exactlyOneBlue == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the first wire.";
                    case "No":
                    case "Definitely no":
                        _exactlyOneBlue = false;
                        return "Is there more than one yellow wire?";
                }
            }
            else
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the last wire.";
                    case "No":
                    case "Definitely no":
                        Complete = true;
                        return "Cut the second wire.";
                }
            }

            // Should never reach this line
            return "Error";
        }

        // Question tree for 5 wires
        private string FiveTree(string str)
        {
            if (!_fivePrompt && str != "Yes" && str != "No")
            {
                _fivePrompt = true;
                return "Is the last wire black?";
            }

            if (_lastWireBlack == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _lastWireBlack = true;
                        return "Is the last digit of the serial number odd?";
                    case "No":
                    case "Definitely no":
                        _lastWireBlack = false;
                        return "Is there exactly one red wire?";
                }
            }
            else if (_lastWireBlack == true)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the fourth wire.";
                    case "No":
                    case "Definitely no":
                        _lastWireBlack = false;
                        return "Is there exactly one red wire?";
                }
            }
            else if (_exactlyOneRed == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _exactlyOneRed = true;
                        return "Is there more than one yellow wire?";
                    case "No":
                    case "Definitely no":
                        _exactlyOneRed = false;
                        return "Are there any black wires?";
                }
            }
            else if (_exactlyOneRed == true)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the first wire.";
                    case "No":
                    case "Definitely no":
                        _exactlyOneRed = false;
                        return "Are there any black wires?";
                }
            }
            else
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the first wire.";
                    case "No":
                    case "Definitely no":
                        Complete = true;
                        return "Cut the second wire.";
                }
            }

            // Should never reach this line
            return "Error";
        }

        // Question tree for 6 wires
        private string SixTree(string str)
        {
            if (!_sixPrompt && str != "Yes" && str != "No")
            {
                _sixPrompt = true;
                return "Are there any yellow wires?";
            }

            if (_anyYellowWires == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _anyYellowWires = true;
                        return "Is there exactly one yellow wire?";
                    case "No":
                    case "Definitely no":
                        _anyYellowWires = false;
                        return "Is the last digit of the serial number odd?";
                }
            }
            else if (_anyYellowWires == false)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the third wire.";
                    case "No":
                    case "Definitely no":
                        _anyYellowWires = true;
                        return "Is there exactly one yellow wire?";
                }
            }
            else if (_exactlyOneYellow == null)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        _exactlyOneYellow = true;
                        return "Is there more than one white wire?";
                    case "No":
                    case "Definitely no":
                        _exactlyOneYellow = false;
                        return "Are there any red wires?";
                }
            }
            else if (_exactlyOneYellow == true)
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the fourth wire.";
                    case "No":
                    case "Definitely no":
                        _exactlyOneYellow = false;
                        return "Are there any red wires?";
                }
            }
            else
            {
                switch (str)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Cut the fourth wire.";
                    case "No":
                    case "Definitely no":
                        Complete = true;
                        return "Cut the last wire.";
                }
            }

            // Should never reach this line
            return "Error";
        }
    }
}
