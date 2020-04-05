using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Utilities.Sound;
using Zenject;

namespace Emotion.Discover
{
    [RequireComponent(typeof(Button))]
    public abstract class AnswerButton : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip answerAudio;
        [SerializeField] protected AnswerButton otherButton;
        [SerializeField] private AnimationClip pressAnimation;

        private Button button;
        private Animator animator;

        #endregion

        #region EVENTS

        public event UnityAction<float> pressedButton;

        #endregion

        #region BEHAVIORS

        public void Initialize()
        {
            button = GetComponent<Button>();
            animator = GetComponent<Animator>();
            button.onClick.AddListener(PlayAnswerAudio);
        }

        public void EnableButton(bool status)
        {
            button.enabled = status;
            animator.speed = status ? 1 : 0;
        }

        private void PlayAnswerAudio()
        {
            pressedButton?.Invoke(answerAudio.length);
            soundManager.PlayEffect(soundManager.AudioGeneral);
            animator.Play(pressAnimation.name);
            StartCoroutine(PlayNarrative());
        }

        private IEnumerator PlayNarrative()
        {
            soundManager.PlayVoice(answerAudio);
            button.interactable = false;
            otherButton.EnableButton(false);
            yield return new WaitForSeconds(pressAnimation.length);
            animator.speed = 0;
            yield return new WaitForSeconds(answerAudio.length - pressAnimation.length);
            AfterPlayNarrative();
        }

        public abstract void AfterPlayNarrative();

        #endregion
    }
}
