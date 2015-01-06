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


        public BalloonPops()
        {
            this.scoreManager = new ScoreManager();
            this.gameBoard = new GameBoard();
            this.coordinates = new Coordinates();
            this.command = new Command();
            this.isCoordinates = false;
        }

        public void Play()
        {
            Initialize();
            Run();
            End();
        }


        private void Initialize()
        {
            gameBoard.GenerateNewGame();
            gameBoard.PrintGameBoard();
        }

        private void Run()
        {
            while (gameBoard.RemainingBaloons > 0)
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
                                    gameBoard.GenerateNewGame();
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

        private void End()
        {
            int result = gameBoard.ShootCounter;

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
