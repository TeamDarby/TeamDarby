namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Command
    {

        private string name;

        /// <summary>
        /// Property for the field name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
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
        /// This method parse the command selected by the player and return true or false for next action
        /// </summary>
        /// <param name="input">The input data</param>
        /// <param name="command">The command selected by the player</param>
        /// <returns>Returns true if there is selected command. Returns false if there are no selected commands</returns>
        public static bool TryParse(string input, ref Command command)
        {
            if (input == "top")
            {
                command.Name = input;
                return true;
            }

            if (input == "restart")
            {
                command.Name = input;
                return true;
            }

            if (input == "exit")
            {
                command.Name = input;
                return true;
            }

            return false;
        }
    }
}
