using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonPops
{
	// veche pisha na c#, uraaaaaaaaaaaaaaa, mnogo e yako tova be!
    class baloncheta
    {
        static void Main(string[] args)
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
                            case "top":
                                {
                                    ts.PrintScoreList();
                                }
                                break;
                            case "restart":
                                {
                                    gb.GenerateNewGame();
                                    gb.PrintGameBoard();
                                }
                                break;
                            case "exit":
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
                Player player = new Player(name, score);
                ts.AddToTopScoreList(player);
            }
            ts.SaveTopScoreList();
        }
    }
}
