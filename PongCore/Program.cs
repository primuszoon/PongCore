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
            
        }
    }
}
