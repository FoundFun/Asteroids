using UnityEngine;

namespace Utilities.Components
{
    public sealed class SpriteRendererFade : MonoBehaviour
    {
        private AsyncMethod _asyncMethod;

        private void OnDestroy()
        {
            if (_asyncMethod != null)
                _asyncMethod.Stop();
        }

        public void LoopFade(SpriteRenderer spriteRenderer, float time, float delay = 0.2f)
        {
            Color baseColor = Color.white;
            Color fadeColor = new Color(1f, 1f, 1f, 0.25f);
            bool isFade = false;

            AsyncMethod _asyncMethod = new AsyncMethod(delay, time, null, () =>
            {
                if (spriteRenderer != null)
                    spriteRenderer.color = isFade ? baseColor : fadeColor;

                isFade = !isFade;
            }, () =>
            {
                if (spriteRenderer != null)
                    spriteRenderer.color = baseColor;
            });

            _asyncMethod.Run();
        }
    }
}
