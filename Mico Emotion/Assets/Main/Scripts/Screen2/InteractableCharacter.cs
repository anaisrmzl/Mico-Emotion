using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Emotion.Screen2
{
    public class InteractableCharacter : MonoBehaviour
    {
        #region FIELDS

        private const float NumberOfSteps = 10.0f;
        private const float TweenDuration = 0.5f;

        [SerializeField] private GameObject blocker;
        [SerializeField] private Image emotionsBar;

        private Animator animator;
        private string lastInteractionId;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayAnimation(AnimationClip clip, int value, string id)
        {
            if (lastInteractionId != id)
            {
                float finalFill = emotionsBar.fillAmount + (value / NumberOfSteps);
                emotionsBar.DOFillAmount(finalFill, TweenDuration);
            }

            lastInteractionId = id;
            animator.Play(clip.name);
            StartCoroutine(WaitAnimation(clip.length));
        }

        private IEnumerator WaitAnimation(float clipLength)
        {
            blocker.SetActive(true);
            yield return new WaitForSeconds(clipLength);
            blocker.SetActive(false);

        }

        #endregion
    }
}
