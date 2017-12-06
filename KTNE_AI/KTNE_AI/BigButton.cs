using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class BigButton
    {
        private bool _readyPrompt = false;

        private bool? _isButtonBlue = null;
        private bool? _moreThanOneBattery = null;
        private bool? _isButtonWhite = null;
        private bool? _moreThanTwoBatteries = null;
        private bool? _isButtonYellow = null;
        private bool? _isButtonRed = null;
        private bool? _saysAbort = null;
        private bool? _indicatorWithCAR = null;

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public BigButton()
        {
            Complete = false;
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Big Button.\nIs the button blue?";
            }

            // Begin navigating the question tree
            if (_isButtonBlue == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _isButtonBlue = true;
                        return "Does the button say abort?";
                    case "No":
                    case "Definitely no":
                        _isButtonBlue = false;
                        return "Is there more than 1 battery on the bomb?";
                }
            }
            else if (_isButtonBlue == true)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _isButtonBlue = false;
                        _saysAbort = true;
                        return "Hold the button. What color is the light strip?";
                    case "No":
                    case "Definitely no":
                        _isButtonBlue = false;
                        return "Is there more than 1 battery on the bomb?";
                }

            }
            else if (_saysAbort == true)
            {
                return ReleasingHeldButton(audioStr);
            }
            else if (_moreThanOneBattery == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _moreThanOneBattery = true;
                        return "Does the button say detonate?";
                    case "No":
                    case "Definitely no":
                        _moreThanOneBattery = false;
                        return "Is the button white?";
                }
            }
            else if (_moreThanOneBattery == true)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Press and immediately release the button.";
                    case "No":
                    case "Definitely no":
                        _moreThanOneBattery = false;
                        return "Is the button white?";
                }
            }
            else if (_isButtonWhite == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _isButtonWhite = true;
                        return "Is there a lit indicator with the label C.A.R.?";
                    case "No":
                    case "Definitely no":
                        _isButtonWhite = false;
                        return "Is there 2 or more batteries on the side of the bomb?";
                }
            }
            else if (_isButtonWhite == true)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _isButtonWhite = false;
                        _indicatorWithCAR = true;
                        return "Hold the button. What color is the light strip?";
                    case "No":
                    case "Definitely no":
                        _isButtonWhite = false;
                        return "Is there 2 or more batteries on the side of the bomb?";
                }
            }
            else if (_indicatorWithCAR == true)
            {
                return ReleasingHeldButton(audioStr);
            }
            else if (_moreThanTwoBatteries == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _moreThanTwoBatteries = true;
                        return "Is there a lit indicator with the label F.R.K.?";
                    case "No":
                    case "Definitely no":
                        _moreThanTwoBatteries = false;
                        return "Is the button yellow?";
                }
            }
            else if (_moreThanTwoBatteries == true)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Press and immediately release the button.";
                    case "No":
                    case "Definitely no":
                        _moreThanTwoBatteries = false;
                        return "Is the button yellow?";
                }
            }
            else if (_isButtonYellow == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _isButtonYellow = true;
                        return "Hold the button. What color is the light strip?";
                    case "No":
                    case "Definitely no":
                        _isButtonYellow = false;
                        return "Is the button red?";
                }
            }
            else if (_isButtonYellow == true)
            {
                return ReleasingHeldButton(audioStr);
            }
            else if (_isButtonRed == null)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        _isButtonRed = true;
                        return "Does the button say hold?";
                    case "No":
                    case "Definitely no":
                        _isButtonRed = false;
                        return "Hold the button. What color is the light strip?";
                }
            }
            else if (_isButtonRed == true)
            {
                switch (audioStr)
                {
                    case "Yes":
                    case "Definitely yes":
                        Complete = true;
                        return "Press and immediately release the button.";
                    case "No":
                    case "Definitely no":
                        _isButtonRed = false;
                        return "Hold the button. What color is the light strip?";
                }
            }
            else
            {
                return ReleasingHeldButton(audioStr);
            }

            // Should never reach this line
            return "Error";
        }

        public string ReleasingHeldButton(string str)
        {
            Complete = true;
            switch (str)
            {
                case "Blue":
                    return "Release the button when the timer has a 4 in any position.";
                case "White":
                    return "Release the button when the timer has a 1 in any position.";
                case "Yellow":
                    return "Release the button when the timer has a 5 in any position.";
                case "Red":
                    return "Release the button when the timer has a 1 in any position.";
            }

            // Should never reach this line
            return "Error";
        }
    }
}
