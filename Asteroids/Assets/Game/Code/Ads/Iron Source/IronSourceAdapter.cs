using Ads;
using Analytics;
using com.unity3d.mediation;
using System;
using UnityEngine;
using Zenject;

namespace AdsAdapterIronSource
{
    public class IronSourceAdapter : AdsAdapter, IDisposable
    {
        private LevelPlayBannerAd _bannerAd;
        private LevelPlayInterstitialAd _interstitialAd;
        private LevelPlayRewardedAd _rewardAd;
        private AnalyticsService _analyticsService;

        private const string AppKey = "85460dcd";
        private const string BannerAdUnitId = "thnfvcsog13bhn08";
        private const string InterstitialAdUnitId = "aeyqi3vqlv6o8sh9";
        private const string RewardAdUnitId = "aeyqi3vqlv6o8sh9";

        [Inject]
        public IronSourceAdapter(AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public override void Init()
        {
            IronSource.Agent.validateIntegration();
            LevelPlay.OnInitSuccess += OnInit;
            LevelPlay.Init(AppKey, adFormats: new[] { LevelPlayAdFormat.BANNER, LevelPlayAdFormat.INTERSTITIAL, LevelPlayAdFormat.REWARDED });
        }

        public override void ShowInterstitial()
        {
#if UNITY_EDITOR
            Debug.Log("IronSource Editor: Show Interstitial Complete.");
            return;
#endif
            _interstitialAd.LoadAd();

            if (_interstitialAd.IsAdReady())
            {
                _interstitialAd.ShowAd();
                _analyticsService.SendProgress("ad", "interstitial", 1);
                Debug.Log("IronSource: Show Interstitial Complete.");
            }
            else
            {
                _analyticsService.SendProgress("ad", "interstitial", 0);
                Debug.Log("IronSource: Show Interstitial Error.");
            }
        }

        public override void ShowReward(Action completeCallback)
        {
#if UNITY_EDITOR
            completeCallback?.Invoke();
            Debug.Log("IronSource Editor: Show Reward Complete.");
            return;
#endif

            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                _rewardAd.ShowAd();
                completeCallback?.Invoke();
                _analyticsService.SendProgress("ad", "reward", 1);
                Debug.Log("IronSource: Show Reward Complete.");
            }
            else
            {
                _analyticsService.SendProgress("ad", "reward", 1);
                Debug.Log("IronSource: Show Reward Error.");
            }
        }

        public override void ShowBanner(bool enable)
        {
#if UNITY_EDITOR
            Debug.Log("IronSource Editor: Show Banner = " + enable + ".");
            return;
#endif

            if (enable)
                _bannerAd.LoadAd();
            else
                _bannerAd.HideAd();

            _analyticsService.SendProgress("ad", "banner", enable ? 1 : 0);
            Debug.Log("IronSource: Show Banner = " + enable + ".");
        }

        public override void OnPause(bool isPaused)
        {
#if UNITY_EDITOR
            Debug.Log("IronSource: OnApplicationPause = " + isPaused + ".");
            return;
#endif

            _analyticsService.SendProgress("ad", "pause", isPaused ? 1 : 0);
            Debug.Log("IronSource: OnApplicationPause = " + isPaused + ".");
            IronSource.Agent.onApplicationPause(isPaused);
        }

        public void Dispose()
        {
            _bannerAd?.DestroyAd();
            _interstitialAd?.DestroyAd();
            _rewardAd?.DestroyAd();
        }

        private void InitAd()
        {
            _bannerAd = new LevelPlayBannerAd(BannerAdUnitId);
            _interstitialAd = new LevelPlayInterstitialAd(InterstitialAdUnitId);
            _rewardAd = new LevelPlayRewardedAd(RewardAdUnitId);
        }

        private void OnInit(LevelPlayConfiguration config)
        {
            InitAd();
            ShowBanner(true);
            OnPause(false);
            Debug.Log("Init");
        }
    }
}
