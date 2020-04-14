using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using Utilities.Sound;
using Zenject;

namespace Emotion
{
    public class Blocker : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip blockAudio;
        [SerializeField] private Button actionButton;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            actionButton.onClick.AddListener(PlayAnimation);
        }

        private void PlayAnimation()
        {
            transform.DOShakePosition(1.0f, Vector3.right * 0.5f, randomness: 0, fadeOut: false);
            soundManager.PlayEffect(blockAudio);
        }

        #endregion
    }
}
