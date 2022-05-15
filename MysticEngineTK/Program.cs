using MysticEngineTK.Core;
using System.IO;

namespace MysticEngineTK {
    public class Program
    {        
        public static void Main(string[] args)
        {            
            Game game = new TextureQuads(800, 600, "Mystic Engine");
            game.Run();
        }
    }
}