namespace BalloonPops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Coordinates
    {
        private int x;
        private int y;

        public int X
        {
            get
            {
                return this.x;
            }

            private set
            {
                if (value >= 0 && value <= 9)
                {
                    this.x = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("X", "Wrong X coordinate value");
                }
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }

            private set
            {
                if (value >= 0 && value <= 4)
                {
                    this.y = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Wrong Y coordinate value");
                }
            }
        }

        public static bool TryParse(string input, ref Coordinates result)
        {
            char[] separators = { ' ', ',' };

            string[] substrings = input.Split(separators);

            if (substrings.Count<string>() != 2)
            {
                Console.WriteLine("Invalid move or command!");
                return false;
            }

            string coordinate = substrings[1].Trim();
            int x;
            if (int.TryParse(coordinate, out x))
            {
                if (x >= 0 && x <= 9)
                {
                    result.X = x;
                }
                else
                {
                    Console.WriteLine("Wrong X coordinate!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid move or command!");
                return false;
            }

            coordinate = substrings[0].Trim();
            int y;
            if (int.TryParse(coordinate, out y))
            {
                if (y >= 0 && y <= 4)
                {
                    result.Y = y;
                }
                else
                {
                    Console.WriteLine("Wrong Y coordinates");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid move or command!");
                return false;
            }

            return true;
        }
    }
}
