using StageBehaviour;
using UnityEngine;
using Zenject;

namespace CameraBehaviour
{
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;

        private Vector2 _clampSize;
        private float _cameraHalfWidth;
        private float _cameraHalfHeight;

        private const float OffsetClampCamera = 2f;

        [Inject]
        public void Constructor(StageGenerator stageGenerator)
        {
            _clampSize = new Vector2(stageGenerator.Stage.Size - OffsetClampCamera, stageGenerator.Stage.Size - OffsetClampCamera);
        }

        private void Start()
        {
            ApplyCameraSettings();
        }

        private void Update()
        {
            FollowToTarget();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void FollowToTarget()
        {
            if (_target == null) return;

            transform.position = new Vector3(GetClampX(), GetClampY(), transform.position.z);
        }

        private float GetClampX()
        {
            Vector3 targetPosition = _target.position;
            Vector2 halfClamSize = _clampSize / 2f;

            float minX = -halfClamSize.x + _cameraHalfWidth;
            float maxX = halfClamSize.x - _cameraHalfWidth;

            if (minX > maxX)
            {
                float cacheMinX = minX;
                float cacheMaxX = maxX;
                minX = cacheMaxX;
                maxX = cacheMinX;
            }

            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            return clampedX;
        }

        private float GetClampY()
        {
            Vector3 targetPosition = _target.position;
            Vector2 halfClamSize = _clampSize / 2f;

            float minY = -halfClamSize.y + _cameraHalfHeight;
            float maxY = halfClamSize.y - _cameraHalfHeight;

            if (minY > maxY)
            {
                float cacheMinY = minY;
                float cacheMaxY = maxY;
                minY = cacheMaxY;
                maxY = cacheMinY;
            }

            float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);
            return clampedY;
        }

        private void ApplyCameraSettings()
        {
            _cameraHalfHeight = _camera.orthographicSize;
            _cameraHalfWidth = _cameraHalfHeight * _camera.aspect;
        }
    }
}
