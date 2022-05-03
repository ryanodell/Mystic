using static MysticEngine.GL.GL;
using System.Drawing;
using System.Drawing.Imaging;

namespace MysticEngine.Core.Rendering
{
    public class Texture2D
    {
        public uint Handle;

        public Texture2D(uint handle)
        {
            Handle = handle;
        }

        public static Texture2D LoadFromFile(string path)
        {
            uint handle = glGenTexture();

            glActiveTexture(GL_TEXTURE0);
            glBindTexture(GL_TEXTURE_2D, handle);

            using (var image = new Bitmap(path))
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);
                glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, image.Width, image.Height, 0, GL_RGBA, GL_UNSIGNED_BYTE, data.Scan0);
            }
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

            glGenerateMipmap(GL_TEXTURE_2D);

            return new Texture2D(handle);
        }

        //This assume TextureUnit0 for  now...
        public void Use()
        {
            glActiveTexture(GL_TEXTURE0);
            glBindTexture(GL_TEXTURE_2D, Handle);
        }
    }
}
