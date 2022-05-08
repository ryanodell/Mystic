namespace MysticEngineTK.Core.Rendering
{
    public enum eShaderType
    {
        NONE = -1, VERTEX = 0, FRAGMENT = 1
    }

    public class ShaderProgramSource
    {
        public string VertexShaderSource;
        public string FragmentShaderSource;
        public ShaderProgramSource(string vertexShaderSource, string fragmentShaderSource)
        {
            VertexShaderSource = vertexShaderSource;
            FragmentShaderSource = fragmentShaderSource;
        }
    }

    public class Shader
    {
        public int ProgramId { get; set; }

        public Shader() { }


    }
}