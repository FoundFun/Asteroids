using UnityEngine.SceneManagement;
using Utilities;

namespace SceneBehaviour
{
    public sealed class SceneLoader
    {
        private AsyncMethod _asyncMethod;

        public void LoadScene(int index, int delay)
        {
            _asyncMethod = new AsyncMethod(delay, true, null, null, () =>
            {
                SceneManager.LoadScene(index);
            });

            _asyncMethod.Run();
        }

        public void RestartScene(int delay)
        {
            _asyncMethod = new AsyncMethod(delay, true, null, null, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });

            _asyncMethod.Run();
        }
    }
}
