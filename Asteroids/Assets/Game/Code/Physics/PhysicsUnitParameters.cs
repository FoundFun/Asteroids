using UniRx;
using UnityEngine;

namespace PhysicsBehaviour
{
    [System.Serializable]
    public sealed class PhysicsUnitParameters
    {
        public float bounceTimer;
        public float angularVelocity;
        public Vector2 newPosition;
        public Vector2 reflectionVector;
        public Vector2 reflectionNormalVector;
        public ReactiveProperty<Vector2> currentVelocity = new ReactiveProperty<Vector2>();
        public ReactiveProperty<Vector2> currentPosition = new ReactiveProperty<Vector2>();
        public ReactiveProperty<float> currentAngle = new ReactiveProperty<float>();
    }
}
