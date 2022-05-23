using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MysticEngineTK.Core.Cameras {
    public class OrthographicCamera {
        private Matrix4 _projectionMatrix;
        private Matrix4 _viewMatrix;
        private Matrix4 _viewProjectionMatrix;

        private Vector3 _position;
        private float _rotation = 0.0f;
        public OrthographicCamera(float left, float right, float bottom, float top) {
            _viewMatrix = Matrix4.Identity;
            _projectionMatrix = Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, -1.0f, 1.0f);
            _viewProjectionMatrix = _projectionMatrix * _viewMatrix;
        }

        private void _recalculateViewMatrix() {
            Matrix4 transform = Matrix4.CreateTranslation(_position);
            //Matrix4 rotation = Matrix4.CreateRotationZ(_rotation);
            _viewMatrix = Matrix4.Invert(transform);
            _viewProjectionMatrix = _projectionMatrix * _viewMatrix;
        }

        public Vector3 GetPosition() => _position;
        public float GetRotation() => _rotation;
        public void SetPosition(Vector3 position) { 
            _position = position; 
            _recalculateViewMatrix(); 
        }
        public void SetRotation(float rotation) => _rotation = rotation;
        public Matrix4 GetProjectionMatrix() => _projectionMatrix;
        public Matrix4 GetViewMatrix() => _viewMatrix;
        public Matrix4 GetProjectionViewMatrix() => _viewProjectionMatrix;
    }
}
