using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace MysticEngineTK.Core.Rendering {
    [StructLayout(LayoutKind.Sequential, Pack =1)]
    public struct Vertex {
        [MarshalAs(UnmanagedType.R4, SizeConst =3)]
        public float[] Position;

        [MarshalAs(UnmanagedType.R4, SizeConst = 4)]
        public float[] Color;

        [MarshalAs(UnmanagedType.R4, SizeConst = 2)]
        public float[] TexCoords;

        [MarshalAs(UnmanagedType.R4)]
        public float TexId;
    }
}
