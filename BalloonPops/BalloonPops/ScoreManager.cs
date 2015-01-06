//-----------------------------------------------------------------------
// <copyright file="ScoreManager.cs" company="SoftUni">
//  Copyright (c) 2015 SoftUni. All rights reserved.
// </copyright>
// <author>Team "Darby"</author>
//-----------------------------------------------------------------------
namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Responsible for managing top results
    /// </summary>
    public class ScoreManager
    {
        #region Constants

        /// <summary>
        /// Represent allowed numbers of top players
        /// </summary>
        private const int TopPlayersCount = 5;
        
        /// <summary>
        /// Path and name of the file where are kept the top players name and result
        /// </summary>
        private const string Path = @"..\..\TopScores.txt";

        #endregion

        /// <summary>
        /// List with all top players name and score
        /// </summary>
        private List<IPlayer> topPlayers = new List<IPlayer>();
   
        #region Public methods

        /// <summary>
        /// Check whether given score meets the criteria for a score in the top 5
        /// </summary>
        /// <param name="score">Given score to check</param>
        /// <returns>True or false depending on the given score</returns>
        public bool IsTopScore(int score)
        {
            bool isTopScore = true;

            if (this.topPlayers.Count >= TopPlayersCount && this.topPlayers[TopPlayersCount - 1].Score > score)
            {
                isTopScore = false;
            }

            return isTopScore;
        }

        /// <summary>
        /// Add new player record to top score records
        /// </summary>
        /// <param name="person">Instance of class Player</param>
        public void AddToTopScoreList(IPlayer person)
        {
            this.topPlayers.Add(person);
            this.topPlayers.Sort();
            while (this.topPlayers.Count > 5)
            {
                this.topPlayers.RemoveAt(5);
            }
        }

        /// <summary>
        /// Gets all records from txt file and push them all to a List
        /// </summary>
        public void LoadTopScore()
        {
            using (StreamReader topScoreReader = new StreamReader(Path))
            {
                string line = topScoreReader.ReadLine();
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
                        this.topPlayers.Add(player);
                    }

                    line = topScoreReader.ReadLine();
                }
            }
        }

        /// <summary>
        /// Save data from top score list into a txt file
        /// </summary>
        public void SaveTopScoreList()
        {
            if (this.topPlayers.Count > 0)
            {
                string topScoreRecords = this.TopScoreListToString();
                
                using (StreamWriter topScoreStreamWriter = new StreamWriter(Path))
                {                    
                    topScoreStreamWriter.Write(topScoreRecords);
                }
            }
        }

        /// <summary>
        /// Prints information from the list with the top scores on the console
        /// </summary>
        public void PrintScoreList()
        {
            string scoreList = this.TopScoreListToString();

            if (scoreList != string.Empty)
            {
                Console.WriteLine("Scoreboard:");
                Console.WriteLine(scoreList);
            }
            else
            {
                Console.WriteLine("Scoreboard is empty");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Converts data from top score list to a System.String
        /// </summary>
        /// <returns>String representation of top score list data</returns>
        private string TopScoreListToString()
        {
            StringBuilder scoreList = new StringBuilder();

            if (this.topPlayers.Count > 0)
            {
                for (int i = 0; i < this.topPlayers.Count; i++)
                {
                    scoreList.AppendLine(string.Format("{0}. {1} --> {2} moves", i + 1, this.topPlayers[i].Name, this.topPlayers[i].Score));
                }
            }

            return scoreList.ToString().Trim();
        }

        #endregion
    }
}