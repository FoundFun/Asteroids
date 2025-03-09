using Zenject;

namespace Analytics
{
    public class AnalyticsService
    {
        AnalyticsAdapter _analyticsAdapter;

        [Inject]
        public AnalyticsService(AnalyticsAdapter analyticsAdapter)
        {
            _analyticsAdapter = analyticsAdapter;
            Init();
        }

        public void Login()
        {
            _analyticsAdapter.Login();
        }

        public void SendProgress(string eventName, string parameterName, double parameterValue)
        {
            _analyticsAdapter.SendProgress(eventName, parameterName, parameterValue);
        }

        public void SendScore(int value)
        {
            _analyticsAdapter.SendScore(value);
        }

        public void SendGroupJoin(string eventName)
        {
            _analyticsAdapter.SendGroupJoin(eventName);
        }

        public void SendLevelUp(double value)
        {
            _analyticsAdapter.SendLevelUp(value);
        }

        public void Reset()
        {
            _analyticsAdapter.Reset();
        }

        public string DisplayID()
        {
            return _analyticsAdapter.DisplayID();
        }

        private void Init()
        {
            _analyticsAdapter.Init();
        }
    }
}
