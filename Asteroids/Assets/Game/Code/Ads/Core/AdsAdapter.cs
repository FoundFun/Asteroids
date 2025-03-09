namespace Ads
{
    public abstract class AdsAdapter
    {
        public abstract void Init();
        public abstract void ShowInterstitial();
        public abstract void ShowReward(System.Action completeCallback);
        public abstract void ShowBanner(bool enable);
        public abstract void OnPause(bool isPaused);    
    }
}
