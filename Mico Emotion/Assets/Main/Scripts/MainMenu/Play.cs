using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Utilities.Scenes;
using Zenject;
using Utilities.Sound;
using DG.Tweening;

namespace Emotion.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class Play : MonoBehaviour
    {
        #region FIELDS

        private const float MinOrthographicSize = 0.5f;
        private const float TransitionDuration = 2.0f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private AnimationClip pressAnimation;
        [SerializeField] private AnimationClip appearAnimation;
        [SerializeField] private Animator planetAnimator;
        [SerializeField] private AnimationClip transitionPlanet;
        [SerializeField] private CanvasGroup canvasGroup;

        private Button playButton;
        private Animator animator;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playButton = GetComponent<Button>();
            playButton.onClick.AddListener(PressButton);
        }

        private void PressButton()
        {
            animator.Play(pressAnimation.name);
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(PlaySequence());
        }

        private IEnumerator PlaySequence()
        {
            yield return new WaitForSeconds(pressAnimation.length);
            canvasGroup.alpha = 0;
            planetAnimator.Play(transitionPlanet.name);
            Camera.main.DOOrthoSize(MinOrthographicSize, TransitionDuration).SetEase(Ease.InOutQuart);
            soundManager.StopEffect();
            soundManager.StopVoice();
            yield return new WaitForSeconds(1.0f);
            FadeSceneChanger.ChangeScene(SceneNames.GameSelection);
        }

        public void AppearSequence()
        {
            animator.Play(appearAnimation.name);
        }

        #endregion
    }
}
