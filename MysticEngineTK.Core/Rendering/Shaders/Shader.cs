using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace MysticEngineTK.Core.Rendering {
    public class Shader {
        public int ProgramId { get; set; }
        private ShaderProgramSource _shaderProgramSource { get; }
        public Shader(ShaderProgramSource shaderProgramSource) {
            _shaderProgramSource = shaderProgramSource;
        }

        public bool CompileShader() {
            if(_shaderProgramSource == null) {
                Debug.WriteLine("Shader Source is null");
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
            #region Messy
            if (shaderExceptions[(int)eShaderType.VERTEX] != string.Empty || shaderExceptions[(int)eShaderType.FRAGMENT] != string.Empty) {
#if DEBUG
                throw new ShaderException(shaderExceptions[(int)eShaderType.VERTEX], shaderExceptions[(int)eShaderType.FRAGMENT]);
#else
                return false;
#endif
            }
            #endregion

            ProgramId = GL.CreateProgram();
            GL.AttachShader(ProgramId, vertexShaderId);
            GL.AttachShader(ProgramId, fragmentShaderId);

            GL.LinkProgram(ProgramId);
            GL.DetachShader(ProgramId, vertexShaderId);
            GL.DetachShader(ProgramId, fragmentShaderId);
            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(vertexShaderId);

            return true;
        }

        public void Use() {
            GL.UseProgram(ProgramId);
        }

        public static async Task<ShaderProgramSource> ParseShader(string filePath) {
            string[] shaderSource = new string[2];
            eShaderType shaderType = eShaderType.NONE;
            var allLines = await File.ReadAllLinesAsync(filePath);
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