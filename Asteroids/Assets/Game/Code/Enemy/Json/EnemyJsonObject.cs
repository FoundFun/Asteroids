using JsonService;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(fileName = "Enemy Json", menuName = "Game/Enemy Json")]
    public sealed class EnemyJsonObject : JsonObject<JsonEnemyData>
    {
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
