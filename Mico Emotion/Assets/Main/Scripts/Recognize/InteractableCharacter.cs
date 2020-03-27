﻿using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Scenes;
using DG.Tweening;

namespace Emotion.Recognize
{
    public class InteractableCharacter : MonoBehaviour
    {
        #region FIELDS

        private const float NumberOfSteps = 10.0f;
        private const float TweenDuration = 0.5f;
        private const float MaxIdleTime = 3.0f;

        [SerializeField] private Image emotionsBar;
        [SerializeField] private AnimationClip winAnimation;

        private Animator animator;
        private bool isIdle = true;
        private bool animated;
        private bool block = false;
        private float timer = 0.0f;
        private string lastInteractionId;
        private int happiness;

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
            happiness = (int)(emotionsBar.fillAmount * NumberOfSteps);
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
            if (block)
                return;

            isIdle = false;
            animated = true;
            string id = clip.name + name;
            if (lastInteractionId != id)
            {
                happiness += value;
                emotionsBar.DOFillAmount(happiness / NumberOfSteps, TweenDuration);
            }

            lastInteractionId = id;
            animator.Play(clip.name);
            interacted?.Invoke();
            StartCoroutine(WaitAnimation(clip.length));
        }

        public void PlaySingleAnimation(AnimationClip clip)
        {
            if (happiness == NumberOfSteps)
                return;

            animator.Play(clip.name);
            interacted?.Invoke();
        }

        private IEnumerator WaitAnimation(float clipLength)
        {
            block = true;
            yield return new WaitForSeconds(clipLength);
            if (CheckValue())
                yield break;

            block = false;
            animated = false;
            timer = 0.0f;
        }

        public void CountForIdle()
        {
            isIdle = false;
            timer = 0.0f;
        }

        private bool CheckValue()
        {
            if (happiness < NumberOfSteps)
                return false;

            ResetValues();
            StartCoroutine(EndGame());
            return true;
        }

        private void ResetValues()
        {
            block = true;
            isIdle = true;
            timer = 0.0f;
            animated = true;
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