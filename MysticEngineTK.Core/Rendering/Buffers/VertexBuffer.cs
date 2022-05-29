﻿using OpenTK.Graphics.OpenGL4;

namespace MysticEngineTK.Core.Rendering {
    public class VertexBuffer : IBuffer {
        public int BufferId { get; }
        public bool Dynamic { get; }
        public VertexBuffer(float[] vertices) {
            BufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            Dynamic = false;
        }

        public void Bind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
        }

        public void Unbind() {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
