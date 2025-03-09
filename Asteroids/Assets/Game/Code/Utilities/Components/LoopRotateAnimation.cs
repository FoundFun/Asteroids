using System.Threading;
using UnityEngine;

namespace Utilities.Components
{
    [RequireComponent(typeof(Transform))]
    public sealed class LoopRotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed = 15f;

        private AsyncMethod _asyncMethod;

        private void Awake()
        {
            _asyncMethod = new AsyncMethod(null, UpdateRotate, null);
        }

        private void Start()
        {
            _asyncMethod.Run();
        }

        private void OnDestroy()
        {
            _asyncMethod.Stop();
        }

        private void UpdateRotate()
        {
            transform.localEulerAngles += new Vector3(0f, 0f, 0.01f * _speed);
        }
    }
}
