﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;
using Zenject;
using Utilities.Sound;

namespace Emotion.MainMenu
{
    public class PiecesController : MonoBehaviour
    {
        #region FIELDS

        private const float WaitTime = 5.0f;
        private const int MaxCount = 5;
        private const float PiecesDuration = 10.0f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private GameObject frontParticles;
        [SerializeField] private GameObject backParticles;
        [SerializeField] private GameObject downParticles;
        [SerializeField] private AudioClip audioEartquake;

        private int counter = 0;

        #endregion

        #region EVENTS

        public event UnityAction earthquake;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            StartCoroutine(PlayParticles());
        }

        private void OnDestroy()
        {
            Camera.main.DOKill();
        }

        private IEnumerator PlayParticles()
        {
            yield return new WaitForSeconds(WaitTime);
            GetAndPlayParticles();
            counter++;
            if (counter <= MaxCount)
                Instantiate(downParticles);

            soundManager.PlayEffect(audioEartquake);
            Camera.main.DOShakePosition(2.0f, new Vector3(0.5f, 0.0f, 0.0f), 5, 0, true).SetEase(Ease.InOutCubic);
            earthquake?.Invoke();
            yield return new WaitForSeconds(PiecesDuration);
            StartCoroutine(PlayParticles());
        }

        private void GetAndPlayParticles()
        {
            foreach (Transform particle in frontParticles.transform)
                particle.GetComponent<ParticleSystem>().Play();

            foreach (Transform particle in backParticles.transform)
                particle.GetComponent<ParticleSystem>().Play();
        }


        #endregion
    }
}
