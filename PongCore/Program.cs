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
            SetCursorPosition(0, origScreenSizeY/2);
            CursorVisible = true;
            CursorSize = 100;
            Write("Setting up the screen");
            System.Threading.Thread.Sleep(500);
            Read();
            
        }
    }
}
