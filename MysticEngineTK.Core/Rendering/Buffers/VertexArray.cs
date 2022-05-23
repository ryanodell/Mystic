using OpenTK.Graphics.OpenGL4;

namespace MysticEngineTK.Core.Rendering {
    public class VertexArray : IBuffer {
        public int BufferId { get; }
        public VertexArray() {
            BufferId = GL.GenVertexArray();
        }

        ~VertexArray() {
            GL.DeleteVertexArray(BufferId);
        }

        public void AddBuffer(VertexBuffer vb, BufferLayout bufferLayout) {
            Bind();
            vb.Bind();
            var elements = bufferLayout.GetBufferElements();
            int offset = 0;
            for(int i = 0; i < elements.Count(); i++) {
                var currentElement = elements[i];
                GL.EnableVertexAttribArray(i);
                //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
                GL.VertexAttribPointer(i, currentElement.Count, currentElement.Type, currentElement.Normalized, bufferLayout.GetStride(), offset);
                offset += currentElement.Count * Utilities.GetSizeOfVertexAttribPointerType(currentElement.Type);
            }
        }

        public void Bind() {
            GL.BindVertexArray(BufferId);
        }

        public void Unbind() {
            GL.BindVertexArray(0);
        }
    }
}
