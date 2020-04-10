using UnityEngine;
using UnityEngine.UI;

using Zenject;
using TMPro;

using Emotion.Badges;
using Emotion.Data;

namespace Emotion.Metrics
{
    public class BadgesMetrics : MonoBehaviour
    {
        #region FIELDS

        private const string PercentageFormat = "{0}%";

        [Inject] private BadgesManager badgesManager;
        [Inject] private UserManager userManager;

        [SerializeField] private BadgeType badgeType;
        [SerializeField] private Image fillAmount;
        [SerializeField] private TextMeshProUGUI percentageText;
        [SerializeField] private TextMeshProUGUI totalText;
        [SerializeField] private Image lastBadge;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            GetPercentage();
            GetLastBadge();
            GetTotalBadges();
        }

        private void GetPercentage()
        {
            int percentage = (badgesManager.GetUnlockedBadgesByType(badgeType).Count * 100) / badgesManager.GetBadgesByType(badgeType).Count;
            percentageText.text = string.Format(PercentageFormat, percentage);
            fillAmount.fillAmount = percentage / 100.0f;
        }

        private void GetLastBadge()
        {
            lastBadge.sprite = badgesManager.GetBadgeById(userManager.GetLastBadgeWon(badgeType)).Sprite;
        }

        private void GetTotalBadges()
        {
            totalText.text = badgesManager.GetUnlockedBadgesByType(badgeType).Count.ToString();
        }

        #endregion
    }
}
