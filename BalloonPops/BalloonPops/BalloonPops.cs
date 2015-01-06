﻿//-----------------------------------------------------------------------
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

        public void Play()
        {

            GameBoard gameBoard = new GameBoard();
            gameBoard.GenerateNewGame();
            gameBoard.PrintGameBoard();
            ScoreManager topScore = new ScoreManager();
            
            bool isCoordinates;
            Coordinates coordinates = new Coordinates();
            Command command = new Command();

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
                                    topScore.PrintScoreList();
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
                    Console.WriteLine("Wrong Input!");
                }
            }

            int score = gameBoard.ShootCounter;

            /*
             * If the player has the best score, his result is saved as top in the list
             * */
            if (topScore.IsTopScore(score))
            {
                Console.WriteLine("Congratulations, you have popped more ballons"
                    + " then the other players!");
                Console.WriteLine("Please enter your name for the top scoreboard: ");
                string name = Console.ReadLine();
                IPlayer player = new Player(name, score);
                topScore.ManageBestPlayer(player);
            }
        }
    }
}
