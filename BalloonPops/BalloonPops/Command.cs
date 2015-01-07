namespace BalloonPops
{

    class Command
    {
        string name;

        /// <summary>
        /// Property of the field name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// This method verify the command from the game with the selected command from the player.
        /// </summary>
        /// <param name="input">The initial command selected from the player</param>
        /// <param name="result">The commands that could be selected from the player</param>
        /// <returns>Returns true if correct command is selected.
        /// Returns false ifwrong command is selected.</returns>
        public static bool TryParse(string input, ref Command result)
        {
            if (input == "top")
            {
                result.Name = input;
                return true;
            }

            if (input == "restart")
            {
                result.Name = input;
                return true;
            }

            if (input == "exit")
            {
                result.Name = input;
                return true;
            }

            return false;
        }
    }
}