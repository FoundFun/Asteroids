using System;
using UnityEngine;

namespace Asteroids.Model.Sources.Model.Simulations
{
    public class Trajectory : Transformable
    {
        private readonly float _speed;
        private readonly Vector2 _startPosition;
        private readonly Vector2 _direction;

        private readonly Func<Trajectory, float> _currentTimeProvider;
        
        public override Vector2 Position => _startPosition + _direction * _speed * _currentTimeProvider.Invoke(this);

        public Trajectory(float speed, Vector2 startPosition, Vector2 direction, Func<Trajectory, float> currentTimeProvider) 
            : base(startPosition, Vector2.SignedAngle(Vector3.up, direction))
        {
            _startPosition = startPosition;
            _direction = direction;
            _speed = speed;
            _currentTimeProvider = currentTimeProvider;
        }
    }
}