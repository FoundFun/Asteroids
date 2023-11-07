using System.Collections.Generic;

namespace Asteroids.Model.Sources.Model
{
    public class Collisions
    {
        private List<(object, object)> _pairs = new List<(object, object)>();

        public IEnumerable<(object, object)> Pairs => _pairs;

        public void TryBind(object a, object b)
        {
            foreach (var (left, right) in _pairs)
            {
                if (left == a && right == b)
                    return;

                if (left == b && right == a)
                    return;
            }

            _pairs.Add((a, b));
        }
    }
}