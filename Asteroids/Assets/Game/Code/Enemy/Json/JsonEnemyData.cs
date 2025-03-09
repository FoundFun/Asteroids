namespace AI
{
    [System.Serializable]
    public struct JsonEnemyData
    {
        public EnemyType EnemyType;
        public int health;
        public float maxSpeed;
        public float accelerationSpeed;
        public float speedRotation;
        public float accelerationRotation;
    }
}