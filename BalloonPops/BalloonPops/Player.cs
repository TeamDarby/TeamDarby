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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="score">The score of the player</param>
        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        /// <summary>
        /// Property of field name
        /// </summary>
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

        /// <summary>
        /// Property of the field score
        /// </summary>
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

        /// <summary>
        /// This method for the < operator verify the score of two players
        /// </summary>
        /// <param name="firstPlayer">The score of the player with best result</param>
        /// <param name="secondPlayer">The score of the current player</param>
        /// <returns>Return the player with lower score</returns>
        public static bool operator <(Player firstPlayer, Player secondPlayer)
        {
            bool lowerScore = firstPlayer.Score < secondPlayer.Score;

            return lowerScore;
        }

        /// <summary>
        /// This method for the > operator verify the score of two players
        /// </summary>
        /// <param name="firstPlayer">The score of the player with best result</param>
        /// <param name="secondPlayer">The score of the current player</param>
        /// <returns>Return the player with the best score</returns>
        public static bool operator >(Player firstPlayer, Player secondPlayer)
        {
            bool betterScore = firstPlayer.Score > secondPlayer.Score;

            return betterScore;
        }

        /// <summary>
        /// This method set the logic for the Compare operator and compares the scores of the players
        /// </summary>
        /// <param name="firstPlayer">The score of the player with best result</param>
        /// <param name="secondPlayer">The score of the current player</param>
        /// <returns></returns>
        public int Compare(Player firstPlayer, Player secondPlayer)
        {
            return firstPlayer.Score.CompareTo(secondPlayer.Score);
        }

        /// <summary>
        /// This method set the logic for the CompareTo operator and compares the scores of the players
        /// </summary>
        /// <param name="obj">Takes the player as an object and takes its score</param>
        /// <returns>It returns 1 if the score of the current player is higher than the score of the other player.
        /// It returns -1 if the score of the current player is less than the score of the other player.
        /// It returns 0 if the score of the current player is equal to the score of the other player.
        /// </returns>
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