using UnityEngine;

namespace Utilities.Static
{
    public static class VectorUtilities
    {
        public static Vector2 ScreenToWorldPoint(Vector2 screenPosition, Camera camera)
        {
            return camera.ScreenToWorldPoint(screenPosition);
        }
    }
}
