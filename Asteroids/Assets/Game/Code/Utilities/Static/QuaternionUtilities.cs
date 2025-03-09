using UnityEngine;

namespace Utilities.Static
{
    public static class QuaternionUtilities
    {
        public static Quaternion CalculateRotation(Transform transformDirection)
        {
            Vector2 direction = transformDirection.right;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }
    }
}
