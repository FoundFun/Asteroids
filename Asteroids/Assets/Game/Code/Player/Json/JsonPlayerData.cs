namespace PlayerBehaviour
{
    [System.Serializable]
    public struct JsonPlayerData
    {
        public int health;
        public float maxSpeed;
        public float accelerationSpeed;
        public float speedRotation;
        public float accelerationRotation;
        public int laserCharges;
        public int laserReloadTime;
        public int bulletSpeed;
    }
}