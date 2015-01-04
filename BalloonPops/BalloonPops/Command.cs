using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonPops
{
    class Command
    {

        string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                this.name = value;
            }
        }

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
