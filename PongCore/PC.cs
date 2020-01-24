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
            
            if(diff < 0)
            {
                this.x = this.x - this.speed;
            }
            else if(diff > 0){
                this.x = this.x + this.speed;
            }

        }

    }
}
