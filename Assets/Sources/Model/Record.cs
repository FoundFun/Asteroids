using System;

namespace Asteroids.Model.Sources.Model
{
    public sealed class Record<T1, T2> : TargetRecord
    {
        public readonly Action<T1, T2> Action;

        public Record(Action<T1, T2> action) => 
            Action = action;

        public void Do(T1 a, T2 b) => 
            Action(a, b);

        public void Do(T2 b, T1 a) => 
            Action(a, b);

        public override bool IsTarget((object, object) pair)
        {
            if (pair.Item1 is T1 && pair.Item2 is T2)
                return true;

            if (pair.Item1 is T2 && pair.Item2 is T1)
                return true;

            return false;
        }
    }
}