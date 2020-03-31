using System.Collections;
using UnityEngine;

using Utilities.Scenes;
using Utilities.Zenject;
using Zenject;

namespace Emotion.Badges
{
    public class BadgeRewardManager : MonoBehaviour
    {
        #region FIELDS

        [Inject] private BadgesManager badgesManager;

        [SerializeField] private Animator planetAnimation;
        [SerializeField] private AnimationClip congratulationAnimation;
        [SerializeField] private Transform badgeHolder;
        [SerializeField] private BadgeUI badgePrefab;

        #endregion

        #region BEHAVIORS

        public void CreateBadge(BadgeType badgeType)
        {
            Badge badge = badgesManager.UnlockRandomBadge(badgeType);
            BadgeUI badgeUI = ZenjectUtilities.Instantiate<BadgeUI>(badgePrefab, badgeHolder.position, Quaternion.identity, badgeHolder);
            badgeUI.Initialize(badge);
            StartCoroutine(ChangeScene());
        }

        private IEnumerator ChangeScene()
        {
            planetAnimation.Play(congratulationAnimation.name);
            yield return new WaitForSeconds(congratulationAnimation.length);
            AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
        }

        #endregion
    }
}
