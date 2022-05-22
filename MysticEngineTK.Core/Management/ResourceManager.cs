using MysticEngineTK.Core.Rendering;
using MysticEngineTK.Core.Management.Textures;

namespace MysticEngineTK.Core.Management {
    public sealed class ResourceManager {
        private static ResourceManager? _instance = null;
        private static readonly object _lock = new();
        private IDictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();

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
            _textureCache.TryGetValue(textureName, out var value);
            if(value is not null) {
                return value;
            }
            value = TextureFactory.Load(textureName);
            _textureCache.Add(textureName, value);
            return value;
        }

    }
}
