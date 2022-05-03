using System.Numerics;

namespace MysticEngine.Core
{
    /// <summary>
    /// Texture Coordinates work with 0,0 being the bottom left and 1,1 being top right.
    /// </summary>
    public struct TextureCoordinates
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public TextureCoordinates(Vector2 position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }
    }
}
