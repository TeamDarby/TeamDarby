﻿namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class GameBoard
    {
        public const int Rows = 8;
        public const int Columns = 25;

        public char[,] gameBoard = new char[Columns, Rows];
        private  int numOfShoots = 0;
        private  int remainingShoots = 50;

        public int ShootCounter
        {
            get
            {
                return numOfShoots;

            }

            private set
            {
                this.numOfShoots = value;
            }
        }

        public int RemainingBaloons
        {
            get
            {
                return remainingShoots;
            }

            private set
            {
                this.remainingShoots = value;
            }
        }

        /// <summary>
        /// Method that start the game again and position all elements on the board
        /// </summary>
        public void GenerateNewGame()
        {
            Console.WriteLine(Message.Welcome);
            Console.WriteLine(Message.Info);
            remainingShoots = 50;
            FillBlankGameBoard();

            Random random = new Random();
            Coordinates coords = new Coordinates();

            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    coords.X = i;
                    coords.Y = j;


                    AddNewBaloonToGameBoard(coords, (char)(random.Next(1, 5) + (int)'0'));
                }
            }
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

        /// <summary>
        /// This method gets the current coordinates
        /// </summary>
        /// <param name="coordinate">The current coordinates</param>
        /// <returns>Returns the current position of the player on the gameboard</returns>
        private char get(Coordinates coordinate)
        {
            int xPosition, yPosition;
            if (coordinate.X < 0 || coordinate.Y < 0 || coordinate.X > 9 || coordinate.Y > 4) return 'e';
            xPosition = 4 + coordinate.X * 2;


            yPosition = 2 + coordinate.Y;
            return gameBoard[xPosition, yPosition];
        }

        /// <summary>
        /// Fill the blank fields on the gameboard
        /// </summary>
        private void FillBlankGameBoard()
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

            char numOfShootser = '0';


            for (int i = 4; i < 25; i++)
            {
                if ((i % 2 == 0) && numOfShootser <= '9') gameBoard[i, 0] = (char)numOfShootser++;
                else gameBoard[i, 0] = ' ';
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
        }

        /// <summary>
        /// Print the gameboard on the screen
        /// </summary>
        public void PrintGameBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 25; j++)
                {

                    // Possible bottleneck!!!
                    Console.Write(gameBoard[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
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
            remainingShoots--;

            tempCoordinates.X = coordinate.X - 1;
            tempCoordinates.Y = coordinate.Y;

            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                remainingShoots--;
                tempCoordinates.X--;
            }

            tempCoordinates.X = coordinate.X + 1; tempCoordinates.Y = coordinate.Y;
            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                remainingShoots--;
                tempCoordinates.X++;
            }

            tempCoordinates.X = coordinate.X;
            tempCoordinates.Y = coordinate.Y - 1;
            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                remainingShoots--;
                tempCoordinates.Y--;
            }

            tempCoordinates.X = coordinate.X;
            tempCoordinates.Y = coordinate.Y + 1;
            while (currentBaloon == get(tempCoordinates))
            {
                AddNewBaloonToGameBoard(tempCoordinates, '.');
                remainingShoots--;
                tempCoordinates.Y++;
            }

            numOfShoots++;
            LandFlyingBaloons();
        }

        private void Swap(Coordinates coordinate, Coordinates coordinate1)
        {
            char tmp = get(coordinate);
            AddNewBaloonToGameBoard(coordinate, get(coordinate1));
            AddNewBaloonToGameBoard(coordinate1, tmp);


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
