using UnityEngine;
using UnityEngine.UI;

using Zenject;
using Utilities.Zenject;
using Utilities.Sound;

namespace Emotion.Badges
{
    public class BadgeCreator : MonoBehaviour
    {
        #region FIELDS

        [Inject] private BadgesManager badgesManager;
        [Inject] private SoundManager soundManager;

        [SerializeField] private Image zoomedBadge;
        [SerializeField] private Button hearButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject zoomPanel;
        [SerializeField] private BadgeUI badgePrefab;

        private AudioClip currentAudio;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            backButton.onClick.AddListener(CloseZoom);
            hearButton.onClick.AddListener(HearDescription);

            foreach (Badge badge in badgesManager.GetAcquiredBadges(true))
            {
                BadgeUI badgeUI = ZenjectUtilities.Instantiate<BadgeUI>(badgePrefab, badgePrefab.transform.position, Quaternion.identity, transform);
                badgeUI.Initialize(badge);
            }

            foreach (Badge badge in badgesManager.GetAcquiredBadges(false))
            {
                BadgeUI badgeUI = ZenjectUtilities.Instantiate<BadgeUI>(badgePrefab, badgePrefab.transform.position, Quaternion.identity, transform);
                badgeUI.Initialize(badge);
            }
        }

        public void InspectBadge(Sprite badgeImage, AudioClip badgeAudio)
        {
            zoomedBadge.sprite = badgeImage;
            currentAudio = badgeAudio;
            zoomPanel.SetActive(true);
        }

        private void HearDescription()
        {
            soundManager.PlayEffect(currentAudio);
        }

        private void CloseZoom()
        {
            zoomPanel.SetActive(false);
            soundManager.StopEffect();
        }

        #endregion
    }
}
