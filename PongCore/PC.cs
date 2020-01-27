using System;
using System.Collections.Generic;
using System.Text;

namespace PongCore
{
    class PC : Player
    {
        /* Represents the PC player */

        public PC(int screenWidth, int pos)
        {
            this.x = screenWidth / 2;
            this.y = pos;
        }

        override public void Update(int x, int y)  
        {
            // procedure to move towards the ball
            int diff = x - this.x;

            // randomize speed aka humanize
            Random rnd = new Random();
            int prob = rnd.Next(10);
            if (prob < 8)
            {
                this.speed = 1;
            }
            else if (prob <= 9)
            {
                this.speed = 0;
            }
            else
            {
                this.speed = 2;
            }

            if(diff < 0)
            {
                this.x = this.x - this.speed;
            }
            else if(diff > 0){
                this.x = this.x + this.speed;
            }

        }

        public override void KeyPress(ConsoleKeyInfo key)
        {
            // do nothing :) 
            ;
        }

    }
}
