﻿using UnityEngine;

namespace Utilities.Sound
{
    public class SoundManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private AudioSource effectSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource voiceSource;

        #endregion

        #region PROPERTIES

        public float EffectLength { get => effectSource.clip.length; }
        public float MusicLength { get => musicSource.clip.length; }
        public float VoiceLength { get => voiceSource.clip.length; }
        public AudioSource VoiceSource { get => voiceSource; }
        public AudioSource MusicSource { get => musicSource; }
        public bool MusicIsPlaying { get => musicSource.isPlaying; }

        #endregion

        #region BEHAVIORS

        public void Awake()
        {
            effectSource.volume = 1;
            musicSource.volume = 1;
            voiceSource.volume = 1;
        }

        public void PlayEffect(AudioClip clip, bool isLoop = false)
        {
            effectSource.loop = isLoop;
            effectSource.clip = clip;
            effectSource.Play();
        }

        public void PlayEffectOneShot(AudioClip clip)
        {
            effectSource.PlayOneShot(clip);
        }

        public void SetEffectPitch(float pitch)
        {
            effectSource.pitch = pitch;
        }

        public void PlayMusic(AudioClip clip, bool isLoop = true)
        {
            musicSource.loop = isLoop;
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PlayVoice(AudioClip clip)
        {
            voiceSource.clip = clip;
            voiceSource.Play();
        }

        public void StopVoice()
        {
            voiceSource.Stop();
        }

        public void StopEffect()
        {
            effectSource.Stop();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void PauseMusic(bool status)
        {
            if (status)
                musicSource.Pause();
            else
                musicSource.UnPause();
        }

        public float GetEffectVolume()
        {
            return effectSource.volume;
        }

        public float GetMusicVolume()
        {
            return musicSource.volume;
        }

        public void UpdateEffectVolume(float volume)
        {
            effectSource.volume = volume;
        }

        public void UpdateMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        #endregion
    }
}
