using UnityEngine;
using Utilities.Static;
using Zenject;

namespace StageBehaviour
{
    public sealed class StageGenerator
    {
        private int _size;
        private Transform _stageParent;
        private Sprite _background;
        private Stage _stage;

        private const int SortingOrder = -10;
        private const int OffsetSizeStage = 20;

        public Stage Stage => _stage;

        [Inject]
        public StageGenerator(JsonStageData jsonStageData, StageJsonObject stageJsonObject)
        {
            _size = jsonStageData.stageSize;
            _background = stageJsonObject.Background;
            _stage = GenerateStage();
        }

        private Stage GenerateStage()
        {
            InstantiateStageParent();
            Stage stage = InstantiateStage();
            InstantiateBackground();
            InstantiateWalls();
            InstantiateStageZoneCollider();
            return stage;
        }

        private Stage InstantiateStage()
        {
            return new Stage(_size, _stageParent);
        }

        private void InstantiateWalls()
        {
            InstantiateWall(new Vector3(_size / 2f, 0f), _stageParent, new Vector2(0.5f, _size), "Right Wall");
            InstantiateWall(new Vector3(-_size / 2f, 0f), _stageParent, new Vector2(0.5f, _size), "Left Wall");
            InstantiateWall(new Vector3(0f, _size / 2f), _stageParent, new Vector2(_size, 0.5f), "Up Wall");
            InstantiateWall(new Vector3(0f, -_size / 2f), _stageParent, new Vector2(_size, 0.5f), "Down Wall");
        }

        private void InstantiateWall(Vector3 position, Transform parent, Vector2 colliderSize, string name)
        {
            GameObject wall = new GameObject(name);
            wall.transform.position = position;
            wall.transform.rotation = Quaternion.identity;
            wall.transform.parent = parent;
            wall.layer = Layers.StageBorder;
            BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            collider.size = colliderSize;
        }

        private void InstantiateBackground()
        {
            GameObject background = new GameObject("Background");
            SpriteRenderer spriteRenderer = background.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _background;
            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.sortingOrder = SortingOrder;
            spriteRenderer.size = new Vector2(_size + OffsetSizeStage, _size + OffsetSizeStage);
        }

        private void InstantiateStageParent()
        {
            GameObject stageParent = new GameObject("Stage");
            stageParent.transform.position = Vector3.zero;
            _stageParent = stageParent.transform;
        }

        private void InstantiateStageZoneCollider()
        {
            GameObject colliderObject = new GameObject("Stage Zone Collider");
            colliderObject.transform.position = Vector3.zero;
            colliderObject.transform.SetParent(_stageParent);
            colliderObject.layer = Layers.StageZone;
            BoxCollider2D collider = colliderObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            collider.size = new Vector2(_size + OffsetSizeStage, _size + OffsetSizeStage);
        }
    }
}
