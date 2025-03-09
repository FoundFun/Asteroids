using Zenject;

namespace Ads
{
    public class AdsService
    {
        private AdsAdapter _adsAdapter;

        [Inject]
        public AdsService(AdsAdapter adsAdapter)
        {
            _adsAdapter = adsAdapter;
            Init();
        }

        public void ShowInterstitial()
        {
            _adsAdapter.ShowInterstitial();
        }

        public void ShowReward(System.Action completeCallback)
        {
            _adsAdapter.ShowReward(completeCallback);
        }

        public void ShowBanner(bool enable)
        {
            _adsAdapter.ShowBanner(enable);
        }

        public void OnPause(bool pause)
        {
            _adsAdapter.OnPause(pause);
        }

        private void Init()
        {
            _adsAdapter.Init();
        }
    }
}
