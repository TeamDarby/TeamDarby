//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    using System;

    public class Player : IPlayer, IComparable
    {
        private string name;
        private int score;

        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        public string Name
        {
            get
            {
                return name;
            }

            private set
            {
                if (value == null || value.Length <= 2)
                {
                    this.name = "Unknown";
                }
                else if (value.Length > 2)
                {
                    this.name = value;
                }
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Score cannot be a negative number!");
                }
                if (value >= int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("Score cannot be a so big number!");
                }
                else
                {
                    this.score = value;
                }
            }
        }

        public static bool operator <(Player firstPlayer, Player secondPlayer)
        {
            bool lowerScore = firstPlayer.Score < secondPlayer.Score;

            return lowerScore;
        }

        public static bool operator >(Player firstPlayer, Player secondPlayer)
        {
            bool betterScore = firstPlayer.Score > secondPlayer.Score;

            return betterScore;
        }

        public int Compare(Player firstPlayer, Player secondPlayer)
        {
            return firstPlayer.Score.CompareTo(secondPlayer.Score);
        }

        public int CompareTo(object obj)
        {
            Player player = obj as Player;
            if (player.Score < Score)
            {
                return 1;
            }

            if (player.Score > Score)
            {
                return -1;
            }

            return 0;
        }
    }
}