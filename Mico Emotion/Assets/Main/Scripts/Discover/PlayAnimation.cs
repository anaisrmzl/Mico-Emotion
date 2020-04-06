using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Discover
{
    [RequireComponent(typeof(Button))]
    public class PlayAnimation : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private AnimationClip animationToPlay;
        [SerializeField] private Animator animator;

        private Button button;

        #endregion

        #region BEHAVIORS

        public void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PlayAnimationClip);
        }

        private void PlayAnimationClip()
        {
            animator.Play(animationToPlay.name);
        }

        #endregion
    }
}
