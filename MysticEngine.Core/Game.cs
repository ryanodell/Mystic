using MysticEngine.Core.Rendering;
using MysticEngine.GLFW;

namespace MysticEngine.Core
{
    public abstract class Game
    {
        //Test comment
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        public Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
        }

        public void Run()
        {
            Initalize();
            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);
            LoadContent();
            GameTime gameTime = new();
            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                gameTime.TotalElapsedSeconds = (float)Glfw.Time;
                gameTime.DeltaTime = (float)Glfw.Time - gameTime.TotalElapsedSeconds;                
                Update(gameTime);
                Glfw.PollEvents();
                Render();
            }
            DisplayManager.CloseWindow();
        }

        protected abstract void Initalize();
        protected abstract void LoadContent();

        protected abstract void Update(GameTime gameTime);
        protected abstract void Render();
    }
}
