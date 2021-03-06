﻿//-----------------------------------------------------------------------
// <copyright file="GameBoard.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    using System;
    using System.Text;

    class GameBoard
    {
        #region Constants

        public const int BoardWidth = 25;
        public const int BoardHeight = 8;
        public const int Rows = 5;
        public const int Cols = 10;

        private const char Dash = '-';
        private const char Blank = ' ';
        private const char VerticalBar = '|';


        #endregion

        #region Fields

        public int[,] gameBoard = new int[Rows, Cols];
        private int shoots = 0;
        private int balloons = 50;

        #endregion

        #region Properties

        /// <summary>
        /// Property of the shoots field
        /// </summary>
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

        /// <summary>
        /// Property of the balloons field
        /// </summary>
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

        /// <summary>
        /// This method generates the gameboard with random values for the balloons
        /// </summary>
        public void GenerateGameBoard()
        {
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
        public void NewGame()
        {
            Shoots = 0;
            Baloons = 50;
            GenerateGameBoard();
        }

        /// <summary>
        /// This method add new baloons to the gameboard on the selected coordinates
        /// </summary>
        /// <param name="coordinate">The current coordinates of the player</param>
        /// <param name="value"></param>
        private void AddBaloon(Coordinates coordinate, int value)
        {
            int xPosition, yPosition;
            xPosition = coordinate.X;
            yPosition = coordinate.Y;
            gameBoard[yPosition, xPosition] = value;
        }

        /// <summary>
        /// This method gets the current coordinates
        /// </summary>
        /// <param name="coordinate">The current coordinates</param>
        /// <returns>Returns the current position of the player on the gameboard</returns>
        private int FindPosition(Coordinates coordinate)
        {
            int xPosition, yPosition;
            if (coordinate.X < 0 || coordinate.Y < 0 || coordinate.X > 9 || coordinate.Y > 4)
            {
                return 'e';
            }

            xPosition = coordinate.X;
            yPosition = coordinate.Y;
            return gameBoard[yPosition, xPosition];
        }

        /// <summary>
        /// Print the gameboard on the screen
        /// </summary>
        public void PrintGameBoard()
        {
            string board = GameBoardsToString();
            Console.WriteLine(board);
        }

        /// <summary>
        /// This method convert the gameboard to a string that could be printed to the screen(on the page).
        /// It prints numbers for the columns and the rows, empty spaces between the baloons and 
        /// paste - when a ballon is missing and it is popped
        /// </summary>
        /// <returns>It returns the gameboard as a string</returns>
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
                    balloonValue = gameBoard[row, col];
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
            int currentBaloon = FindPosition(coordinate);

            if (currentBaloon < 1 || currentBaloon > 4)
            {
                Console.WriteLine(Message.IllegalMove);
                return;
            }

            AddBaloon(coordinate, 0);
            Baloons--;
            Shoots++;

            CheckNeighborsCells(coordinate, currentBaloon);
            LandFlyingBaloons();
        }

        private void CheckNeighborsCells(Coordinates coordinate, int currentBaloon)
        {
            Coordinates tempCoordinates = new Coordinates();

            for (int direction = 0; direction < 4; direction++)
            {
                switch (direction)
                {
                    case 0:
                        {
                            tempCoordinates.X = coordinate.X - 1;
                            tempCoordinates.Y = coordinate.Y;
                            break;
                        }
                    case 1:
                        {
                            tempCoordinates.X = coordinate.X + 1;
                            tempCoordinates.Y = coordinate.Y;
                            break;
                        }
                    case 2:
                        {
                            tempCoordinates.X = coordinate.X;
                            tempCoordinates.Y = coordinate.Y - 1;
                            break;
                        }
                    case 3:
                        {
                            tempCoordinates.X = coordinate.X;
                            tempCoordinates.Y = coordinate.Y + 1;
                            break;
                        }
                    default:
                        break;
                }

                while (currentBaloon == FindPosition(tempCoordinates))
                {
                    AddBaloon(tempCoordinates, 0);
                    Baloons--;
                    tempCoordinates.X--;
                }
            }
        }

        /// <summary>
        /// This method find the initial coordinates, takes the new coordintates and position the player location there.
        /// </summary>
        /// <param name="coordinate">Initial coordinates</param>
        /// <param name="coordinate1">The new coordinates</param>
        private void Swap(Coordinates coordinate, Coordinates coordinate1)
        {
            int tmp = FindPosition(coordinate);
            AddBaloon(coordinate, FindPosition(coordinate1));
            AddBaloon(coordinate1, tmp);
        }

        /// <summary>
        /// This method moves through the rows and the columns, takes the coordinates of the popped balloons and 
        /// land the balloons from the top rows to the empty positions.
        /// </summary>
        private void LandFlyingBaloons()
        {
            Coordinates coordinate = new Coordinates();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    coordinate.X = i;
                    coordinate.Y = j;
                    if (FindPosition(coordinate) == 0)
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