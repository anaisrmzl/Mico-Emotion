using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

namespace Emotion.Splash
{
    public class SplashManager : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SplashModule splashModule = null;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            StartSplash();
        }

        private void StartSplash()
        {
            splashModule.StartSplash(GoToFirstScene);
        }

        private void GoToFirstScene()
        {
            SceneManager.LoadScene(SceneNames.Screen1);
        }

        #endregion
    }
}
