using MysticEngineTK.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using System;

namespace MysticEngineTK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game game = new TestGame(100, 200, "test");
            game.Run();
        }
    }
}