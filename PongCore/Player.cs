using System;
using System.Collections.Generic;
using System.Text;

namespace PongCore
{
    abstract class Player
    {
        public int points = 0;
        public readonly string pad = "--";

        public int x; // horizontal position
        public int y; // vertical position (needed for collision detection)
        public string name;

        protected readonly int speed = 1;

        abstract public void Update(int x, int y);
        


    }
}
