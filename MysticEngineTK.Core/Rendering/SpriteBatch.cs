using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK.Core.Rendering {
    public class SpriteBatch {
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;
        private bool _hasBegun = false;
        private int _batchSize;
        private Sprite[] _spriteList;
        private int _cursor = 0;
        private Shader _shader;
        private Matrix4 _cameraMatrix;
        //These will never change. This is our quad
        private readonly uint[] _indices = {
            0, 1, 3,
            1, 2, 3
        };
        public SpriteBatch(int batchSizeEstimate) {
            batchSizeEstimate = _batchSize;
            _spriteList = new Sprite[batchSizeEstimate];
            _vertexArray = new();
        }
        public void Begin(Shader shader, Matrix4 cameraMatrix = default(Matrix4)) {
            if(_hasBegun) {
                throw new InvalidOperationException($"Cannot call Begin on Sprite Renderer before End() has been called");
            }
            _vertexArray.Bind();
            _shader = shader;
            _cameraMatrix = cameraMatrix;
            _hasBegun = true;
        }

        public void End() {
            if(!_hasBegun) {
                throw new InvalidOperationException($"Cannot call End on Sprite Renderer before Begin() has been called");
            }
            Flush();
            _hasBegun = false;
        }

        public void Draw(Texture2D texture, Vector2 position, float size, float rotate, Color4 color) {
            _checkIfFlushNeeded();
            _spriteList[_cursor] = new Sprite { Texture = texture, Position = position, Size = size, Rotate = rotate, Color = color };
            _cursor++;
        }

        private void _checkIfFlushNeeded() {
            if(_cursor > _batchSize) {
                Flush();
            }
        }

        public void Flush() {
            _vertexArray.Bind();
            int drawCount = 0;
            foreach(ref readonly Sprite sprite in _spriteList.AsSpan()) {
                if(drawCount > _batchSize) {
                    break;
                }
                //We're going to assume a few things for the moment.              
                float[] verts = {
                    0.5f,  0.5f, 0.0f, 1.0f, 1.0f, sprite.Color.R, sprite.Color.G, sprite.Color.B,
                    0.5f, -0.5f, 0.0f, 1.0f, 0.0f, sprite.Color.R, sprite.Color.G, sprite.Color.B
                   -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, sprite.Color.R, sprite.Color.G, sprite.Color.B
                   -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, sprite.Color.R, sprite.Color.G, sprite.Color.B
                };
                //Create vertex buffer object
                VertexBuffer vertexBuffer = new(verts);
                //Define our layout
                BufferLayout bufferLayout = new();
                bufferLayout.Add<float>(3);
                bufferLayout.Add<float>(2);
                bufferLayout.Add<float>(2);
                _vertexArray.AddBuffer(vertexBuffer, bufferLayout);
                _shader.Use();
                sprite.Texture.Use();
                _indexBuffer = new(_indices);
                //Add the vertex buffers
                drawCount++;
            }
            _vertexArray.Bind();
            _shader.Use();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length * drawCount, DrawElementsType.UnsignedInt, 0);
            Array.Clear(_spriteList);
            _cursor = 0;
        }
    }
}
