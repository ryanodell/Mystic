using OpenTK.Mathematics;

namespace MysticEngineTK.Core
{
    public class Camera2D
    {
        public Vector2 LookAtPosition { get; set; }
        public float Zoom { get; set; }

        public Camera2D(Vector2 lookAtPosition, float zoom)
        {
            LookAtPosition = lookAtPosition;
            Zoom = zoom;
        }

        public void LookAt(Vector2 position)
        {
            LookAtPosition = position;
        }

        public Matrix4 GetViewMatrix()
        {
            //float left = FocusPosition.X - DisplayManager.WindowSize.X / 2f;
            //float right = FocusPosition.X + DisplayManager.WindowSize.X / 2f;
            //float top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2f;
            //float bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2f;

            var orthoGraphicMatrix = Matrix4.CreateOrthographicOffCenter(1, 2, 3, 4, 1.01f, 100f);
            var zoomMatrix = Matrix4.CreateScale(Zoom);
            return orthoGraphicMatrix * zoomMatrix;
        }

    }
}
