﻿using MysticEngineTK.Core;
using MysticEngineTK.Core.Management;
using MysticEngineTK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Drawing;

namespace MysticEngineTK {
    internal class UsingVertexStruct : Game {
        public UsingVertexStruct(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }
        private readonly float[] _vertices = {
             //Positions            TexCoords       Color                   TexSlot
             0.5f,  0.5f, 0.0f,     1.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f, // top right      //4 : 0
             0.5f, -0.5f, 0.0f,     1.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom right   //5 : 1
            -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom left    //6 : 2
            -0.5f,  0.5f, 0.0f,     0.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f  // top left       //7 : 3
        };

        //private float[] _vertices = new float[4 * 10];

        private Vertex[] _vetexVerts = new Vertex[4];

        private readonly uint[] _indices = {
            0, 1, 3,    1, 2, 3
        };

        private VertexBuffer _vertexBuffer;
        private VertexArray _vertexArray;
        private IndexBuffer _indexBuffer;

        Shader _shader;
        Matrix4 _projectionMatrix;

        protected override void Initalize() {

        }

        protected override void LoadContent() {

            //private readonly float[] _vertices = {
            //     //Positions            TexCoords       Color                   TexSlot
            //     0.5f,  0.5f, 0.0f,     1.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f, // top right      //4 : 0
            //     0.5f, -0.5f, 0.0f,     1.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom right   //5 : 1
            //    -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,     1.0f, 1.0f, 1.0f,       0.0f, // bottom left    //6 : 2
            //    -0.5f,  0.5f, 0.0f,     0.0f, 1.0f,     1.0f, 1.0f, 1.0f,       0.0f  // top left       //7 : 3
            //};

            _vertexArray = new();
            Color4 color = Color4.White;
            Vertex _01 = new Vertex {
                Position = new[] { 0.5f, 0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { 1.0f, 1.0f },
                TexId = 0f
            };
            Vertex _02 = new Vertex {
                Position = new[] { 0.5f, -0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { 1.0f, 0.0f },
                TexId = 0f
            };
            Vertex _03 = new Vertex {
                Position = new[] { -0.5f, -0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { 0.0f, 0.0f },
                TexId = 0f
            };
            Vertex _04 = new Vertex {
                Position = new[] { -0.5f, 0.5f, 0.0f },
                Color = new[] { color.R, color.G, color.B },
                TexCoords = new[] { 0.0f, 1.0f },
                TexId = 0f
            };
            _vetexVerts[0] = _01;
            _vetexVerts[1] = _02;
            _vetexVerts[2] = _03;
            _vetexVerts[3] = _04;

            _vertexBuffer = new(4);
            BufferLayout bufferLayout = new();
            //Positions
            bufferLayout.Add<float>(3);
            //Texture Coordinates
            bufferLayout.Add<float>(2);
            //Colors:
            bufferLayout.Add<float>(3);
            //Texture Slot:
            bufferLayout.Add<float>(1);

            _vertexArray.AddBuffer(_vertexBuffer, bufferLayout);
            _shader = new(ShaderProgramSource.ParseShader("Resources/Shaders/MatrixTextureSlotColor.glsl"));
            bool result = _shader.CompileShader();
            if (result) {
                _shader.Use();
            }
            _indexBuffer = new(_indices);

            var textureSamplerUniformLocation = _shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSamplerUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Resources/Textures/Objects_v2.png");

            _projectionMatrix = Matrix4.CreateOrthographicOffCenter(-2.0f, 2.0f, -1.5f, 1.5f, -1.0f, 1.0f);
            _projectionMatrix += Matrix4.CreateScale(2.5f);

            _shader.SetMatrix4("u_Projection", _projectionMatrix);

            //_vertexBuffer.WriteData(_vetexVerts.AsSpan<float>());

        }
        protected override void Update(GameTime gameTime) {
            //_projectionMatrix = Matrix4.CreateOrthographicOffCenter(0, DisplayManager.Instance.GameWindow.Size.X, 0, DisplayManager.Instance.GameWindow.Size.Y, -1.0f, 1.0f);
            //_projectionMatrix = Matrix4.CreateOrthographicOffCenter(-2.0f, 2.0f, -1.5f, 1.5f, -1.0f, 1.0f);
            //_projectionMatrix += Matrix4.CreateScale(2.5f);
            //float tmp = _vertices[0];
            //tmp += .005f;
            //_vertices[0] = tmp;

            //_projectionMatrix *= _viewMatrix;

            //_vertices[3] = _vertices[3] - 0.005f;
            //_vertices[4] = _vertices[4] - 0.005f;           
            //_vertex.Position = new float[3] { 0.5f, 0.5f, 0.0f };
            for(int i = 0; i < _vertices.Length; i++) {
                Console.Write($"{_vertices[i]}, ".TrimEnd(','));
            }
            Console.WriteLine(string.Empty);
            Array.Clear(_vertices);
            int cursor = 0;
            foreach(Vertex vert in _vetexVerts.AsEnumerable()) {
                _vertices[cursor++] = vert.Position[0];
                _vertices[cursor++] = vert.Position[1];
                _vertices[cursor++] = vert.Position[2];
                _vertices[cursor++] = vert.TexCoords[0];
                _vertices[cursor++] = vert.TexCoords[1];
                _vertices[cursor++] = vert.Color[0];
                _vertices[cursor++] = vert.Color[1];
                _vertices[cursor++] = vert.Color[2];
                _vertices[cursor++] = vert.TexId;
            }
            for (int i = 0; i < _vertices.Length; i++) {
                Console.Write($"{_vertices[i]}, ".TrimEnd(','));
            }
            _vertexBuffer.WriteData(_vertices);


        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.Black);
            _vertexArray.Bind();
            _shader.SetMatrix4("u_Projection", _projectionMatrix);
            _shader?.Use();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        private void _drawSprite(Texture2D texture, Vector2 position, Rectangle srcRect, float scale, float rotate, Color4 color) {

        }
    }
}
