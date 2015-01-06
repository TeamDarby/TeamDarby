namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class GameBoard
    {
        #region Constants

        public const int BoardWidth = 25;
        public const int BoardHeight = 8;        
        public const int Rows = 5;
        public const int Cols = 10;

        private const char Zero = '0';
        private const char Dash = '-';
        private const char Blank = ' ';
        private const char Nine = '9';
        private const char Four = '4';
        private const char VerticalBar = '|';


        #endregion

        #region Fields

        public char[,] gameBoard = new char[BoardWidth, BoardHeight];
        public int[,] newGameBoard = new int[Rows, Cols];
        private int shoots = 0;
        private int balloons = 50;

        #endregion

        #region Properties

        public int Shoots
        {
            get
            {
                return this.shoots;
            }

            private set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentOutOfRangeException("shoots", "Shoots cant be negative or more than all possible balloons!");
                }

                this.shoots = value;
            }
        }

        public int Baloons
        {
            get
            {
                return this.balloons;
            }

            private set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentOutOfRangeException("balloons", "Balloons cant be negative or more than all possible balloons!");
                }

                this.balloons = value;
            }
        }

        #endregion


        public void GenerateGameBoard()
        {
            Shoots = 50;

            Random random = new Random();
            Coordinates coords = new Coordinates();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    coords.X = j;
                    coords.Y = i;


                    AddBaloon(coords, (random.Next(1, 5)));
                }
            }
        }


        /// <summary>
        /// Method that start the game again and position all elements on the board
        /// </summary>
        public void GenerateNewGame()
        {

            Shoots = 50;
            //FillBlankGameBoard();
            GenerateGameBoard();
            /*Random random = new Random();
            Coordinates coords = new Coordinates();


            for (int i = 0; i < BoardWidth; i++)
            {
                for (int j = 0; j < BoardHeight; j++)
                {
                    coords.X = i;
                    coords.Y = j;


                    AddNewBaloonToGameBoard(coords, (char)(random.Next(1, 5) + (int)Zero));
                }
            }*/

            
        }

        /// <summary>
        /// This method add new baloons to the gameboard on the selected coordinates
        /// </summary>
        /// <param name="coordinate">The current coordinates of the player</param>
        /// <param name="value"></param>
        private void AddNewBaloonToGameBoard(Coordinates coordinate, char value)
        {
            int xPosition, yPosition;
            xPosition = 4 + coordinate.X * 2;
            yPosition = 2 + coordinate.Y;
            gameBoard[xPosition, yPosition] = value;
        }

        private void AddBaloon(Coordinates coordinate, int value)
        {
            int xPosition, yPosition;
            xPosition = coordinate.X;
            yPosition = coordinate.Y;
            newGameBoard[yPosition, xPosition] = value;
        }





        /// <summary>
        /// This method gets the current coordinates
        /// </summary>
        /// <param name="coordinate">The current coordinates</param>
        /// <returns>Returns the current position of the player on the gameboard</returns>
        private char get(Coordinates coordinate)
        {
            int xPosition, yPosition;
            if (coordinate.X < 0 || coordinate.Y < 0 || coordinate.X > 9 || coordinate.Y > 4)
            {
                return 'e';
            }

            xPosition = 4 + coordinate.X * 2;
            yPosition = 2 + coordinate.Y;
            return gameBoard[xPosition, yPosition];
        }

        private int FindPosition(Coordinates coordinate)
        {
            int xPosition, yPosition;
            if (coordinate.X < 0 || coordinate.Y < 0 || coordinate.X > 9 || coordinate.Y > 4)
            {
                return 'e';
            }

            xPosition = coordinate.X;
            yPosition = coordinate.Y;
            return newGameBoard[yPosition, xPosition];
        }

        /// <summary>
        /// Fill the blank fields on the gameboard
        /// </summary>
       /* private void FillBlankGameBoard()
        {

            //printing blank spaces
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 25; j++)
                {

                    gameBoard[j, i] = ' ';
                }
            }

            //printing firs row
            for (int i = 0; i < 4; i++)
            {
                gameBoard[i, 0] = ' ';
            }

            char numOfShootser = Zero;


            for (int i = 4; i < 25; i++)
            {
                if ((i % 2 == 0) && numOfShootser <= '9')
                {
                    gameBoard[i, 0] = (char)numOfShootser++;
                }
                else
                {
                    gameBoard[i, 0] = ' ';
                }
            }

            //printing second row
            for (int i = 3; i < 24; i++)
            {
                gameBoard[i, 1] = '-';
            }


            //printing left game board wall
            numOfShootser = '0';

            for (int i = 2; i < 8; i++)
            {
                if (numOfShootser <= '4')
                {
                    gameBoard[0, i] = numOfShootser++;
                    gameBoard[1, i] = ' ';


                    gameBoard[2, i] = '|';
                    gameBoard[3, i] = ' ';
                }
            }

            //printing down game board wall
            for (int i = 3; i < 24; i++)
            {
                gameBoard[i, 7] = '-';
            }

            //printing right game board wall
            for (int i = 2; i < 7; i++)
            {
                gameBoard[24, i] = '|';
            }
        }*/

        /// <summary>
        /// Print the gameboard on the screen
        /// </summary>
        public void PrintGameBoard()
        {
            string board = GameBoardsToString();
            Console.WriteLine(board);
        }

        public string GameBoardsToString()
        {
            StringBuilder board = new StringBuilder();
            string header = "    0 1 2 3 4 5 6 7 8 9 ";
            string empty = new string(' ', 3);
            string line = empty + new string('-', 21) + empty;
            string currentLine = "";
            int balloonValue = 0;
            board.AppendLine(header);
            board.AppendLine(line);

            for (int row = 0; row < 5; row++)
            {
                currentLine = string.Format("{0} {1}", row, VerticalBar);
                for (int col = 0; col < 10; col++)
                {
                    balloonValue = newGameBoard[row, col];
                    currentLine += " ";
                    if (balloonValue == 0)
                    {
                        currentLine += '.';
                    }
                    else
                    {
                        currentLine += balloonValue;
                    }
                }
                currentLine += " " + VerticalBar;
                board.AppendLine(currentLine);
            }
            board.AppendLine(line);

            return board.ToString();
        }
        
        /// <summary>
        /// This method pops the selected ballons and clean them from the board
        /// </summary>
        /// <param name="coordinate">The current coordinates on the player</param>
        public void Shoot(Coordinates coordinate)
        {
            char currentBaloon;
            currentBaloon = get(coordinate);
            Coordinates tempCoordinates = new Coordinates();

            if (currentBaloon < '1' || currentBaloon > '4')
            {
                Console.WriteLine(Message.IllegalMove);
                return;
            }



            AddNewBaloonToGameBoard(coordinate, '.');
            Shoots--;

            tempCoordinates.X = coordinate.X - 1;
            tempCoordinates.Y = coordinate.Y;

            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                Shoots--;
                tempCoordinates.X--;
            }

            tempCoordinates.X = coordinate.X + 1; tempCoordinates.Y = coordinate.Y;
            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                Shoots--;
                tempCoordinates.X++;
            }

            tempCoordinates.X = coordinate.X;
            tempCoordinates.Y = coordinate.Y - 1;
            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                Shoots--;
                tempCoordinates.Y--;
            }

            tempCoordinates.X = coordinate.X;
            tempCoordinates.Y = coordinate.Y + 1;
            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                Shoots--;
                tempCoordinates.Y++;
            }

            Shoots++;
            LandFlyingBaloons();
        }

        public void NewShoot(Coordinates coordinate)
        {
            int currentBaloon;
            currentBaloon = FindPosition(coordinate);
            Coordinates tempCoordinates = new Coordinates();

            if (currentBaloon < 1 || currentBaloon > 4)
            {
                Console.WriteLine(Message.IllegalMove);
                return;
            }



            AddBaloon(coordinate, 0);
            Shoots--;

            tempCoordinates.X = coordinate.X - 1;
            tempCoordinates.Y = coordinate.Y;

            while (currentBaloon == FindPosition(tempCoordinates))
            {
                AddBaloon(tempCoordinates, 0);
                Shoots--;
                tempCoordinates.X--;
            }

            tempCoordinates.X = coordinate.X + 1; tempCoordinates.Y = coordinate.Y;
            while (currentBaloon == FindPosition(tempCoordinates))
            {
                AddBaloon(tempCoordinates, 0);
                Shoots--;
                tempCoordinates.X++;
            }

            tempCoordinates.X = coordinate.X;
            tempCoordinates.Y = coordinate.Y - 1;
            while (currentBaloon == FindPosition(tempCoordinates))
            {
                AddBaloon(tempCoordinates, 0);
                Shoots--;
                tempCoordinates.Y--;
            }

            tempCoordinates.X = coordinate.X;
            tempCoordinates.Y = coordinate.Y + 1;
            while (currentBaloon == FindPosition(tempCoordinates))
            {
                AddBaloon(tempCoordinates, 0);
                Shoots--;
                tempCoordinates.Y++;
            }

            Shoots++;
            NewLandFlyingBaloons();
        }



        private void Swap(Coordinates coordinate, Coordinates coordinate1)
        {
            char tmp = get(coordinate);
            AddNewBaloonToGameBoard(coordinate, get(coordinate1));
            AddNewBaloonToGameBoard(coordinate1, tmp);


        }

        private void NewSwap(Coordinates coordinate, Coordinates coordinate1)
        {
            int tmp = FindPosition(coordinate);
            AddBaloon(coordinate, FindPosition(coordinate1));
            AddBaloon(coordinate1, tmp);


        }

        private void LandFlyingBaloons()
        {
            Coordinates coordinate = new Coordinates();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    coordinate.X = i;
                    coordinate.Y = j;
                    if (get(coordinate) == '.')
                    {
                        for (int k = j; k > 0; k--)
                        {
                            Coordinates tempCoordinates = new Coordinates();
                            Coordinates tempCoordinates1 = new Coordinates();
                            tempCoordinates.X = i;
                            tempCoordinates.Y = k;
                            tempCoordinates1.X = i;
                            tempCoordinates1.Y = k - 1;
                            Swap(tempCoordinates, tempCoordinates1);
                        }
                    }
                }


            }
        }


        private void NewLandFlyingBaloons()
        {
            Coordinates coordinate = new Coordinates();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    coordinate.X = i;
                    coordinate.Y = j;
                    if (FindPosition(coordinate) == '.')
                    {
                        for (int k = j; k > 0; k--)
                        {
                            Coordinates tempCoordinates = new Coordinates();
                            Coordinates tempCoordinates1 = new Coordinates();
                            tempCoordinates.X = i;
                            tempCoordinates.Y = k;
                            tempCoordinates1.X = i;
                            tempCoordinates1.Y = k - 1;
                            NewSwap(tempCoordinates, tempCoordinates1);
                        }
                    }
                }


            }
        }

        /// <summary>
        /// This method takes the input data where the player want to be positioned
        /// </summary>
        /// <param name="IsCoordinates">Verify whether the coordinates are correct</param>
        /// <param name="coordinates">The position on the player</param>
        /// <param name="command">The selected command</param>
        /// <returns>Returns true if the coordinates are correct. Return false if the coordinates are not correct</returns>
        public bool ReadInput(out bool IsCoordinates, ref Coordinates coordinates, ref Command command)
        {
            Console.Write(Message.EnterValues);
            string consoleInput = Console.ReadLine();

            coordinates = new Coordinates();
            command = new Command();

            if (Command.TryParse(consoleInput, ref command))
            {
                IsCoordinates = false;
                return true;
            }
            else if (Coordinates.TryParse(consoleInput, ref coordinates))
            {
                IsCoordinates = true;
                return true;
            }
            else
            {
                IsCoordinates = false;
                return false;
            }
        }
    }



}