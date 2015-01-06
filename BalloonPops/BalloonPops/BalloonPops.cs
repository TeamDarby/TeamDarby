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

        public void Play()
        {

            GameBoard board = new GameBoard();
            board.GenerateNewGame();
            board.PrintGameBoard();
            TopScore topScore = new TopScore();

            topScore.OpenTopScoreList();
            
            bool isCoordinates;
            Coordinates coordinates = new Coordinates();
            Command command = new Command();

            while (board.RemainingBaloons > 0)
            {
                if (board.ReadInput(out isCoordinates, ref coordinates, ref command))
                {
                    if (isCoordinates)
                    {
                        board.Shoot(coordinates);
                        board.PrintGameBoard();
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
                                    board.GenerateNewGame();
                                    board.PrintGameBoard();
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

            int score = board.ShootCounter;

            if (topScore.IsTopScore(score))
            {
                Console.WriteLine("Please enter your name for the top scoreboard: ");
                string name = Console.ReadLine();
                IPlayer player = new Player(name, score);
                topScore.AddToTopScoreList(player);
            }
            topScore.SaveTopScoreList();
        }
    }
}
