using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace MysticEngineTK.Core
{
    public abstract class Game
    {
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
        private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

        public Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;

            _gameWindowSettings.RenderFrequency = 60.0;
            _gameWindowSettings.UpdateFrequency = 60.0;
        }

        public void Run()
        {
            Initalize();
            using (GameWindow gameWindow = new GameWindow(_gameWindowSettings, _nativeWindowSettings))
            {
                gameWindow.Load += LoadContent;
                gameWindow.UpdateFrame += (FrameEventArgs eventArgs) =>
                {
                    Update((float)eventArgs.Time);
                };
                gameWindow.RenderFrame += (FrameEventArgs eventArgs) =>
                {
                    Render();
                };
                gameWindow.Run();
            }
        }

        protected abstract void Initalize();
        protected abstract void LoadContent();
        protected abstract void Update(float detlaTime);
        protected abstract void Render();
    }
}
