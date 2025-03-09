namespace Analytics
{
    public abstract class AnalyticsAdapter
    {
        public abstract void Init();
        public abstract void Login();
        public abstract void SendProgress(string eventName, string parameterName, double parameterValue);
        public abstract void SendScore(int value);
        public abstract void SendGroupJoin(string eventName);
        public abstract void SendLevelUp(double value);
        public abstract void Reset();
        public abstract string DisplayID();
    }
}
