namespace BalloonPops.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScoreManagerTest
    {
        [TestMethod]
        public void TestEmptyTopScoreList()
        {
            InitializeTestPreparation();

            string expectedTopScoreListData = "";
            string actualTopScoreListData = ReadFile(Path);

            int expectedTopPlayersCount = 0;
            int actualTopPlayersCount = topPlayers.Count;

            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The top score list data isn't correct!");
            Assert.AreEqual(expectedTopPlayersCount, actualTopPlayersCount, "The top score list count isn't correct!");
            ClearFile();
        }

        [TestMethod]
        public void TestAddBestPlayer()
        {
            InitializeTestPreparation();

            string expectedTopScoreListData = "1. Ivan --> 46 moves";
            IPlayer ivan = new Player("Ivan", 46);

            scoreManager.AddBestPlayer(ivan);
            string actualTopScoreListData = ReadFile(Path);

            int expectedTopPlayersCount = 1;
            int actualTopPlayersCount = topPlayers.Count;

            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The adding of best player result isn't correct!");
            Assert.AreEqual(expectedTopPlayersCount, actualTopPlayersCount, "The adding of best player result isn't correct!");
            ClearFile();
        }

        [TestMethod]
        public void TestNotAddBestPlayer()
        {
            InitializeTestPreparation();
            FillTopScoreData();

            IPlayer petia = new Player("Petia", 32);
            IPlayer ivo = new Player("Ivo", 33);

            scoreManager.AddBestPlayer(petia);
            scoreManager.AddBestPlayer(ivo);

            string expectedTopScoreListData = "1. Ivan --> 21 moves2. Kamen --> 23 moves3. Pavel --> 25 moves4. Stoian --> 28 moves5. Ilia --> 30 moves";
            string actualTopScoreListData = ReadFile(Path);

            int expectedTopPlayersCount = 5;
            int actualTopPlayersCount = topPlayers.Count;

            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The adding of best player result isn't correct!");
            Assert.AreEqual(expectedTopPlayersCount, actualTopPlayersCount, "The adding of best player result isn't correct!");
            ClearFile();
        }

        [TestMethod]
        public void TestIsTopScore()
        {
            InitializeTestPreparation();

            IPlayer pavel = new Player("Pavel", 47);

            bool expectedIsTopScoreValue = true;
            bool actualIsTopScoreValue = scoreManager.IsTopScore(pavel.Score);

            string expectedTopScoreListData = "";
            string actualTopScoreListData = ReadFile(Path);

            int expectedTopPlayersCount = 0;
            int actualTopPlayersCount = topPlayers.Count;

            Assert.AreEqual(expectedIsTopScoreValue, actualIsTopScoreValue, "The check for top score result isn't correct!");
            Assert.AreEqual(expectedTopPlayersCount, actualTopPlayersCount, "The check for top score result isn't correct!");
            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The check for top score result isn't correct!");
            ClearFile();
        }

        [TestMethod]
        public void TestIsNotTopScore()
        {
            InitializeTestPreparation();
            FillTopScoreData();

            IPlayer tania = new Player("Tania", 31);

            bool expectedIsTopScoreValue = false;
            bool actualIsTopScoreValue = scoreManager.IsTopScore(tania.Score);

            string expectedTopScoreListData = "1. Ivan --> 21 moves2. Kamen --> 23 moves3. Pavel --> 25 moves4. Stoian --> 28 moves5. Ilia --> 30 moves";
            string actualTopScoreListData = ReadFile(Path);

            int expectedTopPlayersCount = 5;
            int actualTopPlayersCount = topPlayers.Count;

            Assert.AreEqual(expectedIsTopScoreValue, actualIsTopScoreValue, "The check for top score result isn't correct!");
            Assert.AreEqual(expectedTopPlayersCount, actualTopPlayersCount, "The check for top score result isn't correct!");
            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The check for top score result isn't correct!");
            ClearFile();
        }

        [TestMethod]
        public void TestPrintScoreList()
        {
            InitializeTestPreparation();
            FillTopScoreData();
            string path = @"..\..\ConsoleTests.txt";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                TextWriter tmp = Console.Out;
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);
                scoreManager.PrintScoreList();
                Console.SetOut(tmp);
                sw.Close();
            }

            string expectedTopScoreListData = "Scoreboard:\r\n1. Ivan --> 21 moves\r\n2. Kamen --> 23 moves\r\n3. Pavel --> 25 moves\r\n4. Stoian --> 28 moves\r\n5. Ilia --> 30 moves\r\n";
            string actualTopScoreListData = File.ReadAllText(path);

            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The print of console isn't correct!");
            ClearFile();
        }

        [TestMethod]
        public void TestPrintEmptyScoreList()
        {
            InitializeTestPreparation();

            string path = @"..\..\ConsoleTests.txt";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                TextWriter tmp = Console.Out;
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);
                scoreManager.PrintScoreList();
                Console.SetOut(tmp);
                sw.Close();
            }

            string expectedTopScoreListData = "Scoreboard is empty\r\n";
            string actualTopScoreListData = File.ReadAllText(path);

            Assert.AreEqual(expectedTopScoreListData, actualTopScoreListData, "The print of console isn't correct!");
            ClearFile();
        }

        #region Private data

        private const string Path = @"..\..\TopScoresTests.txt";
        private ScoreManager scoreManager;
        private List<IPlayer> topPlayers;

        private void InitializeTestPreparation() 
        {
            topPlayers = new List<IPlayer>();
            scoreManager = new ScoreManager();            
            ClearFile();
        }

        private void ClearFile()
        {
            File.WriteAllText(Path, String.Empty);
        }

        private void FillTopScoreData()
        {
            IPlayer vania = new Player("Vania", 31);
            IPlayer ivan = new Player("Ivan", 21);
            IPlayer kamen = new Player("Kamen", 23);
            IPlayer pavel = new Player("Pavel", 25);
            IPlayer stoian = new Player("Stoian", 28);
            IPlayer ilia = new Player("Ilia", 30);

            this.scoreManager.AddBestPlayer(vania);
            this.scoreManager.AddBestPlayer(ivan);
            this.scoreManager.AddBestPlayer(kamen);
            this.scoreManager.AddBestPlayer(pavel);
            this.scoreManager.AddBestPlayer(stoian);
            this.scoreManager.AddBestPlayer(ilia);
        }

        private string ReadFile(string path)
        {
            string result = "";
            using (StreamReader topScoreReader = new StreamReader(path))
            {
                string line = topScoreReader.ReadLine();
                result += line;
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
                    result += line;
                }
            }

            return result;
        }

        private string ListToString(IList<IPlayer> data)
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