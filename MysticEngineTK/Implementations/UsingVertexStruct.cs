using MysticEngineTK.Core;
using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK {
    internal class UsingVertexStruct : Game {
        public UsingVertexStruct(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }
        private int _tileSize = 16;
        private readonly float[] _vertices = {
             //Positions            TexCoords       Color                   TexSlot
             0.5f,  0.5f, 0.0f,     1.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f, // top right      //4 : 0
             0.5f, -0.5f, 0.0f,     1.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom right   //5 : 1
            -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom left    //6 : 2
            -0.5f,  0.5f, 0.0f,     0.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f  // top left       //7 : 3
        };

        private readonly uint[] _indices = {
            0, 1, 3,    1, 2, 3
        };

        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;

        Shader _shader;
        Matrix4 _projectionMatrix;

        Vertex _vertex = new Vertex();
        //private Vertex[] _vertexVertices;

        protected override void Initalize() {
            _vertex.Position = new float[3] { 0.5f, 0.5f, 0.0f };

        }

        protected override void LoadContent() {
            _vertexArray = new();
            _vertexBuffer = new(_vertices.Length);
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

            ResourceManager.Instance.LoadTexture("Resources/Textures/Objects_v2.png");

            _projectionMatrix = Matrix4.CreateOrthographicOffCenter(-2.0f, 2.0f, -1.5f, 1.5f, -1.0f, 1.0f);
            _projectionMatrix += Matrix4.CreateScale(2.5f);

            _shader.SetMatrix4("u_Projection", _projectionMatrix);
            _vertexBuffer.WriteData(_vertices);

        }
        protected override void Update(GameTime gameTime) {
            //_projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, DisplayManager.Instance.GameWindow.Size.X, 0, DisplayManager.Instance.GameWindow.Size.Y, -1.0f, 1.0f);
            //_projectionMatrix = Matrix4.CreateOrthographicOffCenter(-2.0f, 2.0f, -1.5f, 1.5f, -1.0f, 1.0f);
            //_projectionMatrix += Matrix4.CreateScale(2.5f);
            //float tmp = _vertices[0];
            //tmp += .005f;
            //_vertices[0] = tmp;

            //_projectionMatrix *= _viewMatrix;

            //_vertices[3] = _vertices[3] - 0.005f;
            //_vertices[4] = _vertices[4] - 0.005f;
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
    }
}
