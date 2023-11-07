using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Model.Sources.Model
{
    public class PhysicsRouter
    {
        private Collisions _collisions = new Collisions();

        private readonly Func<IEnumerable<TargetRecord>> _recordsProvider;

        public PhysicsRouter(Func<IEnumerable<TargetRecord>> recordsProvider) => 
            _recordsProvider = recordsProvider;

        public void TryAddCollision(object modelA, object modelB) => 
            _collisions.TryBind(modelA, modelB);

        public void Step()
        {
            foreach (var pair in _collisions.Pairs)
                TryRoute(pair);

            _collisions = new Collisions();
        }

        public void TryRoute((object, object) pair)
        {
            IEnumerable<TargetRecord> records = _recordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
                ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
        }
    }
}