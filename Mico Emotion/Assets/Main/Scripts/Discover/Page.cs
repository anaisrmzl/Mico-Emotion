using UnityEngine;

using Utilities.Sound;
using Zenject;

namespace Emotion.Discover
{
    public abstract class Page : MonoBehaviour
    {
        #region FIELDS

        [Inject] protected SoundManager soundManager;

        [SerializeField] protected AudioClip narrativeAudio;

        #endregion

        #region PROPERTIES

        public virtual float PageLength { get => narrativeAudio.length + 1.0f; }

        #endregion

        #region BEHAVIORS

        private void OnEnable()
        {
            Initialize();
        }

        public abstract void Initialize();

        #endregion
    }
}
