using System;

namespace Asteroids.Model.Sources.Model
{
    public class Timer<T>
    {
        public float AccumulatedTime;
        public readonly float Time;
        public readonly T Context;
        public readonly Action<T> OnEnd;

        public bool IsEnd => Time <= AccumulatedTime;

        public Timer(T context, float time, Action<T> onEnd)
        {
            Time = time;
            Context = context;
            OnEnd = onEnd;
        }
    }
}