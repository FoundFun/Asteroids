namespace Asteroids.Model.Sources.Model
{
    public abstract class TargetRecord
    {
        public abstract bool IsTarget((object, object) pair);
    }
}