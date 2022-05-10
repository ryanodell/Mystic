namespace MysticEngineTK.Core.Rendering {
    public class ShaderException : Exception {
        public string VertexShaderError { get; set; }
        public string FragmentShaderError { get; set; }
        public ShaderException(string vertexShaderError, string fragmentShaderError) 
            : base( vertexShaderError + Environment.NewLine + fragmentShaderError) {
            VertexShaderError = vertexShaderError;
            FragmentShaderError = fragmentShaderError;
        }
    }
}
