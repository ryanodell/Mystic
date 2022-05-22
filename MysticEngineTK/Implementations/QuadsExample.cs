using MysticEngineTK.Core;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK {
    internal class QuadsExample : Game {
        public QuadsExample(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        private readonly float[] _vertices = {
            //Positions         //Colors
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f, //top right - Red
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, //bottom right- Green
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, //bottom left - Blue
            -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f  //top left - White
        };

        private uint[] _indices = {
            0, 1, 3,
            1, 2, 3 
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private int _elementBufferObject;

        Shader _shader;

        protected override void Initalize() {

        }

        protected override void LoadContent() {
            _shader = new(Shader.ParseShader("Resources/Shaders/InterpolatedColor.glsl"));
            bool result = _shader.CompileShader();
            if (!result) {
                Console.WriteLine("Failed to compile shader");
            }

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        }
        protected override void Update(GameTime gameTime) {

        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader?.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

        }

    }
}
