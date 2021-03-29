using UnityEngine;

namespace BlastGame.Utils
{
    public class CameraAdjust : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Camera _camera;

        void Start()
        {
            Adjust();
        }

        public void Adjust()
        {
            SetPosition();
            SetSize();
        }

        private void SetPosition()
        {
            Vector3 position = _spriteRenderer.bounds.center;
            position.z = transform.position.z;
            transform.position = position;
        }

        private void SetSize()
        {
            float levelAspect = _spriteRenderer.bounds.size.x / _spriteRenderer.bounds.size.y;

            if (levelAspect <= _camera.aspect)
            {
                _camera.orthographicSize = _spriteRenderer.bounds.extents.y;
            }
            else
            {
                _camera.orthographicSize = _spriteRenderer.bounds.size.x * Screen.height / Screen.width * 0.5f;
            }
        }
    }
}