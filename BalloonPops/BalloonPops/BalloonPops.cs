 //-----------------------------------------------------------------------
// <copyright file="BalloonPops.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
        bool isCoordinates;

        /// <summary>
        /// The constructor for the fields
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
            Initialize();
            Run();
            End();
        }

        /// <summary>
        /// Initial messages and printing the new gameboard
        /// </summary>
        private void Initialize()
        {
            Console.WriteLine(Message.Welcome);
            Console.WriteLine(Message.Info);
            gameBoard.NewGame();
            gameBoard.PrintGameBoard();
        }

        /// <summary>
        /// This method holds the play rules and run the moves of the player
        /// </summary>
        private void Run()
        {
            while (gameBoard.Baloons > 0)
            {                
                if (gameBoard.ReadInput(out isCoordinates, ref coordinates, ref command))
                {
                    if (isCoordinates)
                    {
                        gameBoard.Shoot(coordinates);
                        gameBoard.PrintGameBoard();
                    }
                    else
                    {
                        switch (command.Name)
                        {
                            case TOP:
                                {
                                    scoreManager.PrintScoreList();
                                }
                                break;
                            case RESTART:
                                {
                                    gameBoard.NewGame();
                                    gameBoard.PrintGameBoard();
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
        /// When all the ballons are popped, the result will be saved in the score list
        /// </summary>
        private void End()
        {
            int result = gameBoard.Shoots;

            if (scoreManager.IsTopScore(result))
            {
                Console.WriteLine(Message.Success);
                Console.WriteLine(Message.EnterName);
                string name = Console.ReadLine();
                IPlayer player = new Player(name, result);
                scoreManager.AddBestPlayer(player);
            }
            else
            {
                Console.WriteLine(Message.NotBestScore);
            }
        }
    }
}
