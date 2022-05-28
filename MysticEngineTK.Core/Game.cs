using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace MysticEngineTK.Core {
    public abstract class Game {
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
        private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;
        public SpriteBatch SpriteBatch;

        public Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
            _nativeWindowSettings.Size = new Vector2i(initialWindowWidth, InitialWindowHeight);
            _nativeWindowSettings.Title = initialWindowTitle;
            _nativeWindowSettings.API = ContextAPI.OpenGL;

            _gameWindowSettings.RenderFrequency = 60.0;
            _gameWindowSettings.UpdateFrequency = 60.0;

        }

        public void Run() {
            Initalize();
            using GameWindow gameWindow = DisplayManager.Instance.CreateWindow(_gameWindowSettings, _nativeWindowSettings);
            GameTime gameTime = new();
            gameWindow.Load += LoadContent;
            gameWindow.UpdateFrame += (FrameEventArgs eventArgs) => {
                double time = eventArgs.Time;
                gameTime.ElapsedGameTime = TimeSpan.FromMilliseconds(time);
                gameTime.TotalGameTime += TimeSpan.FromMilliseconds(time);
                Update(gameTime);
            };
            gameWindow.RenderFrame += (FrameEventArgs eventArgs) => {
                Render();
                //Render(gameTime, SpriteBatch);
                gameWindow.SwapBuffers();
            };
            gameWindow.Resize += (ResizeEventArgs) => {
                GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
            };
            gameWindow.Run();
        }

        protected abstract void Initalize();
        protected abstract void LoadContent();
        protected abstract void Update(GameTime gameTime);
        protected abstract void Render();
        //protected abstract void Render(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
