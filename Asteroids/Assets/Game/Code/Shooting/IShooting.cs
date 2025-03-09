using System.Collections.Generic;

namespace ShootingBehaviour
{
    public interface IShooting
    {
        Shooting Shooting { get; }
        Dictionary<string, System.Action> ShootingDictionary { get; }
    }
}
