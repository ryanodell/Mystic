using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK.Core.Rendering {
    public class Shader {
        public int ProgramId { get; set; }
        public bool Compiled { get; private set; } = false;
        private ShaderProgramSource _shaderProgramSource { get; }
        private readonly IDictionary<string, int> _uniforms = new Dictionary<string, int>();
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
            if(Compiled) {
                Console.WriteLine("Already Compiled!");
                return false;
            }
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

            GL.GetProgram(ProgramId, GetProgramParameterName.ActiveUniforms, out var totalUniforms);
            for (int i = 0; i < totalUniforms; i++) {
                string key = GL.GetActiveUniform(ProgramId, i, out _, out _);
                int location = GL.GetUniformLocation(ProgramId, key);
                _uniforms.Add(key, location);
            }
            Compiled = true;
            return true;
        }

        public int GetUniformLocation(string uniformName) => _uniforms[uniformName];

        public void Use() {
            if(!Compiled) {
                CompileShader();
            }
            GL.UseProgram(ProgramId);
        }

        public int GetAttributeLocation(string attributeName) {
            return GL.GetAttribLocation(ProgramId, attributeName);
        }

        public void SetInt(string name, int data) {
            GL.UseProgram(ProgramId);
            GL.Uniform1(_uniforms[name], data);
        }

        public void SetFloat(string name, float data) {
            GL.UseProgram(ProgramId);
            GL.Uniform1(_uniforms[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data) {
            GL.UseProgram(ProgramId);
            GL.UniformMatrix4(_uniforms[name], true, ref data);
        }

        public void SetVector3(string name, Vector3 data) {
            GL.UseProgram(ProgramId);
            GL.Uniform3(_uniforms[name], data);
        }
    }
}