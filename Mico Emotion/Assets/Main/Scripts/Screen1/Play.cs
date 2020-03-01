using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Collections;

namespace Emotion.Screen1
{
    [RequireComponent(typeof(Button))]
    public class Play : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private AnimationClip pressAnimation;
        [SerializeField] private PlayableDirector timelineSequence;

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
            playButton.interactable = false;
            StartCoroutine(PlaySequence());
        }

        private IEnumerator PlaySequence()
        {
            yield return new WaitForSeconds(pressAnimation.length);
            timelineSequence.Stop();
            timelineSequence.Play();
            yield return new WaitForSeconds((float)timelineSequence.duration);
            playButton.interactable = true;
        }

        #endregion
    }
}
