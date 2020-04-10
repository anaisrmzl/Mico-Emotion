using UnityEngine;
using System.Collections;

using Zenject;
using Utilities.Sound;

namespace Emotion.Sound
{
    public class PlayRandomAudio : MonoBehaviour
    {
        #region FIELDS

        private const string PlayingAudioMethod = "StartPlayingAudio";
        private const float WaitTime = 6.0f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip[] randomAudios;

        #endregion 

        #region BEHAVIORS

        private void Awake()
        {
            Invoke(PlayingAudioMethod, WaitTime);
        }

        private void StartPlayingAudio()
        {
            StartCoroutine(PlayAudio());
        }

        private IEnumerator PlayAudio()
        {
            int randomIndex = Random.Range(0, randomAudios.Length);
            soundManager.PlayVoice(randomAudios[randomIndex]);
            yield return new WaitForSeconds(randomAudios[randomIndex].length);
            Invoke(PlayingAudioMethod, WaitTime);
        }

        #endregion
    }
}
