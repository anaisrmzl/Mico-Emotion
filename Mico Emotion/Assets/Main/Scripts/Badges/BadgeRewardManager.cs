using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

using Utilities.Scenes;
using Utilities.Zenject;
using Zenject;

using Emotion.Data;

namespace Emotion.Badges
{
    public class BadgeRewardManager : MonoBehaviour
    {
        #region FIELDS

        private const float WaitTime = 0.5f;

        [Inject] private BadgesManager badgesManager;
        [Inject] private UserManager userManager;

        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private Transform badgeHolder;
        [SerializeField] private BadgeUI badgePrefab;

        #endregion

        #region BEHAVIORS

        public void CreateRandomBadge(BadgeType badgeType)
        {
            Badge badge = badgesManager.UnlockRandomBadge(badgeType);
            userManager.UpdateLastBadgeWon(badge.Id, badgeType);
            CreateBadge(badge, (int)badgeType);
        }

        public void CreateSpecificBadge(BadgeType badgeType, string id)
        {
            Badge badge = badgesManager.UnlockBadge(id);
            userManager.UpdateLastBadgeWon(badge.Id, badgeType);
            CreateBadge(badge, (int)badgeType);
        }

        private void CreateBadge(Badge badge, int badgeType)
        {
            BadgeUI badgeUI = ZenjectUtilities.Instantiate<BadgeUI>(badgePrefab, badgeHolder.position, Quaternion.identity, badgeHolder);
            badgeUI.Initialize(badge);
            userManager.UpdateLastGamePlayed(badgeType);
            StartCoroutine(ChangeScene());
        }

        private IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds((float)playableDirector.duration - WaitTime);
            AnimationSceneChanger.ChangeScene(SceneNames.Mood);
        }

        #endregion
    }
}
