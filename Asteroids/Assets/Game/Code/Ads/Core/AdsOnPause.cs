using UnityEngine;
using Zenject;

namespace Ads
{
    public class AdsOnPause : MonoBehaviour
    {
        private AdsService _adsService;

        [Inject]
        public void Constructor(AdsService adsService)
        {
            _adsService = adsService;
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationPause(bool pause)
        {
            _adsService.OnPause(pause);
        }
    }
}
