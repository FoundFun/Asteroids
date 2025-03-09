using JsonService;
using UnityEngine;

namespace StageBehaviour
{
    [CreateAssetMenu(fileName = "Stage Json", menuName = "Game/Stage Json")]
    public sealed class StageJsonObject : JsonObject<JsonStageData>
    {
        [SerializeField] private Sprite _background;

        public Sprite Background => _background;

        [ContextMenu("Import")]
        public override void Import()
        {
            base.Import();
        }

        [ContextMenu("Export")]
        public override void Export()
        {
            base.Export();
        }
    }
}
