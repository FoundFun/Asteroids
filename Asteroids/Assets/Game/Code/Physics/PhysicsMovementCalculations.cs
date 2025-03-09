using UnityEngine;
using Utilities.Static;

namespace PhysicsBehaviour
{
    public sealed class PhysicsMovementCalculations
    {
        private const float MinValueDirection = 0.01f;
        private const int ArrayCollidersSize = 10;

        public event System.Action<PhysicsSettings> onBounce;

        public void Move(Vector2 direction, Rigidbody2D rb, Collider2D collider, ref PhysicsUnitParameters physicsUnitParameters, ref PhysicsSettings physicsSettings)
        {
            UpdatePosition(direction, rb, ref physicsUnitParameters, physicsSettings);
            UpdateRotation(direction, rb, ref physicsUnitParameters, physicsSettings);
            CheckCollisionsBounce(rb, collider, ref physicsUnitParameters, physicsSettings);
        }

        private void UpdatePosition(Vector2 direction, Rigidbody2D rb, ref PhysicsUnitParameters physicsUnitParameters, PhysicsSettings physicsSettings)
        {
            if (physicsSettings.isMove == false) return;

            if (direction.magnitude > MinValueDirection)
            {
                Vector2 targetVelocity = direction.normalized * physicsSettings.maxSpeed;
                physicsUnitParameters.currentVelocity.Value = Vector2.MoveTowards(physicsUnitParameters.currentVelocity.Value, targetVelocity, physicsSettings.acceleration * Time.deltaTime);
            }
            else
            {
                physicsUnitParameters.currentVelocity.Value = Vector2.MoveTowards(physicsUnitParameters.currentVelocity.Value, Vector2.zero, physicsSettings.deceleration * Time.deltaTime);
            }

            physicsUnitParameters.newPosition = rb.position + physicsUnitParameters.currentVelocity.Value * Time.deltaTime;
            physicsUnitParameters.currentPosition.Value = rb.transform.position;
            rb.MovePosition(physicsUnitParameters.newPosition);
        }

        private void UpdateRotation(Vector2 direction, Rigidbody2D rb, ref PhysicsUnitParameters physicsUnitParameters, PhysicsSettings physicsSettings)
        {
            if (physicsSettings.isRotate == false) return;

            float toAngle;

            if (direction.magnitude > MinValueDirection)
            {
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                float smoothedAngle = Mathf.LerpAngle(rb.rotation, targetAngle, physicsSettings.rotationSpeed * Time.deltaTime);
                float angleDifference = Mathf.DeltaAngle(rb.rotation, targetAngle);
                physicsUnitParameters.angularVelocity = Mathf.MoveTowards(physicsUnitParameters.angularVelocity, angleDifference, physicsSettings.rotationAcceleration * Time.deltaTime);
                toAngle = smoothedAngle;
            }
            else
            {
                physicsUnitParameters.angularVelocity = Mathf.MoveTowards(physicsUnitParameters.angularVelocity, 0f, physicsSettings.rotationDeceleration * Time.deltaTime);
                float targetAngle = rb.rotation + physicsUnitParameters.angularVelocity * Time.deltaTime;
                toAngle = targetAngle;
            }

            physicsUnitParameters.currentAngle.Value = rb.transform.eulerAngles.z;
            rb.MoveRotation(toAngle);
        }

        private void CheckCollisionsBounce(Rigidbody2D rb, Collider2D collider, ref PhysicsUnitParameters physicsUnitParameters, PhysicsSettings physicsSettings)
        {
            if (physicsUnitParameters.bounceTimer < physicsSettings.bounceDelay)
            {
                physicsUnitParameters.bounceTimer += Time.deltaTime;
            }
            else
            {
                if (physicsSettings.isBounce == false) return;
                if (collider.isTrigger) return;

                Collider2D[] colliders = new Collider2D[ArrayCollidersSize];
                int hitCount = collider.OverlapCollider(new ContactFilter2D(), colliders);
                bool isBounce = false;

                for (int i = 0; i < hitCount; i++)
                {
                    if (isBounce) break;

                    Collider2D targetCollider = colliders[i];

                    if (targetCollider == null || targetCollider == collider) continue;
                    if (MaskUtilities.IsLayerInMask(targetCollider.gameObject.layer, physicsSettings.bounceIgnoreMask)) continue;

                    Vector2 reflectionNormal = (rb.transform.position - targetCollider.transform.position).normalized;
                    Vector2 reflectionVelocity = (targetCollider.transform.position - rb.transform.position).normalized;
                    Vector2 reflection = Vector2.Reflect(reflectionVelocity, reflectionNormal);
                    Vector2 velocityReflection = reflection * physicsSettings.bounceFactor;
                    physicsUnitParameters.currentVelocity.Value = Vector2.MoveTowards(physicsUnitParameters.currentVelocity.Value, velocityReflection, physicsSettings.deceleration * physicsSettings.bounceFactor * Time.deltaTime);
                    isBounce = true;
                    physicsUnitParameters.bounceTimer = 0f;
                    physicsUnitParameters.reflectionVector = velocityReflection;
                    physicsUnitParameters.reflectionNormalVector = reflectionNormal;
                    onBounce?.Invoke(physicsSettings);
                }
            }
        }
    }
}
