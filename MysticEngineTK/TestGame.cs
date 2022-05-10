using MysticEngineTK.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using MysticEngineTK.Core.Rendering;

namespace MysticEngineTK {
    public class TestGame : Game {
        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        protected override void Initalize() {

        }

        protected override async void LoadContent() {
            Shader shader = new Shader(await Shader.ParseShader("Resources/Shaders/Default.glsl"));

            var test = shader;
        }
        protected override void Update(GameTime gameTIme) {

        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
        }

    }
}
