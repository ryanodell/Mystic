using OpenTK.Graphics.OpenGL4;

namespace MysticEngineTK.Core.Rendering {
    public class Texture2D : IDisposable {
        private bool _disposed;
        public int Handle { get; set; }
        public Texture2D(int handle) {
            Handle = handle;
        }

        public void Use() {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        ~Texture2D() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing) {
            if(!_disposed) {
                GL.DeleteTexture(Handle);
                _disposed = true;
            }
        }
    }
}
