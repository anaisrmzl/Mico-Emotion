﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Scenes;
using Utilities.Sound;
using Zenject;

namespace Emotion.Recognize
{
    public class SelectionCharacter : MonoBehaviour
    {
        #region FIELDS

        private const string HiTrigger = "hi";
        private const string ByeTrigger = "bye";
        private const string SelectedTrigger = "selected";
        private const float MinSecondsHi = 5.0f;
        private const float MaxSecondsHi = 10.0f;
        private const float HiPercentage = 0.5f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private Button selectionButton;
        [SerializeField] private Button backButton;
        [SerializeField] private AnimationClip celebration;
        [SerializeField] private AnimationClip goodbyeAnimation;
        [SerializeField] private AudioClip hiAudio;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Goodbye goodbye;
        [SerializeField] private Renderer bodyRenderer;

        private Animator animator;
        private bool selected = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            animator = GetComponent<Animator>();
            selectionButton.onClick.AddListener(Celebrate);
            backButton.onClick.AddListener(Goodbye);
            StartCoroutine(RandomHi());
        }

        private IEnumerator RandomHi()
        {
            while (!selected)
            {
                yield return new WaitForSeconds(Random.Range(MinSecondsHi, MaxSecondsHi));
                if (Random.value > HiPercentage)
                    Salute();
            }
        }

        private IEnumerator CelebrationAnimation()
        {
            yield return new WaitForSeconds(celebration.length);
            AnimationSceneChanger.ChangeScene(SceneNames.Recognize);
        }

        private void Celebrate()
        {
            selected = true;
            StopAllCoroutines();
            canvasGroup.blocksRaycasts = false;
            animator.SetTrigger(SelectedTrigger);
            StartCoroutine(CelebrationAnimation());
        }

        private void Salute()
        {
            animator.SetTrigger(HiTrigger);
            if (bodyRenderer.isVisible)
                soundManager.PlayVoice(hiAudio);
        }

        private void Goodbye()
        {
            canvasGroup.blocksRaycasts = false;
            animator.SetTrigger(ByeTrigger);
            if (bodyRenderer.isVisible)
                goodbye.ReturnToGameSelection(goodbyeAnimation.length);
        }

        #endregion
    }
}
