using UnityEngine;

namespace Utilities.Components
{
    [RequireComponent(typeof(TrailRenderer))]
    public sealed class SwitchTrailRenderer : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private void OnEnable()
        {
            SetEnable(true);
        }

        private void OnDisable()
        {
            SetEnable(false);
        }

        private void SetEnable(bool enable)
        {
            if (enable)
            {
                _trailRenderer.Clear();
                _trailRenderer.emitting = true;
            }
            else
            {
                _trailRenderer.emitting = false;
                _trailRenderer.Clear();
            }
        }
    }
}
