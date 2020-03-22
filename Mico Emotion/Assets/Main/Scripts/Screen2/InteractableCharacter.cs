using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Scenes;
using DG.Tweening;

namespace Emotion.Screen2
{
    public class InteractableCharacter : MonoBehaviour
    {
        #region FIELDS

        private const float NumberOfSteps = 10.0f;
        private const float TweenDuration = 0.5f;
        private const float MaxIdleTime = 3.0f;

        [SerializeField] private GameObject blocker;
        [SerializeField] private Image emotionsBar;
        [SerializeField] private AnimationClip winAnimation;

        private Animator animator;
        private bool isIdle = true;
        private bool animated;
        private float timer = 0.0f;
        private string lastInteractionId;

        #endregion

        #region EVENTS

        public event UnityAction interacted;
        public event UnityAction idle;

        #endregion

        #region PROPERTIES

        public bool WaitingInteraction { get; private set; } = false;
        public string LastInteractionId { get => lastInteractionId; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            animator = GetComponent<Animator>();
            CountForIdle();
        }

        private void Update()
        {
            if (animated || isIdle)
                return;

            timer += Time.deltaTime;
            if (timer >= MaxIdleTime)
            {
                isIdle = true;
                idle?.Invoke();
            }
        }

        public void WaitForInteraction(bool status)
        {
            WaitingInteraction = status;
        }

        public void PlayAnimation(AnimationClip clip, int value, string name)
        {
            string id = clip.name + name;
            isIdle = false;
            animated = true;
            if (lastInteractionId != id)
            {
                float finalFill = emotionsBar.fillAmount + (value / NumberOfSteps);
                emotionsBar.DOFillAmount(finalFill, TweenDuration);
            }

            lastInteractionId = id;
            animator.Play(clip.name);
            interacted?.Invoke();
            StartCoroutine(WaitAnimation(clip.length));
        }

        private IEnumerator WaitAnimation(float clipLength)
        {
            blocker.SetActive(true);
            yield return new WaitForSeconds(clipLength);
            blocker.SetActive(false);
            animated = false;
            timer = 0.0f;
            CheckValue();
        }

        public void CountForIdle()
        {
            isIdle = false;
            timer = 0.0f;
        }

        private void CheckValue()
        {
            if (emotionsBar.fillAmount < 1)
                return;

            blocker.SetActive(true);
            isIdle = true;
            animated = true;
            StartCoroutine(EndGame());
        }

        private IEnumerator EndGame()
        {
            animator.Play(winAnimation.name);
            yield return new WaitForSeconds(winAnimation.length);
            AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
        }

        #endregion
    }
}
