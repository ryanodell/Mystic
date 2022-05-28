using OpenTK.Mathematics;
using System.Drawing;

namespace MysticEngineTK.Core.Rendering {
    public struct Sprite {
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle Rectangle;
        public float Size;
        public float Rotate;
        public Color4 Color;
    }
}
