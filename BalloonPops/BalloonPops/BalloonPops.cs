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

            GameBoard gb = new GameBoard();
            gb.GenerateNewGame();
            gb.PrintGameBoard();
            TopScore ts = new TopScore();

            ts.OpenTopScoreList();
            
            bool isCoordinates;
            Coordinates coordinates = new Coordinates();
            Command command = new Command();
            while (gb.RemainingBaloons > 0)
            {
                if (gb.ReadInput(out isCoordinates, ref coordinates, ref command))
                {
                    if (isCoordinates)
                    {
                        gb.Shoot(coordinates);
                        gb.PrintGameBoard();
                    }
                    else
                    {
                        switch (command.Name)
                        {
                            case TOP:
                                {
                                    ts.PrintScoreList();
                                }
                                break;
                            case RESTART:
                                {
                                    gb.GenerateNewGame();
                                    gb.PrintGameBoard();
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

            int score = gb.ShootCounter;

            if (ts.IsTopScore(score))
            {
                Console.WriteLine("Please enter your name for the top scoreboard: ");
                string name = Console.ReadLine();
                IPlayer player = new Player(name, score);
                ts.AddToTopScoreList(player);
            }
            ts.SaveTopScoreList();
        }
    }
}
