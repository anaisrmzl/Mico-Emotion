using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Emotion.MainMenu
{
    [RequireComponent(typeof(PlayableDirector))]
    public class IntroSequenceController : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Play play;
        [SerializeField] private GameObject parentsButton;

        private PlayableDirector playableDirector;
        private bool cameraStopped = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.Play();
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(WaitForSequenceToEnd());
        }

        private void Update()
        {
            if (!cameraStopped)
                return;

            if (Input.GetMouseButtonDown(0))
                SkipAnimation();
        }

        public void CameraStopped()
        {
            cameraStopped = true;
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
            cameraStopped = false;
            canvasGroup.blocksRaycasts = true;
            play.AppearSequence();
            parentsButton.SetActive(true);
        }

        #endregion
    }
}
