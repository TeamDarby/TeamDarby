 //-----------------------------------------------------------------------
// <copyright file="BalloonPops.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    using System;

    /// <summary>
    /// Includes engine for the game Balloon Pops
    /// </summary>
    public class BalloonPops
    {
        #region constants
        const string TOP = "top";
        const string RESTART = "restart";
        const string EXIT = "exit";
        #endregion

        ScoreManager scoreManager;
        GameBoard gameBoard;
        Coordinates coordinates;
        Command command;
        private bool isCoordinates;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalloonPops"/> class.
        /// </summary>
        public BalloonPops()
        {
            this.scoreManager = new ScoreManager();
            this.gameBoard = new GameBoard();
            this.coordinates = new Coordinates();
            this.command = new Command();
            this.isCoordinates = false;
        }

        /// <summary>
        /// This method starts the game and call all the methods needed
        /// </summary>
        public void Play()
        {
            this.Initialize();
            this.Run();
            this.End();
        }

        /// <summary>
        /// Initial messages and printing the new game board
        /// </summary>
        private void Initialize()
        {
            Console.WriteLine(Message.Welcome);
            Console.WriteLine(Message.Info);
            this.gameBoard.NewGame();
            this.gameBoard.PrintGameBoard();
        }

        /// <summary>
        /// This method holds the play rules and run the moves of the player
        /// </summary>
        private void Run()
        {
            while (this.gameBoard.Baloons > 0)
            {
                if (this.gameBoard.ReadInput(out this.isCoordinates, ref this.coordinates, ref this.command))
                {
                    if (this.isCoordinates)
                    {
                        this.gameBoard.Shoot(coordinates);
                        this.gameBoard.PrintGameBoard();
                    }
                    else
                    {
                        switch (this.command.Name)
                        {
                            case TOP:
                                {
                                    this.scoreManager.PrintScoreList();
                                }
                                break;
                            case RESTART:
                                {
                                    this.gameBoard.NewGame();
                                    this.gameBoard.PrintGameBoard();
                                }
                                break;
                            case EXIT:
                                {
                                    return;
                                }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(Message.WrongInput);
                }
            }
        }

        /// <summary>
        /// When all the balloons are popped, the result will be saved in the score list
        /// </summary>
        private void End()
        {
            int result = this.gameBoard.Shoots;

            if (this.scoreManager.IsTopScore(result))
            {
                Console.WriteLine(Message.Success);
                Console.WriteLine(Message.EnterName);
                string name = Console.ReadLine();
                IPlayer player = new Player(name, result);
                this.scoreManager.AddBestPlayer(player);
            }
            else
            {
                Console.WriteLine(Message.NotBestScore);
            }
        }
    }
}
