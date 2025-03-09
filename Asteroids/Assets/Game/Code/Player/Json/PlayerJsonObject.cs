using JsonService;
using UnityEngine;

namespace PlayerBehaviour
{
    [CreateAssetMenu(fileName = "Player Json", menuName = "Game/Player Json")]
    public sealed class PlayerJsonObject : JsonObject<JsonPlayerData>
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
