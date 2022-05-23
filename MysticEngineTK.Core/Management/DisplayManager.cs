using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;

namespace MysticEngineTK.Core.Management {
    public sealed class DisplayManager {
        private static DisplayManager _instance = null;
        private static readonly object _lock = new();
        public GameWindow GameWindow;
        public static DisplayManager Instance {
            get {
                lock(_lock) {
                    if(_instance is null) {
                        _instance = new DisplayManager();
                    }
                    return _instance;
                }
            }
        }

        public GameWindow CreateWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) {
            GameWindow = new GameWindow(gameWindowSettings, nativeWindowSettings);            
            return GameWindow;
        }


    }
}
