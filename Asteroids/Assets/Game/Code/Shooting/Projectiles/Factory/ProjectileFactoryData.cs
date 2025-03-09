using FactoryService;
using UnityEngine;

namespace ShootingBehaviour.Projectile
{
    [System.Serializable]
    public sealed class ProjectileFactoryData : FactoryData<Projectile>
    {
        public ProjectileFactoryData()
        {
            onInstantiate = OnInstantiate;
            onGet = OnGet;
            onReturn = OnReturn;
        }

        private void OnInstantiate(Projectile projectile)
        {
            if (projectile != null)
            {
                projectile.gameObject.SetActive(false);
            }
        }

        private void OnGet(Projectile projectile, Vector3 position)
        {
            if (projectile != null)
            {
                projectile.transform.position = position;
                projectile.gameObject.SetActive(true);
            }
        }

        private void OnReturn(Projectile projectile)
        {
            if (projectile != null)
            {
                projectile.gameObject.SetActive(false);
            }
        }
    }
}
