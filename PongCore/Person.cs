using System;
using System.Collections.Generic;
using System.Text;

namespace PongCore
{
    class Person : Player
    {
        /* Represents a human player */


        public Person(int screenWidth, int pos)
        {
            this.x = screenWidth / 2;
            this.y = pos;
        }

        override public void Update(int i, int y)
        {
            // do nothing :)
            ;
        }

        public override void KeyPress(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow)
            {
                this.x = this.x - 1;
            }
            if (key.Key == ConsoleKey.RightArrow)
            {
                this.x = this.x + 1;
            } 
        }


    }
}
