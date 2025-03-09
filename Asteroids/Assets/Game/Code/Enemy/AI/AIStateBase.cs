namespace AI
{
    public class AIStateBase : IAIState
    {
        BaseEnemy _enemy;

        public AIStateBase(BaseEnemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
}