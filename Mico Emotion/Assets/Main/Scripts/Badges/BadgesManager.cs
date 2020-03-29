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

        public Badge GetBadgeByType(BadgeType type)
        {
            return badges.Find(collectible => collectible.Type == type);
        }

        #endregion
    }
}
