using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Emotion.Screen1
{
    [RequireComponent(typeof(PlayableDirector))]
    public class IntroSequenceController : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private DoubleClick doubleClick;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Play play;
        [SerializeField] private GameObject parentsButton;

        private PlayableDirector playableDirector;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.Play();
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(WaitForSequenceToEnd());
        }

        public void CameraStopped()
        {
            doubleClick.doubleClicked += SkipAnimation;
        }

        private void OnDestroy()
        {
            doubleClick.doubleClicked -= SkipAnimation;
        }

        private IEnumerator WaitForSequenceToEnd()
        {
            yield return new WaitForSeconds((float)playableDirector.duration);
            if (!parentsButton.activeSelf)
                SkipAnimation();
        }

        private void SkipAnimation()
        {
            playableDirector.Stop();
            doubleClick.doubleClicked -= SkipAnimation;
            canvasGroup.blocksRaycasts = true;
            play.AppearSequence();
            parentsButton.SetActive(true);
        }

        #endregion
    }
}
