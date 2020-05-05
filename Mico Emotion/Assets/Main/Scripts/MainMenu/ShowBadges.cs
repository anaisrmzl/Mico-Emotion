using UnityEngine;
using System.Collections;

using Zenject;
using Utilities.Sound;

namespace Emotion.MainMenu
{
    public class ShowBadges : MonoBehaviour
    {
        #region FIELDS

        private const float WaitTime = 4.0f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip instructionsClip;
        [SerializeField] private AnimationClip showAnimation;
        [SerializeField] private Animator badgeButtonAnimator;

        #endregion 

        #region BEHAVIORS

        private void Awake()
        {
            StartCoroutine(PlayAudio());
        }

        private IEnumerator PlayAudio()
        {
            yield return new WaitForSeconds(WaitTime);
            soundManager.PlayVoice(instructionsClip);
            badgeButtonAnimator.Play(showAnimation.name);
            yield return new WaitForSeconds(instructionsClip.length);
        }

        #endregion
    }
}
