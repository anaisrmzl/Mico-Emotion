using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Emotion.Badges
{
    public class BadgesManager : MonoBehaviour
    {
        #region FIELDS

        private const string BadgesResourceFolder = "Badges";

        private List<Badge> badges = new List<Badge>();

        #endregion

        #region BEHAVIORS

        public void LoadCurrencies()
        {
            Object[] badgeObjects = Resources.LoadAll(BadgesResourceFolder, typeof(Badge));
            foreach (Badge badge in badgeObjects)
                badges.Add(badge);
        }

        #endregion
    }
}
