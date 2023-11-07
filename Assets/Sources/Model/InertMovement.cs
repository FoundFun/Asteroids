using UnityEngine;

namespace Asteroids.Model.Sources.Model
{
    public class InertMovement
    {
        private const float UnitsPerSecond = 0.001f;
        private const float MaxSpeed = 0.0015f;
        private const float SecondsToStop = 1f;

        public Vector2 Acceleration { get; private set; }

        public void Accelerate(Vector2 forward, float deltaTime)
        {
            Acceleration += forward * (UnitsPerSecond * deltaTime);
            Acceleration = Vector2.ClampMagnitude(Acceleration, MaxSpeed);
        }

        public void Slowdown(float deltaTime) => 
            Acceleration -= Acceleration * (deltaTime / SecondsToStop);
    }
}