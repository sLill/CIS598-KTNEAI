using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class WordCalculator
    {
        private bool _readyPrompt = false;
        private bool _nextSet = false;

        private bool _displayWordDetermined = false;
        private string _possibilities = "";

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public WordCalculator()
        {
            Complete = false;
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Word Calculator. Spell out the word displayed.";
            }

            if (audioStr == "Repeat")
            {
                return _possibilities;
            }
            else if (audioStr == "Finished")
            {
                Complete = true;
                return "Okay.";
            }
            else if (audioStr == "Got it")
                _nextSet = true;

            if(_nextSet)
            {
                _nextSet = false;
                _possibilities = "";
                return "\nSpell out the next word.";
            }

            // Determine what is being displayed and ask the user for the corresponding word
            if (!_displayWordDetermined)
                return determineDisplayWord(audioStr);
            else
                return readOffWordList(audioStr);
        }

        private string determineDisplayWord(string displayWord)
        {
            _displayWordDetermined = true;
            switch (displayWord)
            {
                case "Y E S":
                    return "What is the word in column 1, row 2?"; 
                case "F I R S T":
                    return "What is the word in column 2, row 1?";
                case "D I S P L A Y":
                    return "What is the word in column 2, row 3?";
                case "O K A Y":
                    return "What is the word in column 2, row 1?";
                case "S A Y S":
                    return "What is the word in column 2, row 3?";
                case "N O T H I N G":
                    return "What is the word in column 1, row 2?";
                case "B L A N K":
                    return "What is the word in column 2, row 2?";
                case "N O":
                    return "What is the word in column 2, row 3?";
                case "L E D":
                    return "What is the word in column 1, row 2?";
                case "L E A D":
                    return "What is the word in column 2, row 3?";
                case "R E A D":
                    return "What is the word in column 2, row 2?";
                case "R E D":
                    return "What is the word in column 2, row 2?";
                case "R E E D":
                    return "What is the word in column 1, row 3?";
                case "L E E D":
                    return "What is the word in column 1, row 3?";
                case "H O L D O N":
                    return "What is the word in column 2, row 3?";
                case "Y O U":
                    return "What is the word in column 2, row 2?";
                case "Y O U A R E":
                    return "What is the word in column 2, row 3?";
                case "Y O U R":
                    return "What is the word in column 2, row 2?";
                case "Y O U R E":
                    return "What is the word in column 2, row 2?";
                case "U R":
                    return "What is the word in column 1, row 1?";
                case "T H E R E":
                    return "What is the word in column 2, row 3";
                case "T H E Y R E":
                    return "What is the word in column 1, row 3?";
                case "T H E I R":
                    return "What is the word in column 2, row 2?";
                case "T H E Y A R E":
                    return "What is the word in column 1, row 2?";
                case "S E E":
                    return "What is the word in column 2, row 3?";
                case "C":
                    return "What is the word in column 2, row 1?";
                case "C E E":
                    return "What is the word in column 2, row 3?";
                case "Blank":
                    return "What is the word in column 1, row 3?";
                default:
                    _displayWordDetermined = false;
                    return "I don't recognize that word.";
            }
        }

        private string readOffWordList(string positionWord)
        {
            _displayWordDetermined = false;
            _possibilities = "\nPress the first key to correspond to one of the following words...\n";
            switch (positionWord)
            {
                case "R E A D Y":
                    return _possibilities += "YES, OKAY, WHAT, MIDDLE, LEFT, PRESS, RIGHT, BLANK, READY, NO, FIRST, UHHH, NOTHING, WAIT";
                case "F I R S T":
                    return _possibilities += "LEFT, OKAY, YES, MIDDLE, NO, RIGHT, NOTHING, UHHH, WAIT, READY, BLANK, WHAT, PRESS, FIRST";
                case "N O":
                    return _possibilities += "BLANK, UHHH, WAIT, FIRST, WHAT, READY, RIGHT, YES, NOTHING, LEFT, PRESS, OKAY, NO, MIDDLE";
                case "B L A N K":
                    return _possibilities += "WAIT, RIGHT, OKAY, MIDDLE, BLANK, PRESS, READY, NOTHING, NO, WHAT, LEFT, UHHH, YES, FIRST";
                case "N O T H I N G":
                    return _possibilities += "UHHH, RIGHT, OKAY, MIDDLE, YES, BLANK, NO, PRESS, LEFT, WHAT, WAIT, FIRST, NOTHING, READY";
                case "Y E S":
                    return _possibilities += "OKAY, RIGHT, UHHH, MIDDLE, FIRST, WHAT, PRESS, READY, NOTHING, YES, LEFT, BLANK, NO, WAIT";
                case "W H A T":
                    return _possibilities += "UHHH, WHAT, LEFT, NOTHING, READY, BLANK, MIDDLE, NO, OKAY, FIRST, WAIT, YES, PRESS, RIGHT";
                case "U H H H":
                    return _possibilities += "READY, NOTHING, LEFT, WHAT, OKAY, YES, RIGHT, NO, PRESS, BLANK, UHHH, MIDDLE, WAIT, FIRST";
                case "L E F T":
                    return _possibilities += "RIGHT, LEFT, FIRST, NO, MIDDLE, YES, BLANK, WHAT, UHHH, WAIT, PRESS, READY, OKAY, NOTHING";
                case "R I G H T":
                    return _possibilities += "YES, NOTHING, READY, PRESS, NO, WAIT, WHAT, RIGHT, MIDDLE, LEFT, UHHH, BLANK, OKAY, FIRST";
                case "M I D D L E":
                    return _possibilities += "BLANK, READY, OKAY, WHAT, NOTHING, PRESS, NO, WAIT, LEFT, MIDDLE, RIGHT, FIRST, UHHH, YES";
                case "O K A Y":
                    return _possibilities += "MIDDLE, NO, FIRST, YES, UHHH, NOTHING, WAIT, OKAY, LEFT, READY, BLANK, PRESS, WHAT, RIGHT";
                case "W A I T":
                    return _possibilities += "UHHH, NO, BLANK, OKAY, YES, LEFT, FIRST, PRESS, WHAT, WAIT, NOTHING, READY, RIGHT, MIDDLE";
                case "P R E S S":
                    return _possibilities += "RIGHT, MIDDLE, YES, READY, PRESS, OKAY, NOTHING, UHHH, BLANK, LEFT, FIRST, WHAT, NO, WAIT";
                case "Y O U":
                    return _possibilities += "SURE, YOU ARE, YOUR, YOU'RE, NEXT, UH HUH, UR, HOLD, WHAT?, YOU, UH UH, LIKE, DONE, U";
                case "Y O U A R E":
                    return _possibilities += "YOUR, NEXT, LIKE, UH HUH, WHAT?, DONE, UH UH, HOLD, YOU, U, YOU'RE, SURE, UR, YOU ARE";
                case "Y O U R":
                    return _possibilities += "UH UH, YOU ARE, UH HUH, YOUR, NEXT, UR, SURE, U, YOU'RE, YOU, WHAT?, HOLD, LIKE, DONE";
                case "Y O U R E":
                    return _possibilities += "YOU, YOU'RE, UR, NEXT, UH UH, YOU ARE, U, YOUR, WHAT?, UH HUH, SURE, DONE, LIKE, HOLD";
                case "U R":
                    return _possibilities += "DONE, U, UR, UH HUH, WHAT?, SURE, YOUR, HOLD, YOU'RE, LIKE, NEXT, UH UH, YOU ARE, YOU";
                case "U":
                    return _possibilities += "UH HUH, SURE, NEXT, WHAT?, YOU'RE, UR, UH UH, DONE, U, YOU, LIKE, HOLD, YOU ARE, YOUR";
                case "U H H U H":
                    return _possibilities += "UH HUH, YOUR, YOU ARE, YOU, DONE, HOLD, UH UH, NEXT, SURE, LIKE, YOU'RE, UR, U, WHAT?";
                case "U H U H":
                    return _possibilities += "UR, U, YOU ARE, YOU'RE, NEXT, UH UH, DONE, YOU, UH HUH, LIKE, YOUR, SURE, HOLD, WHAT?";
                case "W H A T ?":
                    return _possibilities += "YOU, HOLD, YOU'RE, YOUR, U, DONE, UH UH, LIKE, YOU ARE, UH HUH, UR, NEXT, WHAT?, SURE";
                case "D O N E":
                    return _possibilities += "SURE, UH HUH, NEXT, WHAT?, YOUR, UR, YOU'RE, HOLD, LIKE, YOU, U, YOU ARE, UH UH, DONE";
                case "N E X T":
                    return _possibilities += "WHAT?, UH HUH, UH UH, YOUR, HOLD, SURE, NEXT, LIKE, DONE, YOU ARE, UR, YOU'RE, U, YOU";
                case "H O L D":
                    return _possibilities += "YOU ARE, U, DONE, UH UH, YOU, UR, SURE, WHAT?, YOU'RE, NEXT, HOLD, UH HUH, YOUR, LIKE";
                case "S U R E":
                    return _possibilities += "YOU ARE, DONE, LIKE, YOU'RE, YOU, HOLD, UH HUH, UR, SURE, U, WHAT?, NEXT, YOUR, UH UH";
                case "L I K E":
                    return _possibilities += "YOU'RE, NEXT, U, UR, HOLD, DONE, UH UH, WHAT?, UH HUH, YOU, LIKE, SURE, YOU ARE, YOUR";
                //case "Blank":
                //    return _possibilities += "";
                default:
                    return "I don't recognize that word.";
            }
        }
    }
}
