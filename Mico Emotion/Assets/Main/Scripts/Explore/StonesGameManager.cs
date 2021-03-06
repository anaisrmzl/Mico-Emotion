﻿using UnityEngine;
using System.Collections;

using Utilities.Scenes;
using Utilities.Sound;
using Zenject;

using Emotion.Data;

namespace Emotion.Explore
{
    public class StonesGameManager : MonoBehaviour
    {
        #region FIELDS

        private const int MaxFinishedToWin = 3;

        [Inject] private UserManager userManager;
        [Inject] private SoundManager soundManager;

        [SerializeField] private StonesManager blueManager;
        [SerializeField] private StonesManager pinkManager;
        [SerializeField] private StonesManager yellowManager;

        private int finishCounter = 0;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            userManager.UpdateCompletedStonesGame(false);
            blueManager.finished += CountFinished;
            pinkManager.finished += CountFinished;
            yellowManager.finished += CountFinished;
        }

        private void OnDestroy()
        {
            blueManager.finished -= CountFinished;
            pinkManager.finished -= CountFinished;
            yellowManager.finished -= CountFinished;
        }

        private void CountFinished(bool stauts)
        {
            if (!stauts)
                return;

            finishCounter++;
            if (finishCounter != MaxFinishedToWin)
                return;

            userManager.UpdateCompletedStonesGame(true);
            soundManager.StopEffect();
            soundManager.StopVoice();
            StartCoroutine(PlayWinAudio());
        }

        private IEnumerator PlayWinAudio()
        {
            soundManager.PlayEffect(soundManager.AudioWin);
            yield return new WaitForSeconds(soundManager.AudioWin.length);
            AnimationSceneChanger.ChangeScene(SceneNames.Meditation);
        }

        #endregion
    }
}
