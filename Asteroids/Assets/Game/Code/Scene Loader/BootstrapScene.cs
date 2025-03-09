using UnityEngine;
using Zenject;

namespace SceneBehaviour
{
    public sealed class BootstrapScene : MonoBehaviour
    {
        [SerializeField] private int _indexGameScene = 1;
        [SerializeField] private int _delayLoadGameScene = 2;

        [Inject]
        public void Constructor(SceneLoader sceneLoader)
        {
            sceneLoader.LoadScene(_indexGameScene, _delayLoadGameScene);
        }
    }
}
