﻿namespace MysticEngineTK.Core.Rendering {
    public class ShaderProgramSource {
        public string VertexShaderSource;
        public string FragmentShaderSource;
        public ShaderProgramSource(string vertexShaderSource, string fragmentShaderSource) {
            VertexShaderSource = vertexShaderSource;
            FragmentShaderSource = fragmentShaderSource;
        }
    }
}
