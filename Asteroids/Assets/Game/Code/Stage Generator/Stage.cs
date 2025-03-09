using UnityEngine;

namespace StageBehaviour
{
    public sealed class Stage
    {
        private int _size;
        private Transform _stageParent;

        public int Size => _size;

        public Stage(int size, Transform stageParent)
        {
            _size = size;
            _stageParent = stageParent;
        }

        public Vector2 GetRandomPointOnBorder()
        {
            int side = Random.Range(0, 4);
            float halfSize = _size / 2f;
            float randomPosition = Random.Range(-halfSize, halfSize);

            Vector2 point = Vector2.zero;

            switch (side)
            {
                case 0:
                    point = new Vector2(_stageParent.position.x + randomPosition, _stageParent.position.y + halfSize);
                    break;
                case 1:
                    point = new Vector2(_stageParent.position.x + halfSize, _stageParent.position.y + randomPosition);
                    break;
                case 2:
                    point = new Vector2(_stageParent.position.x + randomPosition, _stageParent.position.y - halfSize);
                    break;
                case 3:
                    point = new Vector2(_stageParent.position.x - halfSize, _stageParent.position.y + randomPosition);
                    break;
            }

            return point;
        }

        public Vector2 GetOppositePointOnBorder(Vector2 currentPoint)
        {
            float halfSize = _size / 2f;
            float leftDistance = Mathf.Abs(currentPoint.x - (_stageParent.position.x - halfSize));
            float rightDistance = Mathf.Abs(currentPoint.x - (_stageParent.position.x + halfSize));
            float topDistance = Mathf.Abs(currentPoint.y - (_stageParent.position.y + halfSize));
            float bottomDistance = Mathf.Abs(currentPoint.y - (_stageParent.position.y - halfSize));
            float offset = 1f;

            if (leftDistance <= rightDistance && leftDistance <= topDistance && leftDistance <= bottomDistance)
            {
                return new Vector2(_stageParent.position.x + halfSize, currentPoint.y) + new Vector2(-offset, 0);
            }
            else if (rightDistance <= leftDistance && rightDistance <= topDistance && rightDistance <= bottomDistance)
            {
                return new Vector2(_stageParent.position.x - halfSize, currentPoint.y) + new Vector2(offset, 0);
            }
            else if (topDistance <= leftDistance && topDistance <= rightDistance && topDistance <= bottomDistance)
            {
                return new Vector2(currentPoint.x, _stageParent.position.y - halfSize) + new Vector2(0, offset);
            }
            else
            {
                return new Vector2(currentPoint.x, _stageParent.position.y + halfSize) + new Vector2(0, -offset);
            }
        }

        public Vector2 GetRandomPointInStage()
        {
            float halfSize = _size / 2f;
            float randomX = Random.Range(_stageParent.position.x - halfSize, _stageParent.position.x + halfSize);
            float randomY = Random.Range(_stageParent.position.y - halfSize, _stageParent.position.y + halfSize);
            return new Vector2(randomX, randomY);
        }

        public Vector2 GetCenterStage()
        {
            return _stageParent.position;
        }
    }
}
