using UnityEngine;

namespace Utilities.Static
{
    public static class MaskUtilities
    {
        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }
    }
}
