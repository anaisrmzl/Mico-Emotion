using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Emotion.Screen1
{
    [RequireComponent(typeof(PlayableDirector))]
    public class IntroSequenceController : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private CanvasGroup canvasGroup;

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

        private IEnumerator WaitForSequenceToEnd()
        {
            yield return new WaitForSeconds((float)playableDirector.duration);
            playableDirector.Stop();
            canvasGroup.blocksRaycasts = true;
        }

        #endregion
    }
}
