using UnityEngine;
using UnityEngine.UI;

namespace Emotion.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ButtonPress : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private AnimationClip animtion;

        private Button actionButton;
        private Animator animator;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            actionButton = GetComponent<Button>();
            animator = GetComponent<Animator>();
            actionButton.onClick.AddListener(PlayAnimation);
        }

        private void PlayAnimation()
        {
            animator.Play(animtion.name);
        }

        #endregion
    }
}
