﻿using UnityEngine;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    public class PlayJungleBackground : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            if (soundManager.CurrentMusic == soundManager.MusicJungle)
                return;

            soundManager.PlayMusic(soundManager.MusicJungle);
        }

        #endregion
    }
}
