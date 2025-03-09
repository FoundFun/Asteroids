using FactoryService;
using UnityEngine;

namespace FxService
{
    [System.Serializable]
    public sealed class FxFactoryData : FactoryData<ParticleSystem>
    {
        public FxFactoryData()
        {
            onInstantiate = OnInstantiate;
            onGet = OnGet;
            onReturn = OnReturn;
        }

        private void OnInstantiate(ParticleSystem fx)
        {
            if (fx != null)
            {
                fx.gameObject.SetActive(false);
            }
        }

        private void OnGet(ParticleSystem fx, Vector3 position)
        {
            if (fx != null)
            {
                fx.transform.position = position;
                fx.gameObject.SetActive(true);
            }
        }

        private void OnReturn(ParticleSystem fx)
        {
            if (fx != null)
            {
                fx.gameObject.SetActive(false);
            }
        }
    }
}
