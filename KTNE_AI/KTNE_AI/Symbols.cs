using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class Symbols
    {
        private bool _readyPrompt = false;

        private List<SymbolType> _listOne;
        private List<SymbolType> _listTwo;
        private List<SymbolType> _listThree;
        private List<SymbolType> _listFour;
        private List<SymbolType> _listFive;
        private List<SymbolType> _listSix;

        private List<List<SymbolType>> _remainingPossibleLists;
        private List<SymbolType> _userSymbols;

        private enum SymbolType
        {
            Head,
            Pyramid,
            Lambda,
            Lightning,
            AlienWithACane,
            H,
            BackwardsC,
            C,
            BackwardsE,
            CO,
            HollowStar,
            FilledStar,
            UpsideDownQuestion,
            Copyright,
            Nose,
            SmilyFace,
            Candlestick,
            BT,
            BackwardsThree,
            BackwardsP,
            Omega,
            BackwardsN,
            AE,
            Six,
            PoundSign,
            QuestionMark,
            XI
        };

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public Symbols()
        {
            Complete = false;
            Init();
        }

        private void Init()
        {
            _userSymbols = new List<SymbolType>();

            _listOne = new List<SymbolType>
            { SymbolType.Head,
              SymbolType.Pyramid,
              SymbolType.Lambda,
              SymbolType.Lightning,
              SymbolType.AlienWithACane,
              SymbolType.H,
              SymbolType.BackwardsC
            };

            _listTwo = new List<SymbolType>
            {
                SymbolType.BackwardsE,
                SymbolType.Head,
                SymbolType.BackwardsC,
                SymbolType.CO,
                SymbolType.HollowStar,
                SymbolType.H,
                SymbolType.UpsideDownQuestion
            };

            _listThree = new List<SymbolType>
            {
                SymbolType.Copyright,
                SymbolType.Nose,
                SymbolType.CO,
                SymbolType.XI,
                SymbolType.QuestionMark,
                SymbolType.Lambda,
                SymbolType.HollowStar
            };

            _listFour = new List<SymbolType>
            {
                SymbolType.Six,
                SymbolType.BackwardsP,
                SymbolType.BT,
                SymbolType.AlienWithACane,
                SymbolType.XI,
                SymbolType.UpsideDownQuestion,
                SymbolType.SmilyFace
            };

            _listFive = new List<SymbolType>
            {
                SymbolType.Candlestick,
                SymbolType.SmilyFace,
                SymbolType.BT,
                SymbolType.C,
                SymbolType.BackwardsP,
                SymbolType.BackwardsThree,
                SymbolType.FilledStar
            };

            _listSix = new List<SymbolType>
            {
                SymbolType.Six,
                SymbolType.BackwardsE,
                SymbolType.PoundSign,
                SymbolType.AE,
                SymbolType.Candlestick,
                SymbolType.BackwardsN,
                SymbolType.Omega
            };

            _remainingPossibleLists = new List<List<SymbolType>>
            {
                _listOne,
                _listTwo,
                _listThree,
                _listFour,
                _listFive,
                _listSix
            };

        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Symbols.\nWhat is the first symbol?";
            }

            return DetermineOrder(audioStr);
        }

        private string DetermineOrder(string symbol)
        {
            switch (symbol)
            {
                case "Head":
                    _userSymbols.Add(SymbolType.Head);
                    return CheckForMatch();
                case "Pyramid":
                    _userSymbols.Add(SymbolType.Pyramid);
                    return CheckForMatch();
                case "Lambda":
                    _userSymbols.Add(SymbolType.Lambda);
                    return CheckForMatch();
                case "Lightning":
                    _userSymbols.Add(SymbolType.Lightning);
                    return CheckForMatch();
                case "Alien with a cane":
                    _userSymbols.Add(SymbolType.AlienWithACane);
                    return CheckForMatch();
                case "H":
                    _userSymbols.Add(SymbolType.H);
                    return CheckForMatch();
                case "Backwards C":
                    _userSymbols.Add(SymbolType.BackwardsC);
                    return CheckForMatch();
                case "C":
                    _userSymbols.Add(SymbolType.C);
                    return CheckForMatch();
                case "Backwards E":
                    _userSymbols.Add(SymbolType.BackwardsE);
                    return CheckForMatch();
                case "C O":
                    _userSymbols.Add(SymbolType.CO);
                    return CheckForMatch();
                case "Hollow star":
                    _userSymbols.Add(SymbolType.HollowStar);
                    return CheckForMatch();
                case "Filled star":
                    _userSymbols.Add(SymbolType.FilledStar);
                    return CheckForMatch();
                case "Upside down question mark":
                    _userSymbols.Add(SymbolType.UpsideDownQuestion);
                    return CheckForMatch();
                case "Copyright":
                    _userSymbols.Add(SymbolType.Copyright);
                    return CheckForMatch();
                case "Nose":
                    _userSymbols.Add(SymbolType.Nose);
                    return CheckForMatch();
                case "Smiley face":
                    _userSymbols.Add(SymbolType.SmilyFace);
                    return CheckForMatch();
                case "Candlestick":
                    _userSymbols.Add(SymbolType.Candlestick);
                    return CheckForMatch();
                case "B T":
                    _userSymbols.Add(SymbolType.BT);
                    return CheckForMatch();
                case "Backwards three":
                    _userSymbols.Add(SymbolType.BackwardsThree);
                    return CheckForMatch();
                case "Backwards P":
                    _userSymbols.Add(SymbolType.BackwardsP);
                    return CheckForMatch();
                case "Omega":
                    _userSymbols.Add(SymbolType.Omega);
                    return CheckForMatch();
                case "Backwards N":
                    _userSymbols.Add(SymbolType.BackwardsN);
                    return CheckForMatch();
                case "A E":
                    _userSymbols.Add(SymbolType.AE);
                    return CheckForMatch();
                case "Six":
                    _userSymbols.Add(SymbolType.Six);
                    return CheckForMatch();
                case "Pound sign":
                    _userSymbols.Add(SymbolType.PoundSign);
                    return CheckForMatch();
                case "Question mark":
                    _userSymbols.Add(SymbolType.QuestionMark);
                    return CheckForMatch();
                case "X I":
                    _userSymbols.Add(SymbolType.XI);
                    return CheckForMatch();
                default:
                    return "I don't recognize that symbol.\nPlease repeat it.";
            }
        }

        private string CheckForMatch()
        {
            for (int i = 0; i < _userSymbols.Count; i++)
            {
                int numListRemaining = _remainingPossibleLists.Count;
                for (int j = 0; j < numListRemaining; j++)
                {
                    if (!_remainingPossibleLists[j].Contains(_userSymbols[i]))
                    {
                        _remainingPossibleLists.Remove(_remainingPossibleLists[j]);

                        if (_remainingPossibleLists.Count > 1)
                        {
                            j--;
                            numListRemaining--;
                        }
                        else
                            break;
                    }
                }
            }

            if (_remainingPossibleLists.Count == 1 && _userSymbols.Count == 4)
            {
                List<SymbolType> answerList = _remainingPossibleLists[0];
                string symbolOrder = "Press in this order.\n\n";

                foreach (SymbolType s in answerList)
                {
                    if (_userSymbols.Contains(s))
                    {
                        switch (s)
                        {
                            case SymbolType.Head:
                                symbolOrder += "Head.\n";
                                break;
                            case SymbolType.Pyramid:
                                symbolOrder += "Pyramid.\n";
                                break;
                            case SymbolType.Lambda:
                                symbolOrder += "Lambda.\n";
                                break;
                            case SymbolType.Lightning:
                                symbolOrder += "Lightning.\n";
                                break;
                            case SymbolType.AlienWithACane:
                                symbolOrder += "Alien with a cane.\n";
                                break;
                            case SymbolType.H:
                                symbolOrder += "H.\n";
                                break;
                            case SymbolType.BackwardsC:
                                symbolOrder += "Backwards C.\n";
                                break;
                            case SymbolType.C:
                                symbolOrder += "C.\n";
                                break;
                            case SymbolType.BackwardsE:
                                symbolOrder += "Backwards E.\n";
                                break;
                            case SymbolType.CO:
                                symbolOrder += "C O.\n";
                                break;
                            case SymbolType.HollowStar:
                                symbolOrder += "Hollow star.\n";
                                break;
                            case SymbolType.FilledStar:
                                symbolOrder += "Filled star.\n";
                                break;
                            case SymbolType.UpsideDownQuestion:
                                symbolOrder += "Upside down question mark.\n";
                                break;
                            case SymbolType.Copyright:
                                symbolOrder += "Copyright.\n";
                                break;
                            case SymbolType.Nose:
                                symbolOrder += "Nose.\n";
                                break;
                            case SymbolType.SmilyFace:
                                symbolOrder += "Smiley face.\n";
                                break;
                            case SymbolType.Candlestick:
                                symbolOrder += "Candlestick.\n";
                                break;
                            case SymbolType.BT:
                                symbolOrder += "B T.\n";
                                break;
                            case SymbolType.BackwardsThree:
                                symbolOrder += "Backwards three.\n";
                                break;
                            case SymbolType.BackwardsP:
                                symbolOrder += "Backwards P.\n";
                                break;
                            case SymbolType.Omega:
                                symbolOrder += "Omega.\n";
                                break;
                            case SymbolType.BackwardsN:
                                symbolOrder += "Backwards N.\n";
                                break;
                            case SymbolType.AE:
                                symbolOrder += "A E.\n";
                                break;
                            case SymbolType.Six:
                                symbolOrder += "Six.\n";
                                break;
                            case SymbolType.PoundSign:
                                symbolOrder += "Pound sign.\n";
                                break;
                            case SymbolType.QuestionMark:
                                symbolOrder += "Question mark.\n";
                                break;
                            case SymbolType.XI:
                                symbolOrder += "X I.\n";
                                break;
                        }
                    }
                }

                Complete = true;
                return symbolOrder;
            }
            else
                return "Next symbol.";
        }
    }
}
