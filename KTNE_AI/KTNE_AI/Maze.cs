using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNE_AI
{
    public class Maze
    {
        private bool _readyPrompt = false;
        private bool _mazeDetermined = false;

        private int _x, _y;
        private Tuple<int, int> _playerPosition;
        private Tuple<int, int> _endPosition;
        private int _endPositionIndex;
        private Tuple<int, int> _firstIndicatorPosition;
        private Tuple<int, int> _secondIndicatorPosition;

        private List<Tuple<Tuple<Tuple<int, int>, Tuple<int, int>>, string>> _mazes;
        private List<Tuple<int, string>> _possibleSolutions;
        private object _solutionLock = new object();

        // Signals to the main method that this module is finished
        public bool Complete { get; private set; }

        public Maze()
        {
            init();
        }

        private void init()
        {
            _mazes = new List<Tuple<Tuple<Tuple<int, int>, Tuple<int, int>>, string>> {
                // 1,5    6,4
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (1,5), new Tuple<int, int>(6,4)),
                "|x x x|x x x||  -     - -||x|x x|x x x||    - - -  ||x|x x|x x x||  -     -  ||x|x x x|x x||  - - - -  ||x x x|x x|x||  -     -  ||x x|x x|x x|"),
                // 2,3    5,5
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (2,3), new Tuple<int, int>(5,5)),
                "|x x x|x x x||-   -     -||x x|x x|x x||  -   - -  ||x|x x|x x x||    -   -  ||x x|x x|x|x||  -   -    ||x|x|x|x x|x||        -  ||x|x x|x x x|"),
                // 4,3    6,3
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (4,3), new Tuple<int, int>(6,3)),
                "|x x x|x|x x||  -        ||x|x|x|x x|x||-     - -  ||x x|x|x x|x||           ||x|x|x|x|x|x||           ||x|x x|x|x|x||  - -      ||x x x x|x x|"),
                // 1,6    1,6
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (1,3), new Tuple<int, int>(1,6)),
                "|x x|x x x x||    - - -  ||x|x|x x x x||      - -  ||x|x x|x x|x||  - -   -  ||x|x x x x x||  - - - -  ||x x x x x|x||  - - -    ||x x x|x x|x|"),
                // 4,1    5,4
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (4,1), new Tuple<int, int>(5,4)),
                "|x x x x x x||- - - -    ||x x x x x|x||  - -   - -||x x|x x|x x||    - -    ||x|x x x|x|x||  - -   -  ||x|x x x x|x||    - - -  ||x|x x x x x|"),
                // 3,2    5,6
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (3,2), new Tuple<int, int>(5,6)),
                "|x|x x|x x x||      -    ||x|x|x|x x|x||        -  ||x x|x|x|x x||  - -     -||x x|x x|x|x||-          ||x x|x|x|x x||  - -   -  ||x x x x|x x|"),
                // 2,1    2,6
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (2,1), new Tuple<int, int>(2,6)),
                "|x x x x|x x||  - -      ||x|x x|x x|x||    - - -  ||x x|x x|x x||- -   -   -||x x|x x x|x||      - -  ||x|x|x x x|x||  - - -    ||x x x x x x|"),
                // 3,3    4,6
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (3,3), new Tuple<int, int>(4,6)),
                "|x|x x x|x x||    -      ||x x x|x x|x||  - - - -  ||x|x x x x|x||    - -    ||x|x x|x x x||  -   - - -||x|x|x x x x||    - - - -||x x x x x x|"),
                // 1,2    3,5
                new Tuple<Tuple<Tuple<int,int>, Tuple<int,int>>, string> (new Tuple<Tuple<int,int>, Tuple<int,int>>(new Tuple<int, int> (1,2), new Tuple<int, int>(3,5)),
                "|x|x x x x x||    - -    ||x|x|x x|x|x||      -    ||x x x|x x|x||  - -   -  ||x|x|x x|x x||      - -  ||x|x|x|x x|x||          -||x x|x x|x x|"),
            };

            _x = 0;
            _y = 0;

            _possibleSolutions = new List<Tuple<int, string>>();
        }

        public string Update(string audioStr)
        {
            // Initial response
            if (!_readyPrompt)
            {
                _readyPrompt = true;
                return "\nReady for Maze. What is the X coordinate for the first indicator?";
            }

            if (!_mazeDetermined)
                return determineMaze(audioStr);

            return "";
        }

        private string determineMaze(string position)
        {
            if (_firstIndicatorPosition == null)
                return getFirstIndicator(position);
            else if (_secondIndicatorPosition == null)
                return getSecondIndicator(position);
            else if (_playerPosition == null)
                return getPlayerPosition(position);
            else
                return getEndPosAndTraverse(position);
        }

        private int determineNum(string position)
        {
            switch (position)
            {
                case "One":
                    return 1;
                case "Two":
                    return 2;
                case "Three":
                    return 3;
                case "Four":
                    return 4;
                case "Five":
                    return 5;
                case "Six":
                    return 6;
                default:
                    return 0;
            }
        }

        private string getFirstIndicator(string position)
        {
            if (_x == 0)
            {
                _x = determineNum(position);

                if (_x != 0)
                    return "\n" + _x.ToString() + "\n\nWhat is the Y coordinate?";
                else
                    return "\nI didn't quite get that.";
            }
            else
            {
                _y = determineNum(position);

                if (_y != 0)
                {
                    _firstIndicatorPosition = new Tuple<int, int>(_x, _y);

                    return eliminateMazes();
                }
                else
                    return "\nI didn't quite get that.";
            }
        }

        private string getSecondIndicator(string position)
        {
            if (_x == 0)
            {
                _x = determineNum(position);

                if (_x != 0)
                    return "\n" + _x.ToString() + "\n\nWhat is the Y coordinate?";
                else
                    return "\nI didn't quite get that.";
            }
            else
            {
                _y = determineNum(position);

                if (_y != 0)
                {
                    _secondIndicatorPosition = new Tuple<int, int>(_x, _y);

                    return eliminateMazes();
                }
                else
                    return "\nI didn't quite get that.";
            }
        }

        private string getPlayerPosition(string position)
        {
            if (_x == 0)
            {
                _x = determineNum(position);

                if (_x != 0)
                    return "\n" + position + "\n\nWhat is the Y coordinate?";
                else
                    return "\nI didn't quite get that.";
            }
            else
            {
                _y = determineNum(position);

                if (_y != 0)
                {
                    _playerPosition = new Tuple<int, int>(_x, _y);
                    _x = 0;
                    _y = 0;
                    return "\n" + position + "\n\nWhat is the ending position beginning with the X coordinate?";
                }
                else
                    return "\nI didn't quite get that.";
            }
        }

        private string getEndPosAndTraverse(string position)
        {
            if (_x == 0)
            {
                _x = determineNum(position);

                if (_x != 0)
                    return "\n" + position + "\n\nWhat is the Y coordinate?";
                else
                    return "\nI didn't quite get that.";
            }
            else
            {
                _y = determineNum(position);

                if (_y != 0)
                {
                    _endPosition = new Tuple<int, int>(_x, _y);
                    _x = 0;
                    _y = 0;
                    return "\n" + position + "\n\n" + traverseMaze();
                }
                else
                    return "\nI didn't quite get that.";
            }
        }

        private string traverseMaze()
        {
            int length = 0;
            int currentPlayerIndex = 156 - (_playerPosition.Item2 * 26) + (_playerPosition.Item1 * 2 - 1);
            _endPositionIndex = 156 - (_endPosition.Item2 * 26) + (_endPosition.Item1 * 2 - 1);
            string path = "";
            Tuple<int, string> shortestSolution = null;

            string maze = _mazes[0].Item2;
              recursiveTraversal(maze, length, currentPlayerIndex, path);

            for (int i = 0; i < _possibleSolutions.Count; i++)
            {
                if (shortestSolution == null)
                    shortestSolution = _possibleSolutions[i];
                else if (shortestSolution.Item1 > _possibleSolutions[i].Item1)
                    shortestSolution = _possibleSolutions[i];
            }

            Complete = true;
            return shortestSolution.Item2;
        }

        // Recursively checks all paths and if theyre a viable solution will add them to _possibleSolutions along with their
        // length. The parameter previousMove prevents us from potential looping back on ourselves
        private void recursiveTraversal(string maze, int length, int currentPlayerIndex, string path, string previousMove = null)
        {
            bool movementPossible = false;

            // Check for end condition
            if (currentPlayerIndex == _endPositionIndex)
            {
                // Add to the list of possible solutions and return
                lock (_solutionLock)
                {
                    _possibleSolutions.Add(new Tuple<int, string>(length, path));
                }

                return;
            }

            // Check if right index exists
            if ((currentPlayerIndex + 2) % 13 <= 9)
            {
                // Check right
                if (maze[currentPlayerIndex + 1] == ' ' && previousMove != "Left")
                {
                    movementPossible = true;
                    recursiveTraversal(maze, length + 1, currentPlayerIndex + 2, path + "Right. ", "Right");
                }
            }
            // Check if left index exists
            if ((currentPlayerIndex - 2) % 13 >= 1)
            {
                // Check left
                if (maze[currentPlayerIndex - 1] == ' ' && previousMove != "Right")
                {
                    movementPossible = true;
                    recursiveTraversal(maze, length + 1, currentPlayerIndex - 2, path + "Left. ", "Left");
                }
            }
            // Check if up index exists
            if (currentPlayerIndex - 26 >= 0)
            {
                // Check up
                if (maze[currentPlayerIndex - 13] == ' ' && previousMove != "Down")
                {
                    movementPossible = true;
                    recursiveTraversal(maze, length + 1, currentPlayerIndex - 26, path + "Up. ", "Up");
                }
            }
            // Check if down index exists
            if (currentPlayerIndex + 26 <= 155)
            {
                // Check down
                if (maze[currentPlayerIndex + 13] == ' ' && previousMove != "Up")
                {
                    movementPossible = true;
                    recursiveTraversal(maze, length + 1, currentPlayerIndex + 26, path + "Down. ", "Down");
                }
            }
            
            // An alternative end condition is hitting a dead end. In which case we just scrap the whole path leading up to it.
            if (!movementPossible)
            {
                return;
            }
        }

        private string eliminateMazes()
        {
            _x = 0;
            _y = 0;

            // Eliminate as many mazes as possible based on the indicator information we already have
            for (int i = 0; i < _mazes.Count; i++)
            {
                bool coordinateCheck = false;

                if (_mazes[i].Item1.Item1.Item1 == _firstIndicatorPosition.Item1)
                {
                    // Check that the Y coordinate also matches
                    if (_mazes[i].Item1.Item1.Item2 == _firstIndicatorPosition.Item2)
                        coordinateCheck = true;
                }
                if (_mazes[i].Item1.Item2.Item1 == _firstIndicatorPosition.Item1)
                {
                    // Check that the Y coordinate also matches
                    if (_mazes[i].Item1.Item2.Item2 == _firstIndicatorPosition.Item2)
                        coordinateCheck = true;
                }

                if (_secondIndicatorPosition != null)
                {
                    if (_mazes[i].Item1.Item1.Item1 == _secondIndicatorPosition.Item1)
                    {
                        // Check that the Y coordinate also matches
                        if (_mazes[i].Item1.Item1.Item2 == _secondIndicatorPosition.Item2)
                            coordinateCheck = true;
                    }
                    if (_mazes[i].Item1.Item2.Item1 == _secondIndicatorPosition.Item1)
                    {
                        // Check that the Y coordinate also matches
                        if (_mazes[i].Item1.Item2.Item2 == _secondIndicatorPosition.Item2)
                            coordinateCheck = true;
                    }
                }

                if (!coordinateCheck)
                {
                    _mazes.RemoveAt(i);
                    i--;
                }
            }

            if (_mazes.Count == 1)
            {
                if (_secondIndicatorPosition == null)
                {
                    _secondIndicatorPosition = new Tuple<int, int>(-1, -1);
                }
                               
                return "\nI've found the correct maze.\n\nWhat is your starting position beginning with the X coordinate?";
            }
            else if (_mazes.Count == 0)
            {
                Complete = true;
                return "\nThere are no possible mazes with the given indicators. Please reset and try again.";
            }
            else
            {
                return "\n" + _firstIndicatorPosition.Item2.ToString() + ".\n\nWhat is the X coordinate for the second indicator?";
            }
        }
    }
}
