﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BalloonPops
{
    class TopScore
    {
        public const int MAX_TOP_SCORE_COUNT = 5;
        List<IPlayer> topScoreList = new List<IPlayer>();

        public bool IsTopScore(int score)
        {
            if (topScoreList.Count >= MAX_TOP_SCORE_COUNT)
            {
               //ersonScoreComparer comparer = new PersonScoreComparer();
                topScoreList.Sort();
                if (topScoreList[MAX_TOP_SCORE_COUNT - 1].Score > score)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void AddToTopScoreList(IPlayer person)
        {
            topScoreList.Add(person);
            //PersonScoreComparer comparer = new PersonScoreComparer();
            topScoreList.Sort();
            while (topScoreList.Count > 5)
            {
                topScoreList.RemoveAt(5);
            }
        }

        public void OpenTopScoreList()
        {
            using (StreamReader TopScoreStreamReader = new StreamReader("..\\..\\TopScore.txt"))
            {
                string line = TopScoreStreamReader.ReadLine();
                while (line != null)
                {
                    char[] separators = { ' ' };
                    string[] substrings = line.Split(separators);
                    int substringsCount = substrings.Count<string>();
                    if (substringsCount > 0)
                    {
                        string name = substrings[1];
                        int score = int.Parse(substrings[substringsCount - 2]);
                        IPlayer player = new Player(name, score);
                        topScoreList.Add(player);
                    }
                    line = TopScoreStreamReader.ReadLine();
                }
            }
        }

        public void SaveTopScoreList()
        {
            if (topScoreList.Count > 0)
            {
                string toWrite = "";
                using (StreamWriter TopScoreStreamWriter = new StreamWriter("..\\..\\TopScore.txt"))
                {
                    for (int i = 0; i < topScoreList.Count; i++)
                    {
                        toWrite += i.ToString() + ". " + topScoreList[i].Name + " --> " + topScoreList[i].Score.ToString() + " moves";
                        TopScoreStreamWriter.WriteLine(toWrite);
                        toWrite = "";
                    }
                }
            }
            PrintScoreList();
        }

        public void PrintScoreList()
        {
            Console.WriteLine("Scoreboard:");
            if (topScoreList.Count > 0)
            {
                for (int i = 0; i < topScoreList.Count; i++)
                {
                    Console.WriteLine(i.ToString() + ". " + topScoreList[i].Name + " --> " + topScoreList[i].Score.ToString() + "moves");
                }
            }
            else
            {
                Console.WriteLine("Scoreboard is empty");
            }
        }
    }
}
