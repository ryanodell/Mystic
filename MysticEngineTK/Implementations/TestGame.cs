﻿using MysticEngineTK.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using MysticEngineTK.Core.Rendering;

namespace MysticEngineTK {
    public class TestGame : Game {
        public TestGame(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }

        private Shader _shader;

        private readonly float[] _vertices = {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        protected override void Initalize() {

        }

        protected override void LoadContent() {
            _shader = new(ShaderProgramSource.ParseShader("Resources/Shaders/Default.glsl"));
            bool result = _shader.CompileShader();
            if(!result) {
                Console.WriteLine("Failed to compile shader");
            }
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }
        protected override void Update(GameTime gameTIme) {

        }

        protected override void Render() {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

    }
}
