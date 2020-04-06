using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Utilities.Sound;

namespace Emotion.Mood
{
    public class MoodSelector : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip selectionAnimation;
        [SerializeField] private MoodManager moodManager;

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
            soundManager.PlayEffect(soundManager.AudioGeneral);
            moodManager.Select(selectionAnimation.length);
        }

        #endregion
    }
}
