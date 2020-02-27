using UnityEngine;
using UnityEngine.Events;

namespace Emotion.Splash
{
    public class SplashModule : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private GameObject container = null;

        private ISplashScreen splashScreen = null;
        private UnityAction onSplashEnded = null;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            splashScreen = GetComponentInChildren<ISplashScreen>();
        }

        public void StartSplash(UnityAction onSplashEnd)
        {
            this.onSplashEnded = onSplashEnd;
            splashScreen.Appear();
        }

        public void EndSplash()
        {
            container.SetActive(false);
            onSplashEnded?.Invoke();
        }

        #endregion
    }
}
