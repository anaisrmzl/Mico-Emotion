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

        public Badge GetBadgeByType(BadgeType type)
        {
            return badges.Find(collectible => collectible.Type == type);
        }

        #endregion
    }
}
