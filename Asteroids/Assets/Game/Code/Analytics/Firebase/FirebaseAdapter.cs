using Analytics;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AnalyticsAdapterFirebase
{
    public class FirebaseAdapter : AnalyticsAdapter
    {
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;
        private bool _firebaseInitialized = false;

        public override void Init()
        {
#if UNITY_EDITOR
            Debug.Log("Firebase Editor: Init.");
#endif

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                _dependencyStatus = task.Result;

                if (_dependencyStatus == DependencyStatus.Available)
                {
                    Debug.Log("Firebase: Enabling data collection.");
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    Debug.Log("Firebase: Set user properties.");
                    FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod, "Google");
                    FirebaseAnalytics.SetUserId("uber_user_510");
                    FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
                    _firebaseInitialized = true;
                }
                else
                {
                    Debug.LogError("Firebase: Could not resolve all Firebase dependencies: " + _dependencyStatus + ".");
                }
            });
        }

        public override void Login()
        {
            if (IsInit() == false) return;

            Debug.Log("Firebase: Logging a login event.");
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
        }

        public override void SendProgress(string eventName, string parameterName, double parameterValue)
        {
            if (IsInit() == false) return;

            Debug.Log("Firebase: Logging a progress event. " + eventName + ", " + parameterName + ", " + parameterValue + ".");
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
        }

        public override void SendScore(int value)
        {
            if (IsInit() == false) return;

            Debug.Log("Firebase: Logging a post-score event. " + value + ".");
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPostScore, FirebaseAnalytics.ParameterScore, value);
        }

        public override void SendGroupJoin(string eventName)
        {
            if (IsInit() == false) return;

            Debug.Log("Firebase: Logging a group join event. " + eventName + ".");
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventJoinGroup, FirebaseAnalytics.ParameterGroupId, eventName);
        }

        public override void SendLevelUp(double value)
        {
            if (IsInit() == false) return;

            Debug.Log("Firebase: Logging a level up event. " + value + ".");
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, new Parameter(FirebaseAnalytics.ParameterLevel, value));
        }

        public override void Reset()
        {
            if (IsInit() == false) return;

            Debug.Log("Firebase: Reset analytics data." + ".");
            FirebaseAnalytics.ResetAnalyticsData();
        }

        public override string DisplayID()
        {
            if (IsInit() == false) return "-1";

            Task<string> task = FirebaseAnalytics.GetAnalyticsInstanceIdAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Firebase: App instance ID fetch was canceled.");
                }
                else if (task.IsFaulted)
                {
                    Debug.Log("Firebase: " + String.Format("Encounted an error fetching app instance ID {0}", task.Exception.ToString()) + ".");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("Firebase: " + String.Format("App instance ID: {0}", task.Result) + ".");
                }
                return task;
            }).Unwrap();

            return task.Result;
        }

        private bool IsInit()
        {
            if (_firebaseInitialized == false)
            {
                Debug.LogWarning("Firebase: Not Init");
            }

            return _firebaseInitialized;
        }
    }
}
