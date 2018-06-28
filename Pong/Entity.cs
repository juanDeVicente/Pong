using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Pong
{
    class Entity
    {
        char[,] pixels;
        public int x { get; set; }
        public int y { get; set; }
        int w;
        int h;

        public int Dx { get; set; }
        public int Dy { get; set; }
        public Entity(int x, int y, int w, int h, char[,] pixels)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.pixels = pixels;
        }
        public void Update()
        {
            x += Dx;
            if (x < 1)
                x = 1;
            else if (x + w > Program.screenMax - 1)
                x = Program.screenMax - w - 1;
            y += Dy;
            if (y < 1)
                y = 1;
            else if (y + h > Program.screenMax - 1)
                y = Program.screenMax - h - 1;
        }
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                    Console.Write(pixels[i, j]);
                
                Console.SetCursorPosition(x, y + i + 1);
            }
        }
        public bool Collision(Entity entity)
        {
            Rectangle r1 = new Rectangle(x, y, w, h);
            Rectangle r2 = new Rectangle(entity.x, entity.y, entity.w, entity.h);

            return r1.IntersectsWith(r2);
        }
    }
}
