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

        public void CreateBadge(BadgeType badgeType)
        {
            Badge badge = badgesManager.UnlockRandomBadge(badgeType);
            BadgeUI badgeUI = ZenjectUtilities.Instantiate<BadgeUI>(badgePrefab, badgeHolder.position, Quaternion.identity, badgeHolder);
            badgeUI.Initialize(badge);
            userManager.UpdateLastGamePlayed((int)badgeType);
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
