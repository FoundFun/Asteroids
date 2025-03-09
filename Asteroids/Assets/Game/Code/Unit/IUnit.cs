using HealthSystem;
using PhysicsBehaviour;

namespace Unit
{
    public interface IUnit
    {
        Health Health { get; }
        PhysicsUnit PhysicsUnit { get; }
    }
}
