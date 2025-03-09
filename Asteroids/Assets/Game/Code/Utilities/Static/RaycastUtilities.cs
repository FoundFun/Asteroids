using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.Static
{
    public static class RaycastUtilities
    {
        public static bool IsTouchOverUI(Touch touch)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = touch.position
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Count > 0;
        }
    }
}
