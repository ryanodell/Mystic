﻿using MysticEngineTK.Core;

namespace MysticEngineTK {
    public class Program
    {        
        public static void Main(string[] args)
        {            
            Game game = new TextureWithColor(800, 600, "Mystic Engine");
            game.Run();
        }
    }
}