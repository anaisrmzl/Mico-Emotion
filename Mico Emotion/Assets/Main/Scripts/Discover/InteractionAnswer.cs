using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Utilities.Sound;
using Zenject;

namespace Emotion.Discover
{
    [RequireComponent(typeof(Button))]
    public class InteractionAnswer : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private AudioClip answerAudio;
        [SerializeField] protected InteractivePage interactivePage;
        [SerializeField] private AnimationClip pressAnimation;
        [SerializeField] private bool correctAnswer;

        private Button button;
        private Animator animator;
        private bool touched = true;
        private ColorBlock colors;

        #endregion

        #region EVENTS

        public event UnityAction<float> pressedButton;

        #endregion

        #region BEHAVIORS

        public void Initialize()
        {
            button = GetComponent<Button>();
            animator = GetComponent<Animator>();
            colors = GetComponent<Button>().colors;
            button.onClick.AddListener(PlayAnswerAudio);
        }

        public void EnableButton(bool status)
        {
            button.interactable = status;
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
            ChangeButtonColors();
            button.interactable = false;
            interactivePage.DisableOtherButtons(this, answerAudio.length, touched, correctAnswer);
            touched = false;
            yield return new WaitForSeconds(pressAnimation.length);
            animator.speed = 0;
            yield return new WaitForSeconds(answerAudio.length - pressAnimation.length);
            interactivePage.EnableButtons();
        }

        private void ChangeButtonColors()
        {
            colors.disabledColor = colors.pressedColor;
            colors.normalColor = colors.pressedColor;
            colors.highlightedColor = colors.pressedColor;
            GetComponent<Button>().colors = colors;
        }

        #endregion
    }
}
