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
        private const float WaitTime = 7.0f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip[] randomAudios;
        [SerializeField] private bool playOnStart = false;

        #endregion 

        #region BEHAVIORS

        private void Awake()
        {
            Invoke(PlayingAudioMethod, playOnStart ? 0.0f : WaitTime);
        }

        private void StartPlayingAudio()
        {
            StartCoroutine(PlayAudio());
        }

        private IEnumerator PlayAudio()
        {
            int randomIndex = Random.Range(0, randomAudios.Length);
            if (!(soundManager.VoiceIsPlaying || soundManager.EffectIsPlaying))
            {
                soundManager.PlayVoice(randomAudios[randomIndex]);
                yield return new WaitForSeconds(randomAudios[randomIndex].length);
            }

            yield return null;
            Invoke(PlayingAudioMethod, WaitTime);
        }

        #endregion
    }
}
