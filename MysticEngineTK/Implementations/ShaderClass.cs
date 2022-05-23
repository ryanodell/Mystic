using MysticEngineTK.Core;
using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK {
    internal class ShaderClass : Game {
        public ShaderClass(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        private readonly float[] _vertices = {
            //Positions         //Colors
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f,    // Bottom-left vertex - Red
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,    // Bottom-right vertex - Green
             0.0f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f    // Top vertex - Blue
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        Shader _shader;

        protected override void Initalize() {

        }

        protected override void LoadContent() {
            _shader = new(ShaderProgramSource.ParseShader("Resources/Shaders/InterpolatedColor.glsl"));
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
        }
        protected override void Update(GameTime gameTime) {
            var input = DisplayManager.Instance.GameWindow.KeyboardState;
            if(input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape)) {
                DisplayManager.Instance.GameWindow.Close();
            }
        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader?.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        }

    }
}
