using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Mood
{
    public class MoodSelector : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip selectionAnimation;
        [SerializeField] private MoodManager moodManager;
        [SerializeField] private int value;

        private Button selectionButton;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            selectionButton = GetComponent<Button>();
            selectionButton.onClick.AddListener(Select);
        }

        private void Select()
        {
            animator.Play(selectionAnimation.name);
            moodManager.Select(selectionAnimation.length, value);
        }

        #endregion
    }
}
