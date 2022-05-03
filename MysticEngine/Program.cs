using MysticEngine.Core;
using System;   

namespace MysticEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game game = new TestGame(800, 600, "Test Textures");
            game.Run();
        }
    }
}