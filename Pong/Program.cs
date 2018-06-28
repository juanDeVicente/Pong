using System;
using System.Threading;

namespace Pong
{
    class Program
    {
        //GLOBALS
        static char[,] pixels = new char[300, 300];
        public static int screenMax = 34;

        //Player
        static int lifes = 3;
        static Entity table;
        static char[,] tablePixels = new char[2, 5]
        {
            {' ','_','_','_',' '},
            {'|','_','_','_','|'}
        };
        //Ball
        static Entity ball;
        static char[,] ballPixels = new char[1, 1]
        {
            {'O'}
        };
        static void Main(string[] args)
        {
            Play();
        }
        static void Play()
        {
            Create();
            Draw();
            while (Console.ReadKey(true).KeyChar != ' ') ;
            Console.Clear();
            while (lifes > 0)
            {
                Input();
                Update();
                Draw();
                Console.Clear();
            }
        }
        static void Create()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(30, 11);
            table = new Entity(1, 7, 10, 2, tablePixels);
            ball = new Entity(3, 5, 1, 1, ballPixels);
            ball.Dx = -1;
            ball.Dy = -1;
        }
        static void Input()
        {
            if (NativeKeyboard.IsKeyDown(KeyCode.Right))
                table.Dx += 1;
            if (NativeKeyboard.IsKeyDown(KeyCode.Left))
                table.Dx -= 1;
        }
        static void Update()
        {
            table.Update();
            table.Dx = 0;
            table.Dy = 0;
            ball.Update();
            if (ball.Collision(table) && ball.y > table.y - 2)
            {
                ball.Dy *= -1;
                Console.Write('\a');
            }
            if (ball.y == 1)
            {
                ball.Dy *= -1;
                Console.Write('\a');
            }
            if (ball.x == 1 || ball.x == screenMax - 7)
            {
                ball.Dx *= -1;
                Console.Write('\a');
            }
            if (ball.y > table.y + 2)
            {
                lifes--;
                Play();
            }
        }
        static void Draw()
        {
            DrawBorders();
            table.Draw();
            ball.Draw();
            Thread.Sleep(100);
        }
        static void DrawBorders()
        {
            for (int i = 0; i < screenMax - 5; i++)
                Console.Write("-");
            Console.SetCursorPosition(0, 9);
            for (int i = 0; i < screenMax - 5; i++)
                Console.Write("-");
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 10; i++)
                Console.WriteLine("|");

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(28, i);
                Console.WriteLine("|");
            }
        }
        internal enum KeyCode : int
        {
            Left = 0x25,
            Up,
            Right,
            Down
        }
        internal static class NativeKeyboard
        {      
            private const int KeyPressed = 0x8000;
            
            public static bool IsKeyDown(KeyCode key)
            {
                return (GetKeyState((int)key) & KeyPressed) != 0;
            }

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            private static extern short GetKeyState(int key);
        }
    }
}
