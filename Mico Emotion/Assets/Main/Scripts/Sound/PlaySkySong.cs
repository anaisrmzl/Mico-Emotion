﻿using UnityEngine;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    public class PlaySkySong : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            soundManager.StopEffect();
            soundManager.StopVoice();
            if (soundManager.CurrentMusic == soundManager.MusicSky)
                return;

            soundManager.PlayMusic(soundManager.MusicSky);
            soundManager.PlayVoice(soundManager.AudioTietoEmotions, true);
        }

        #endregion
    }
}