using MysticEngineTK.Core;
using MysticEngineTK.Core.Cameras;
using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


//NOTE!!! DOING THIS LATER
namespace MysticEngineTK {
    internal class ApplyingMatrices : Game {
        public ApplyingMatrices(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        private readonly float[] _vertices = {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f  // top left
        };

        private readonly uint[] _indices = {
            0, 1, 3,
            1, 2, 3
        };

        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;

        Shader _shader;
        Texture2D _texture;

        OrthographicCamera camera;

        Vector3 position = Vector3.Zero;

        protected override void Initalize() {

        }

        protected override void LoadContent() {
            camera = new OrthographicCamera(-1.0f, 1.0f, -1.0f, 1.0f);

            _vertexArray = new VertexArray();
            _vertexBuffer = new(_vertices);
            BufferLayout bufferLayout = new BufferLayout();
            //Positions
            bufferLayout.Add<float>(3);
            //Texture Coordinates
            bufferLayout.Add<float>(2);
            //Colors:
            bufferLayout.Add<float>(3);

            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);
            _shader = new(ShaderProgramSource.ParseShader("Resources/Shaders/MatrixTextureColor.glsl"));
            bool result = _shader.CompileShader();
            if (result) {
                _shader.Use();
            }
            _indexBuffer = new(_indices);
            _texture = ResourceManager.Instance.LoadTexture("Resources/Textures/container.png");
            _texture.Use();

        }
        protected override void Update(GameTime gameTime) {
            var test = DisplayManager.Instance.GameWindow.Size;
            position -= new Vector3(0, 0, 1);
            camera.SetPosition(position);
        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _vertexArray.Bind();
            _shader?.Use();
            _shader.SetMatrix4("u_ViewProjection", camera.GetProjectionViewMatrix());
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
