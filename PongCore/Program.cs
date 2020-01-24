using System;
using static System.Console;

namespace PongCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // VARS
            // Get current console config
            int origScreenSizeX = WindowWidth;
            int origScreenSizeY = WindowHeight;
            ConsoleColor origForegroundColor = ForegroundColor;
            ConsoleColor origBackgroundColor = BackgroundColor;
            bool origCursorVisible = CursorVisible;
            int origCursorSize = CursorSize;
            int origBufferWidth = BufferWidth;
            int origBufferHeight = BufferHeight;

            Init();

            // Program loop for testing
            string title = "PONG";  // The title we want to appear on top
            // Player p1 = new Person(WindowWidth, WindowHeight - 1); // let's let computers fight!
            Player p1 = new PC(WindowWidth,1);
            Player p2 = new PC(WindowWidth, WindowHeight - 1);
            Ball ball = new Ball(WindowWidth, WindowHeight);

            //p1.name = "Player 1";
            p1.name = "PC";
            p2.name = "PC";

            do
            {
                while (!KeyAvailable)
                {
                    DrawField(p1.points, p2.points, p1.x, p2.x, ball.x,ball.y, p1.name, p2.name, p1.pad, p2.pad, title);
                    if (!ball.Update(p1, p2))
                    {
                        ball = null;
                        ball = new Ball(WindowWidth, WindowHeight);
                    }
                    System.Threading.Thread.Sleep(50);
                    // update player
                    // still to do
                    // udate pc
                    p1.Update(ball.x, ball.y);
                    p2.Update(ball.x, ball.y);

                }
            } while (ReadKey(true).Key != ConsoleKey.Escape);

            // restore console before ending:
            WindowWidth = origScreenSizeX;
            WindowHeight = origScreenSizeY;
            ForegroundColor = origForegroundColor;
            BackgroundColor = origBackgroundColor;
            CursorVisible = origCursorVisible;
            CursorSize = origCursorSize;
            BufferWidth = origBufferWidth;
            BufferHeight = origBufferHeight;
        }

        static void Init()
        {

            // Clear the screen
            Clear();

            // set up our colors, window size to the old DOS format 80 x 25
            // if not allowed return with error and wait for user to quit
            SetCursorPosition(0, WindowHeight / 2);
            CursorVisible = true;
            CursorSize = 100;
            Write("Traveling back in time to the 80\'s ");
            System.Threading.Thread.Sleep(1500);

            SetWindowSize(1, 1);
            SetBufferSize(160, 50);
            SetWindowSize(160, 50);
            // flash colors a bit for CRT feeling :) 
            // Use even loops to result in a Black background
            int i = 0;
            while (i < 4)
            {
                if (BackgroundColor == ConsoleColor.White)
                {
                    BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    BackgroundColor = ConsoleColor.White;
                }
                if (i == 1)
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                Clear();
                SetCursorPosition(0, WindowHeight / 2);
                Write("Traveling back in time to the 80\'s");
                System.Threading.Thread.Sleep(100);
                i++;
            }
            System.Threading.Thread.Sleep(500);
            Clear();
            SetCursorPosition(0, WindowHeight / 2);
            Write("Starting pong");
            i = 0;
            while (i < 3)
            {
                System.Threading.Thread.Sleep(500);
                Write(".");
                i++;
            }

            // show splash screen
            // make sure the first line of the logo has the full width, it's used for calculations
            string[] logo = new string[6];
            logo[0] = @" _ __   ___  _ __   __ _ ";
            logo[1] = @"| '_ \ / _ \| '_ \ / _` |";
            logo[2] = @"| |_) | (_) | | | | (_| |";
            logo[3] = @"| .__/ \___/|_| |_|\__, |";
            logo[4] = @"| |                 __/ |";
            logo[5] = @"|_|                |___/ ";

            // get the cursor position to start
            // x position
            int x = (WindowWidth / 2) - (logo[0].Length / 2);
            int y = (WindowHeight / 2) - (logo.Length / 2);

            // start writing the logo
            Clear();
            SetCursorPosition(x, y);
            foreach (string line in logo)
            {
                int currentCursorX = CursorLeft;
                int currentCursorY = CursorTop;
                Write(line);
                int newCursorX = currentCursorX;
                int newCursorY = currentCursorY + 1;
                SetCursorPosition(newCursorX, newCursorY);
            }

            // Press any key to continue
            SetCursorPosition(0, WindowHeight - 1);
            Write("Press Any Key to Continue... ");
            Read();
            Clear();
        }

        static void DrawField(int scoreTop,int scoreBottom,int topX,int bottomX,int ballX,int ballY, string topPlayer, string bottomPlayer, string topPad, string bottomPad, string title)
        {
            /* draw structure
             *  
             * Draw whole area, draw per line
             * 
             * Calculate position of pads -> done in main loop
             * Calculate position of ball -> done in main loop
             * 
             * First line contains score (invert BG and FG color)
             * 
             * Draw PC pad line
             * Empty lines where there is no ball
             * Draw ball line
             * empty lines where there is no ball
             * Draw Player pad line
             * 
             * 
             */

            // get current screen size
            int screenX = WindowWidth;
            int screenY = WindowHeight-1;

            // cursor visibility false in drawcall to avoid effects of resizing
            CursorVisible = false;

            // create string array as big as screenY for looping through it
            string[] lines = new string[screenY];

            // elements
            string sTop = $"{topPlayer}: {scoreTop.ToString()}";
            string sBottom = $"{bottomPlayer}: {scoreBottom.ToString()}";
            string ball = "0";
            int i;
            int j;
            string spaceI;
            string spaceJ;
         
            // score line
            i = (screenX / 2) - sTop.Length - (title.Length / 2);
            j = (screenX / 2) - sBottom.Length - (title.Length / 2);
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] titleLine = {sTop,spaceI,title,spaceJ,sBottom };
            lines[0] = string.Concat(titleLine);

            // Top Pad line
            i = topX;
            j = screenX - topPad.Length - i;
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] topPadLine = { spaceI, topPad, spaceJ };
            lines[1] = string.Concat(topPadLine);

            // Ball line
            i = ballX;
            j = screenX - ball.Length - i;
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] ballLine = { spaceI, ball, spaceJ };
            lines[ballY] = string.Concat(ballLine);

            // Bottom Pad line
            i = bottomX;
            j = screenX - bottomPad.Length - i;
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] bottomPadLine = { spaceI, bottomPad, spaceJ };
            lines[lines.GetUpperBound(0)] = string.Concat(bottomPadLine);

            // fill the array with empty lines where needed
            spaceI = new string(' ', screenX);
            for(int k=2;k<lines.GetUpperBound(0); k++)
            {
                if (k != ballY)
                {
                    lines[k] = spaceI;
                }
            }

            // Start drawing
            for(int a=0;a<=lines.GetUpperBound(0);a++)
            {
                SetCursorPosition(0, a);
                if (a == 0)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Green;
                    Write(lines[a]);
                    ForegroundColor = ConsoleColor.Green;
                    BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Write(lines[a]);
                }
            }

            // reduce flickering
            System.Threading.Thread.Sleep(50);

        }
    }
}
