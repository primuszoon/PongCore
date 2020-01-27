using System;
using System.Collections.Generic;
using System.Text;

namespace PongCore
{
    class Ball
    {
        public int x;
        public int y;
        public int sX; // horizontal direction, -1=left, +1=right
        public int sY; // vertical direction, -1=up, +1=down

        private int screenWidth;

        public Ball(int width, int height)
        {
            // Center the ball in the middle
            this.x = width / 2;
            this.y = height / 2;
            this.screenWidth = width-1;

            // randomize starting speeds
            Random rnd = new Random();
            if (rnd.Next() % 2 == 0)
            {
                this.sX = 1;
            }
            else
            {
                this.sX = -1;
            }
            if (rnd.Next() % 2 == 0)
            {
                this.sY = 1;
            }
            else
            {
                this.sY = -1;
            }

        }

        private void Increase()
        {
            // Check for the edges of the screen and invert if necessary
            if (this.x + this.sX <= 0 || this.x + this.sX >= this.screenWidth)
            {
                this.sX = -this.sX;
            }


            this.y = this.y + this.sY;
            this.x = this.x + this.sX;
        }

        public bool Update(Player pTop, Player pBottom) // returns false when a goal is scored
        {
            /* Collision detection cases:
             * 
             *    0     Ball above pad: 
             *    --    Ball goes left (no hit on pad): No goal, invert vertical speed, increase horizontal speed
             *          Ball goes right (hit on pad): No goal, invert vertical speed
             * 
             *    0     Ball not above pad:
             *     --   Ball goes right (hit on pad): No goal, invert vertical speed, decrease horizontal speed, (if null invert horizontal speed)
             *     ||   Ball goes left (no hit on pad): Goal
             *     ||---X2    
             *     |----X1    
             */

            // store bounds of pad of players
            int pTopX1 = pTop.x;
            int pTopX2 = pTop.x + pTop.pad.Length - 1;
            int pBottomX1 = pBottom.x;
            int pBottomX2 = pBottom.x + pBottom.pad.Length - 1;


            // going up towards player pTop
            if (this.sY < 0)
            {
                // Check if next hit we would be on the same line as pTop, otherwise just increase
                if (pTop.y == this.y + this.sY)
                {
                    // ball above pad:
                    if(this.x >= pTopX1 && this.x <= pTopX2)
                    {
                        // next hit is on pad
                        if (this.x + this.sX >= pTopX1 && this.x + this.sX <= pTopX2)
                        {
                            this.sY = -this.sY;
                        }
                        // next his is not on pad
                        else
                        {
                            this.sY = -this.sY;
                            this.sX = (this.sX) < 0 ? this.sX - 1 : this.sX + 1;
                        }
                        this.Increase();
                        return true;
                    }
                    // ball not above pad:
                    else
                    {
                        // next hit is on pad
                        if (this.x + this.sX >= pTopX1 && this.x + this.sX <= pTopX2)
                        {
                            this.sY = -this.sY;
                            if (this.sX == -1 || this.sX == 1)
                            {
                                this.sX = -this.sX;                                
                            }
                            else
                            {
                                this.sX = (this.sX) < 0 ? this.sX + 1 : this.sX - 1;
                            }
                            this.Increase();
                            return true;
                        }
                        // next hit not on pad: GOALLL
                        else
                        {
                            // other player gets a point :)
                            pBottom.points++;
                            return false;
                        }
                    }
                }
                else
                {
                    this.Increase();
                }
            }
            // going down towards player pBottom
            else if (this.sY > 0)
            {
                // Check if next hit we would be on the same line as pBottom, otherwise just increase
                if (pBottom.y == this.y + this.sY)
                {
                    // ball above pad:
                    if (this.x >= pBottomX1 && this.x <= pBottomX2)
                    {
                        // next hit is on pad
                        if (this.x + this.sX >= pBottomX1 && this.x + this.sX <= pBottomX2)
                        {
                            this.sY = -this.sY;
                        }
                        // next his is not on pad
                        else
                        {
                            this.sY = -this.sY;
                            this.sX = (this.sX) < 0 ? this.sX - 1 : this.sX + 1;
                        }
                        this.Increase();
                        return true;
                    }
                    // ball not above pad:
                    else
                    {
                        // next hit is on pad
                        if (this.x + this.sX >= pBottomX1 && this.x + this.sX <= pBottomX2)
                        {
                            this.sY = -this.sY;
                            if (this.sX == -1 || this.sX == 1)
                            {
                                this.sX = -this.sX;
                            }
                            else
                            {
                                this.sX = (this.sX) < 0 ? this.sX + 1 : this.sX - 1;
                            }
                            this.Increase();
                            return true;
                        }
                        // next hit not on pad: GOALLL
                        else
                        {
                            // other player gets a point :)
                            pTop.points++;
                            return false;
                        }
                    }
                }
                else
                {
                    this.Increase();
                }
            }
            else // ehm, ball not moving?
            {
                return false;
            }
            return true;


        }

    }
}
