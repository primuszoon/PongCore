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
            Write("Traveling back in time to the 80\'s");
            System.Threading.Thread.Sleep(1500);

            SetWindowSize(1, 1);
            SetBufferSize(80, 25);
            SetWindowSize(80, 25);
            // flash colors a bit for CRT feeling :)
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
            
            Read();
            
        }
    }
}
