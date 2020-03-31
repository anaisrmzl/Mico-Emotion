using System.Collections;
using UnityEngine;

using Zenject;
using Utilities.Zenject;
using Utilities.Scenes;

using Emotion.Data;
using Emotion.Badges;

namespace Emotion.Explore
{
    public class MeditationManager : MonoBehaviour
    {
        #region FIELDS

        private const float MeditationDuration = 3.0f;

        [Inject] private UserManager userManager;

        [SerializeField] private BadgeRewardManager badgeRewardManagerPrefab;
        [SerializeField] private GameObject blocker;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            userManager.UpdateCompletedMeditationGame(false);
            blocker.SetActive(false);
            StartCoroutine(PlayMeditation());
        }

        private IEnumerator PlayMeditation()
        {
            yield return new WaitForSeconds(MeditationDuration);
            userManager.UpdateCompletedMeditationGame(true);
            blocker.SetActive(true);
            if (userManager.CompletedMeditationGame && userManager.CompletedStonesGame)
            {
                yield return new WaitForSeconds(AnimationSceneChanger.Animate());
                BadgeRewardManager badgeRewardManager = ZenjectUtilities.Instantiate<BadgeRewardManager>(badgeRewardManagerPrefab, Vector3.zero, Quaternion.identity, null);
                badgeRewardManager.CreateBadge(BadgeType.Explore);
            }
            else
            {
                AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
            }
        }

        #endregion

    }
}
