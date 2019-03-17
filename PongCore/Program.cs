using System;
using static System.Console;

namespace PongCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get current console config
            int origScreenSizeX = WindowWidth;
            int origScreenSizeY = WindowHeight;
            ConsoleColor origForegroundColor = ForegroundColor;
            ConsoleColor origBackgroundColor = BackgroundColor;
            bool origCursorVisible = CursorVisible;
            int origCursorSize = CursorSize;
            int origBufferWidth = BufferWidth;
            int origBufferHeight = BufferHeight;

            // Clear the screen
            Clear();
            
            // set up our colors, window size to the old DOS format 80 x 25
            // if not allowed return with error and wait for user to quit
            SetCursorPosition(0, WindowHeight/2);
            CursorVisible = true;
            CursorSize = 100;
            Write("Traveling back in time to the 80\'s ");
            System.Threading.Thread.Sleep(1500);

            SetWindowSize(1, 1);
            SetBufferSize(80, 25);
            SetWindowSize(80, 25);
            // flash colors a bit for CRT feeling :) 
            // Use even loops to result in a Black background
            int i = 0;
            while (i<4)
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
                SetCursorPosition(0, WindowHeight/ 2);
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
            SetCursorPosition(0, WindowHeight-1);
            Write("Press Any Key to Continue... ");
            Read();
            Clear();
            
            string playerPad = "--";
            // Program loop for testing
            do
            {
                while (!KeyAvailable)
                {
                    DrawField(1, 5, 8, 15, 14, 20);
                }
            } while (ReadKey(true).Key != ConsoleKey.Escape);

          
            
            
        }

        static void DrawField(int scorePC,int scoreP1,int pcX,int p1X,int ballX,int ballY)
        {
            /* draw structure
             *  
             * Draw whole area, draw per line
             * 
             * Calculate position of pads -> done by main loop
             * Calculate position of ball -> done by main loop
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
            string sPC = $"PC: {scorePC.ToString()}";
            string sP1 = $"Player: {scoreP1.ToString()}";
            string title = "PONG";
            string ball = "0";
            string pad = "--";
            int i;
            int j;
            string spaceI;
            string spaceJ;
         
            // score line
            i = (screenX / 2) - sPC.Length - (title.Length / 2);
            j = (screenX / 2) - sP1.Length - (title.Length / 2);
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] titleLine = {sPC,spaceI,title,spaceJ,sP1 };
            lines[0] = string.Concat(titleLine);

            // Computer Pad line
            i = pcX;
            j = screenX - pad.Length - i;
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] pcPadLine = { spaceI, pad, spaceJ };
            lines[1] = string.Concat(pcPadLine);

            // Ball line
            i = ballX;
            j = screenX - ball.Length - i;
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] ballLine = { spaceI, ball, spaceJ };
            lines[ballY] = string.Concat(ballLine);

            // Player Pad line
            i = p1X;
            j = screenX - pad.Length - i;
            spaceI = new string(' ', i);
            spaceJ = new string(' ', j);
            string[] p1PadLine = { spaceI, pad, spaceJ };
            lines[lines.GetUpperBound(0)] = string.Concat(p1PadLine);

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
