namespace MysticEngineTK.Core.Rendering {
    public class ShaderProgramSource {
        public string VertexShaderSource;
        public string FragmentShaderSource;
        public ShaderProgramSource(string vertexShaderSource, string fragmentShaderSource) {
            VertexShaderSource = vertexShaderSource;
            FragmentShaderSource = fragmentShaderSource;
        }

        public static ShaderProgramSource ParseShader(string filePath) {
            string[] shaderSource = new string[2];
            eShaderType shaderType = eShaderType.NONE;
            var allLines = File.ReadAllLines(filePath);
            for (int i = 0; i < allLines.Length; i++) {
                string current = allLines[i];
                if (current.ToLower().Contains("#shader")) {
                    if (current.ToLower().Contains("vertex")) {
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
