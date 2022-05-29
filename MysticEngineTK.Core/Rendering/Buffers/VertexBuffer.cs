using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK.Core.Rendering {
    public class VertexBuffer : IBuffer {
        private const int _maxBufferLength  = 500;        
        public int BufferId { get; }
        public bool Dynamic { get; }        
        public VertexBuffer(float[] vertices) {
            BufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            Dynamic = false;
        }

        public VertexBuffer(int size) {
            if(size > _maxBufferLength) {
                throw new InvalidOperationException($"Size: {size} Exceeds Max Buffer Length of: {_maxBufferLength}. Increase size to support this");
            }
            BufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, _maxBufferLength * sizeof(float), IntPtr.Zero, BufferUsageHint.DynamicDraw);
            Dynamic = true;
        }

        public void WriteData(float[] vertices) {
            if(!Dynamic) {
                throw new InvalidOperationException("Cannot write data to a static vertex buffer");
            }
            Bind();
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, vertices.Length * sizeof(float), vertices);
        }

        public void Bind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
        }

        public void Unbind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        #region ToDo
        public void WriteData(uint[] vertices) {
            if (!Dynamic) {
                throw new InvalidOperationException("Cannot write data to a static vertex buffer");
            }
            throw new NotImplementedException();

        }
        public void WriteData(byte[] vertices) {
            if (!Dynamic) {
                throw new InvalidOperationException("Cannot write data to a static vertex buffer");
            }
            throw new NotImplementedException();
        }
        #endregion
    }
}
