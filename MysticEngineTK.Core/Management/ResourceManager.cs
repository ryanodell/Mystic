using MysticEngineTK.Core.Rendering;
using MysticEngineTK.Core.Management.Textures;

namespace MysticEngineTK.Core.Management {
    public sealed class ResourceManager {
        private static ResourceManager? _instance = null;
        private static readonly object _lock = new();
        public static ResourceManager Instance {
            get {
                lock (_lock) {
                    if (_instance == null) {
                        _instance = new ResourceManager();
                    }
                    return _instance;
                }
            }
        }

        public Texture2D LoadTexture(string textureName) {
            return TextureFactory.Load(textureName);
        }

    }
}
