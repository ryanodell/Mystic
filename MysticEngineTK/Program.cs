﻿using MysticEngineTK.Core;
using MysticEngineTK.Core.Management;

namespace MysticEngineTK {
    public class Program
    {        
        public static void Main(string[] args)
        {            
            Game game = new UsingVertexStruct(800, 600, "Mystic Engine");
            game.Run();
            DisplayManager.Instance.GameWindow.Close();
        }
    }
}