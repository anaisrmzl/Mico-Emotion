using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Screen1
{
    public class Play : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private AnimationClip pressAnimation;

        private Button playButton;
        private Animator animator;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playButton = GetComponent<Button>();
            playButton.onClick.AddListener(PlayGame);
        }

        private void PlayGame()
        {
            animator.Play(pressAnimation.name);
        }

        #endregion
    }
}
