using UnityEngine;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    public class PlayBadgesMusic : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            soundManager.StopEffect();
            soundManager.StopVoice();
            if (soundManager.CurrentMusic == soundManager.MusicBadges)
                return;

            soundManager.PlayMusic(soundManager.MusicBadges);
        }

        #endregion
    }
}
