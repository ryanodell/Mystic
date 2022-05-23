using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK.Core.Rendering {
    public struct BufferElement {
        public VertexAttribPointerType Type;
        public int Count;
        public bool Normalized;
    };

    public class BufferLayout {
        private List<BufferElement> _elements = new();
        private int _stride;

        public BufferLayout() {
            _stride = 0;
        }

        public List<BufferElement> GetBufferElements() {
            return _elements;
        }

        public int GetStride() {
            return _stride;
        }

        public void Add<T>(int count, bool normalized = false) where T : struct {
            VertexAttribPointerType type;
            if (typeof(float) == typeof(T)) {
                type = VertexAttribPointerType.Float;
                _stride += sizeof(float) * count;
            }
            else if (typeof(uint) == typeof(T)) {
                type = VertexAttribPointerType.UnsignedInt;
                _stride += sizeof(uint) * count;
            }
            else if (typeof(byte) == typeof(T)) {
                type = VertexAttribPointerType.UnsignedByte;
                _stride += sizeof(byte) * count;
            } else {
                throw new ArgumentException($"{typeof(T)} is Not a Valid Type");
            }
            _elements.Add(new BufferElement { Type = type, Count = count, Normalized = normalized });
        }
    }



}
