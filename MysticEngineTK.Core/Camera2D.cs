using MysticEngineTK.Core.Management;
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
            float left = LookAtPosition.X - DisplayManager.Instance.GameWindow.Size.X / 2f;
            float right = LookAtPosition.X + DisplayManager.Instance.GameWindow.Size.X / 2f;
            float top = LookAtPosition.Y - DisplayManager.Instance.GameWindow.Size.Y / 2f;
            float bottom = LookAtPosition.Y + DisplayManager.Instance.GameWindow.Size.Y / 2f;

            Matrix4 orthoMatrix = Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, 1.01f, 100f);
            Matrix4 zoomMatrix = Matrix4.CreateScale(Zoom);

            return orthoMatrix * zoomMatrix;
        }

    }
}
