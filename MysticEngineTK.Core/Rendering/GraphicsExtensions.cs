using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace MysticEngineTK.Core.Rendering
{
    public static class GraphicsExtensions {
        public static void GlCheckError() {
            var error = GL.GetError();
            if (error != ErrorCode.NoError) {
                throw new Exception($"GL.GetError retuned: {error}");
            }
        }

        public static void LogGlError(string location)
        {
            try
            {
                GlCheckError();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GL Error at {location} {ex.Message}");
            }
        }
    }
}
