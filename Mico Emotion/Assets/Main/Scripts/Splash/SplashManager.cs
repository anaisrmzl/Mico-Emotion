using UnityEngine;

using Utilities.Scenes;
using Zenject;

namespace Emotion.Splash
{
    public class SplashManager : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SplashModule splashModule = null;

        [SerializeField] private DoubleClick doubleClick;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            doubleClick.doubleClicked += GoToFirstScene;
        }

        private void OnDestroy()
        {
            doubleClick.doubleClicked -= GoToFirstScene;
        }

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
            FadeSceneChanger.ChangeScene(SceneNames.Screen1);
        }

        #endregion
    }
}
