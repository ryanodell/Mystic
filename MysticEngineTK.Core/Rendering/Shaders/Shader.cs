using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace MysticEngineTK.Core.Rendering {
    public class Shader {
        public int ProgramId { get; set; }
        public bool Compiled { get; } = false;
        private ShaderProgramSource _shaderProgramSource { get; }
        public Shader(ShaderProgramSource shaderProgramSource, bool compile = false) {
            _shaderProgramSource = shaderProgramSource;
            if(compile) {
                CompileShader();
            }
        }

        /// <summary>
        /// This method compiles the shader.
        /// </summary>
        /// <returns></returns>
        public bool CompileShader() {
            if(_shaderProgramSource == null) {
                Console.WriteLine("Shader Source is null");
                return false;
            }
            string[] shaderExceptions = new string[2];
            int vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, _shaderProgramSource.VertexShaderSource);
            GL.CompileShader(vertexShaderId);
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if(vertexShaderCompilationCode != (int)All.True) {
                shaderExceptions[(int)eShaderType.VERTEX] = GL.GetShaderInfoLog(vertexShaderId);
            }
            int fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, _shaderProgramSource.FragmentShaderSource);
            GL.CompileShader(fragmentShaderId);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmentShadercompilationCode);
            if(fragmentShadercompilationCode != (int)All.True) {
                shaderExceptions[(int)eShaderType.FRAGMENT] = GL.GetShaderInfoLog(fragmentShaderId);
            }
            if (!string.IsNullOrEmpty(shaderExceptions[(int)eShaderType.VERTEX]) || !string.IsNullOrEmpty(shaderExceptions[(int)eShaderType.FRAGMENT])) {
                Console.WriteLine("One or more shaders was blank");
                return false;
            }

            ProgramId = GL.CreateProgram();
            GL.AttachShader(ProgramId, vertexShaderId);
            GL.AttachShader(ProgramId, fragmentShaderId);

            GL.LinkProgram(ProgramId);
            GL.DetachShader(ProgramId, vertexShaderId);
            GL.DetachShader(ProgramId, fragmentShaderId);
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            return true;
        }

        public void Use() {
            GL.UseProgram(ProgramId);
        }

        public static ShaderProgramSource ParseShader(string filePath) {
            string[] shaderSource = new string[2];
            eShaderType shaderType = eShaderType.NONE;
            var allLines = File.ReadAllLines(filePath);
            for(int i = 0; i < allLines.Length; i++) {
                string current = allLines[i];
                if(current.ToLower().Contains("#shader")) {
                    if(current.ToLower().Contains("vertex")) {
                        shaderType = eShaderType.VERTEX;
                    } else if (current.ToLower().Contains("fragment")) {
                        shaderType = eShaderType.FRAGMENT;
                    }
                } else {
                    shaderSource[(int)shaderType] += current + Environment.NewLine;
                }
            }
            return new ShaderProgramSource(shaderSource[(int)eShaderType.VERTEX], shaderSource[(int)eShaderType.FRAGMENT]);
        }
    }
}