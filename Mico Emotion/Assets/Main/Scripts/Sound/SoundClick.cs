using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    [RequireComponent(typeof(Button))]
    public class SoundClick : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        #endregion 

        #region BEHAVIORS

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(ClickSound);
        }

        private void ClickSound()
        {
            soundManager.PlayEffect(soundManager.AudioGeneral);
        }

        #endregion
    }
}
