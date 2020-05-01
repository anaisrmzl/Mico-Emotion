using UnityEngine;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    public class PlayGameSong : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            soundManager.StopEffect();
            soundManager.StopVoice();
            if (soundManager.CurrentMusic == soundManager.MusicDiscover && soundManager.MusicIsPlaying)
                return;

            soundManager.PlayMusic(soundManager.MusicDiscover);
        }

        #endregion
    }
}
