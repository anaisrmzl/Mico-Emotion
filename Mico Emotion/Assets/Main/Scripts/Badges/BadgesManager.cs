using System.Collections.Generic;
using UnityEngine;

using Utilities.Data;
using Zenject;

namespace Emotion.Badges
{
    public class BadgesManager : MonoBehaviour
    {
        #region FIELDS

        private const string BadgesResourceFolder = "Badges";

        [Inject] private DataManager dataManager;

        private List<Badge> badges = new List<Badge>();

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            LoadCurrencies();
        }

        public void LoadCurrencies()
        {
            Object[] badgeObjects = Resources.LoadAll(BadgesResourceFolder, typeof(Badge));
            foreach (Badge badge in badgeObjects)
            {
                badges.Add(badge);
                badge.Load(dataManager);
            }
        }

        public Badge GetBadgeById(string id)
        {
            return badges.Find(collectible => collectible.Id == id);
        }

        public List<Badge> GetAcquiredBadges(bool status)
        {
            return badges.FindAll(collectible => collectible.Acquired == status);
        }

        public List<Badge> GetLockedBadgesByType(BadgeType type)
        {
            return GetAcquiredBadges(false).FindAll(collectible => collectible.Type == type);
        }

        public List<Badge> GetUnlockedBadgesByType(BadgeType type)
        {
            return GetAcquiredBadges(true).FindAll(collectible => collectible.Type == type);
        }

        public List<Badge> GetBadgesByType(BadgeType type)
        {
            return badges.FindAll(collectible => collectible.Type == type);
        }

        public Badge UnlockRandomBadge(BadgeType type)
        {
            List<Badge> badges = GetLockedBadgesByType(type);
            if (badges.Count == 0)
                badges = GetUnlockedBadgesByType(type);

            int randomBadge = Random.Range(0, badges.Count);
            badges[randomBadge].AcquireBadge();
            return badges[randomBadge];
        }

        public Badge UnlockBadge(string id)
        {
            Badge badge = GetBadgeById(id);
            badge.AcquireBadge();
            return badge;
        }

        #endregion
    }
}
