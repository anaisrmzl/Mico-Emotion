using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    [RequireComponent(typeof(Button))]
    public class SoundClickCustom : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip audioClick;

        #endregion 

        #region BEHAVIORS

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(ClickSound);
        }

        private void ClickSound()
        {
            soundManager.PlayEffect(audioClick);
        }

        #endregion
    }
}
