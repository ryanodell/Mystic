using MysticEngineTK.Core;
using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Drawing;

namespace MysticEngineTK {
    internal class UsingVertexStruct : Game {
        public UsingVertexStruct(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }
        private readonly float[] _vertices = {
             //Positions            TexCoords       Color                   TexSlot
             0.5f,  0.5f, 0.0f,     1.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f, // top right      //4 : 0
             0.5f, -0.5f, 0.0f,     1.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom right   //5 : 1
            -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom left    //6 : 2
            -0.5f,  0.5f, 0.0f,     0.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f  // top left       //7 : 3
        };

        //private float[] _vertices = new float[4 * 10];

        private Vertex[] _vetexVerts = new Vertex[4];

        private readonly uint[] _indices = {
            0, 1, 3,    1, 2, 3
        };

        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;

        Shader _shader;
        Matrix4 _projectionMatrix;
        Texture2D _texture;

        protected override void Initalize() {

        }

        protected override void LoadContent() {

            //private readonly float[] _vertices = {
            //     //Positions            TexCoords       Color                   TexSlot
            //     0.5f,  0.5f, 0.0f,     1.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f, // top right      //4 : 0
            //     0.5f, -0.5f, 0.0f,     1.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom right   //5 : 1
            //    -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom left    //6 : 2
            //    -0.5f,  0.5f, 0.0f,     0.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f  // top left       //7 : 3
            //};
            _texture = ResourceManager.Instance.LoadTexture("Resources/Textures/Objects_v2.png");

            float x = 2, y = 6;
            float spriteWidth = 16, spriteHeight = 16;
            

            _vertexArray = new();
            Color4 color = Color4.White;
            #region OriginalsForReference
            //Vertex _topRight = new Vertex {
            //    Position = new[] { 0.5f, 0.5f, 0.0f },
            //    Color = new[] { color.R, color.G, color.B },
            //    TexCoords = new[] { 1.0f, 1.0f },
            //    TexId = 0f
            //};
            //Vertex _bottomRight = new Vertex {
            //    Position = new[] { 0.5f, -0.5f, 0.0f },
            //    Color = new[] { color.R, color.G, color.B },
            //    TexCoords = new[] { 1.0f, 0.0f },
            //    TexId = 0f
            //};
            //Vertex _bottomLeft = new Vertex {
            //    Position = new[] { -0.5f, -0.5f, 0.0f },
            //    Color = new[] { color.R, color.G, color.B },
            //    TexCoords = new[] { 0.0f, 0.0f },
            //    TexId = 0f
            //};
            //Vertex _topLeft = new Vertex {
            //    Position = new[] { -0.5f, 0.5f, 0.0f },
            //    Color = new[] { color.R, color.G, color.B },
            //    TexCoords = new[] { 0.0f, 1.0f },
            //    TexId = 0f
            //};
            #endregion

            /*
             * Cherno:
             *  0 bottom left
                1 bottom right
                2 top right
                3 top left
            */
            Vertex _topRight = new Vertex {
                Position = new[] { 0.5f, 0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { ((x + 1) * spriteWidth) / _texture.Width, ((y + 1) * spriteHeight) / _texture.Width },
                TexId = 0f
            };
            Vertex _bottomRight = new Vertex {
                Position = new[] { 0.5f, -0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { ((x + 1) * spriteWidth) / _texture.Width, (y * spriteHeight) / _texture.Width },
                TexId = 0f
            };
            Vertex _bottomLeft = new Vertex {
                Position = new[] { -0.5f, -0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { (x * spriteWidth) / _texture.Width, (y * spriteHeight) / _texture.Width },
                TexId = 0f
            };
            Vertex _topLeft = new Vertex {
                Position = new[] { -0.5f, 0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { (x * spriteWidth) / _texture.Width, ((y + 1) * spriteHeight) / _texture.Width },
                TexId = 0f
            };
            _vetexVerts[0] = _topRight;
            _vetexVerts[1] = _bottomRight;
            _vetexVerts[2] = _bottomLeft;
            _vetexVerts[3] = _topLeft;

            _vertexBuffer = new(4);
            BufferLayout bufferLayout = new();
            //Positions
            bufferLayout.Add<float>(3);
            //Texture Coordinates
            bufferLayout.Add<float>(2);
            //Colors:
            bufferLayout.Add<float>(3);
            //Texture Slot:
            bufferLayout.Add<float>(1);

            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);
            _shader = new(ShaderProgramSource.ParseShader("Resources/Shaders/MatrixTextureSlotColor.glsl"));
            bool result = _shader.CompileShader();
            if (result) {
                _shader.Use();
            }
            _indexBuffer = new(_indices);

            var textureSamplerUniformLocation = _shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSamplerUniformLocation, 2, samplers);

            

            _projectionMatrix = Matrix4.CreateOrthographicOffCenter(-2.0f, 2.0f, -1.5f, 1.5f, -1.0f, 1.0f);
            _projectionMatrix += Matrix4.CreateScale(2.5f);

            _shader.SetMatrix4("u_Projection", _projectionMatrix);

            //_vertexBuffer.WriteData(_vetexVerts.AsSpan<float>());

        }
        protected override void Update(GameTime gameTime) {
            Array.Clear(_vertices);
            int cursor = 0;
            foreach(ref readonly Vertex vert in _vetexVerts.AsSpan()) {
                _vertices[cursor++] = vert.Position[0];
                _vertices[cursor++] = vert.Position[1];
                _vertices[cursor++] = vert.Position[2];
                _vertices[cursor++] = vert.TexCoords[0];
                _vertices[cursor++] = vert.TexCoords[1];
                _vertices[cursor++] = vert.Color[0];
                _vertices[cursor++] = vert.Color[1];
                _vertices[cursor++] = vert.Color[2];
                _vertices[cursor++] = vert.TexId;
            }
            _vertexBuffer.WriteData(_vertices);
        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.Black);
            _vertexArray.Bind();
            _shader.SetMatrix4("u_Projection", _projectionMatrix);
            _shader?.Use();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        private void _drawSprite(Texture2D texture, Vector2 position, Rectangle srcRect, float scale, float rotate, Color4 color) {

        }
    }
}
