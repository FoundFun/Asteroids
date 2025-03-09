using FactoryService;
using UnityEngine;

namespace AI
{
    [System.Serializable]
    public sealed class EnemyFactoryData : FactoryData<BaseEnemy>
    {
        public EnemyFactoryData()
        {
            onInstantiate = OnInstantiate;
            onGet = OnGet;
            onReturn = OnReturn;
        }

        private void OnInstantiate(BaseEnemy enemy)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void OnGet(BaseEnemy enemy, Vector3 position)
        {
            if (enemy != null)
            {
                enemy.transform.position = position;
                enemy.gameObject.SetActive(true);
            }
        }

        private void OnReturn(BaseEnemy enemy)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
}
