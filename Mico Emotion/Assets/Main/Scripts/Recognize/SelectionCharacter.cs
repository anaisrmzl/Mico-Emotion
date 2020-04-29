using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Scenes;
using Utilities.Sound;
using Zenject;

using Emotion.MainMenu;

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
        [SerializeField] private AnimationClip earthquakeAnimation;
        [SerializeField] private AudioClip hiAudio;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Goodbye goodbye;
        [SerializeField] private Renderer bodyRenderer;
        [SerializeField] private PiecesController piecesController;
        [SerializeField] private bool available;

        private Animator animator;
        private bool selected = false;
        private bool earthquake = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            animator = GetComponent<Animator>();
            backButton.onClick.AddListener(Goodbye);
            StartCoroutine(RandomHi());
            piecesController.earthquake += DoEarthquake;
            if (!available)
                return;

            selectionButton.onClick.AddListener(Celebrate);
        }

        private void OnDestroy()
        {
            piecesController.earthquake -= DoEarthquake;
        }

        private IEnumerator RandomHi()
        {
            while (!selected)
            {
                yield return new WaitForSeconds(Random.Range(MinSecondsHi, MaxSecondsHi));
                if (Random.value > HiPercentage && !earthquake)
                    Salute();
            }
        }

        private void DoEarthquake()
        {
            if (selected)
                return;

            StartCoroutine(PlayEarthquakeAnimation());
        }

        private IEnumerator PlayEarthquakeAnimation()
        {
            earthquake = true;
            animator.Play(earthquakeAnimation.name);
            yield return new WaitForSeconds(earthquakeAnimation.length);
            earthquake = false;
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
