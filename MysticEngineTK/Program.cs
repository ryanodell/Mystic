using MysticEngineTK.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using System;
using System.Diagnostics;

namespace MysticEngineTK
{
    public class Program
    {        
        public static void Main(string[] args)
        {            
            Game game = new ShaderClass(800, 600, "Mystic Engine");
            game.Run();
        }
    }
}