using UnityEngine;

namespace PhysicsBehaviour
{
    [System.Serializable, CreateAssetMenu(fileName = "Physics Settings", menuName = "Game/Physics Settings")]
    public sealed class PhysicsSettings : ScriptableObject
    {
        [Header("Settings")]
        public bool isRotate = true;
        public bool isMove = true;
        public bool isBounce = true;
        [Header("Move")]
        public float maxSpeed = 50f;
        public float acceleration = 5f;
        public float deceleration = 2.5f;
        [Header("Rotate")]
        public float rotationSpeed = 2.5f;
        public float rotationAcceleration = 100f;
        public float rotationDeceleration = 50f;
        [Header("Bounce")]
        public float bounceDelay = 0.25f;
        public float bounceFactor = 0.8f;
        public LayerMask bounceIgnoreMask;
    }
}