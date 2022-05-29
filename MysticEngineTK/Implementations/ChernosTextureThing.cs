using MysticEngineTK.Core;
using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK {
    internal class ChernosTextureThing : Game {
        public ChernosTextureThing(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        private readonly float[] _vertices = {
            //Position, texture coordinates, colors, texture slots
             1.5f,  0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f,  // top right      //0
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,  // bottom right   //1
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f,  // bottom left    //2
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f,   // top left      //3

             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, // top right      //4 : 0
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, // bottom right   //5 : 1
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, // bottom left    //6 : 2
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f  // top left       //7 : 3
        };

        private readonly uint[] _indices = {
            0, 1, 3,    1, 2, 3,
            4, 5, 7,    5, 6, 7
        };

        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;

        Shader _shader;

        protected override void Initalize() {

        }

        protected unsafe override void LoadContent() {
            _vertexArray = new VertexArray();
            _vertexBuffer = new(_vertices);
            BufferLayout bufferLayout = new BufferLayout();
            //Positions
            bufferLayout.Add<float>(3);
            //Texture Coordinates
            bufferLayout.Add<float>(2);
            //Colors:
            bufferLayout.Add<float>(3);
            //Texture Slot:
            bufferLayout.Add<float>(1);

            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);
            _shader = new(ShaderProgramSource.ParseShader("Resources/Shaders/TextureSlotWithColor.glsl"));
            bool result = _shader.CompileShader();
            if (result) {
                _shader.Use();
            }
            _indexBuffer = new(_indices);

            var textureSamplerUniformLocation = _shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1};
            //GL.Uniform4(textureSamplerUniformLocation, 2, samplers);
            GL.Uniform1(textureSamplerUniformLocation, 2, samplers);
            //GL.Uniform4(textureSamplerUniformLocation, ref thing);

            ResourceManager.Instance.LoadTexture("Resources/Textures/container.png");
            //_texture1.Use();
            ResourceManager.Instance.LoadTexture("Resources/Textures/Objects_v2.png");
            //GL.BindTextureUnit(0, _texture1.Handle);
            //GL.BindTextureUnit(1, _texture2.Handle);
            //_texture2.Use();

        }
        protected override void Update(GameTime gameTime) {

        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _vertexArray.Bind();
            _shader?.Use();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
