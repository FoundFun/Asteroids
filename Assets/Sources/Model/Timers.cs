using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Model.Sources.Model
{
    class Timers<T>
    {
        private List<Timer<T>> _timers = new List<Timer<T>>();

        public void Start(T context, float time, Action<T> onEnd) => 
            _timers.Add(new Timer<T>(context, time, onEnd));

        public void StopAll (T context) => 
            _timers.RemoveAll(timer => timer.Context.Equals(context));

        public void Tick(float deltaTime)
        {
            foreach (var timer in _timers.ToList())
            {
                timer.AccumulatedTime += deltaTime;
            
                if(timer.IsEnd)
                {
                    _timers.Remove(timer);
                    timer.OnEnd.Invoke(timer.Context);
                }
            }
        }

        public float GetAccumulatedTime(T context) => 
            _timers.First(timer => timer.Context.Equals(context)).AccumulatedTime;
    }
}