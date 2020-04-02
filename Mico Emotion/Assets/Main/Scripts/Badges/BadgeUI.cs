using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Utilities.Sound;

namespace Emotion.Badges
{
    public class BadgeUI : MonoBehaviour
    {
        #region FIELDS

        [InjectOptional] private BadgeCreator badgeCreator;
        [Inject] private SoundManager soundManager;

        [SerializeField] private Image badgeImage;
        [SerializeField] private Image blocked;
        [SerializeField] private Button openButton;

        private Badge badge;

        #endregion

        #region BEHAVIORS   

        private void Awake()
        {
            openButton.onClick.AddListener(OpenBadge);
        }

        public void Initialize(Badge newBadge)
        {
            badge = newBadge;
            badgeImage.sprite = badge.Sprite;
            blocked.sprite = badge.Sprite;
            blocked.gameObject.SetActive(!badge.Acquired);
        }

        private void OpenBadge()
        {
            if (badgeCreator == null)
                return;

            soundManager.PlayEffect(badge.Title);
            if (!badge.Acquired)
                return;

            badgeCreator.InspectBadge(badge.Sprite, badge.Title.length, badge.Description);
        }

        #endregion
    }
}
