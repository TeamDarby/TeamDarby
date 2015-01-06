namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Command
    {
        string name;

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