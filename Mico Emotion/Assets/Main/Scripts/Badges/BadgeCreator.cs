using System.Collections;
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

        private const float WaitBetweenAudios = 0.5f;

        [Inject] private BadgesManager badgesManager;
        [Inject] private SoundManager soundManager;

        [SerializeField] private Image zoomedBadge;
        [SerializeField] private CanvasGroup blocker;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject zoomPanel;
        [SerializeField] private BadgeUI badgePrefab;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            backButton.onClick.AddListener(CloseZoom);

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

        public void InspectBadge(Sprite badgeImage, float titleBadgeAudioLength, AudioClip descriptionAudioClip)
        {
            blocker.blocksRaycasts = false;
            StartCoroutine(HearDescription(badgeImage, titleBadgeAudioLength, descriptionAudioClip));
        }

        private IEnumerator HearDescription(Sprite badgeImage, float timeToWait, AudioClip descriptionAudioClip)
        {
            yield return new WaitForSeconds(timeToWait + WaitBetweenAudios);
            blocker.blocksRaycasts = true;
            zoomedBadge.sprite = badgeImage;
            zoomPanel.SetActive(true);
            soundManager.PlayEffect(descriptionAudioClip);
        }

        private void CloseZoom()
        {
            zoomPanel.SetActive(false);
            StopAllCoroutines();
            soundManager.StopEffect();
        }

        #endregion
    }
}
