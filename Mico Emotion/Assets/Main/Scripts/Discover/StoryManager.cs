using System.Collections;
using Emotion.Badges;
using UnityEngine;

using Utilities.Scenes;
using Utilities.Extensions;
using Utilities.Zenject;

namespace Emotion.Discover
{
    public class StoryManager : MonoBehaviour
    {
        #region FIELDS

        private const float FadeDuration = 1.0f;
        private const float StayDuration = 0.5f;

        [SerializeField] private Page[] storyPages;
        [SerializeField] private BadgeRewardManager badgeRewardManagerPrefab;

        private int currentPage = 0;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            EnableNextPage();
        }

        public void ChangePage()
        {
            currentPage++;
            StartCoroutine(FadePage());
        }

        private void EnableNextPage()
        {
            storyPages[currentPage].gameObject.SetActive(true);
            if (storyPages[currentPage].PageLength != 0)
                StartCoroutine(ChangeSimplePage());

            if (currentPage == 0)
                return;

            storyPages[currentPage - 1].gameObject.SetActive(false);
        }

        private IEnumerator FadePage()
        {
            if (currentPage >= storyPages.Length)
            {
                yield return EndGame();
            }
            else
            {
                yield return new WaitForSeconds(FadeSceneChanger.FadeCanvas(FadeDuration, StayDuration));
                EnableNextPage();
            }
        }

        private IEnumerator ChangeSimplePage()
        {
            yield return new WaitForSeconds(storyPages[currentPage].PageLength);
            currentPage++;
            StartCoroutine(FadePage());
        }

        private IEnumerator EndGame()
        {
            yield return new WaitForSeconds(AnimationSceneChanger.Animate());
            BadgeRewardManager badgeRewardManager = ZenjectUtilities.Instantiate<BadgeRewardManager>(badgeRewardManagerPrefab, Vector3.zero, Quaternion.identity, null);
            badgeRewardManager.CreateSpecificBadge(BadgeType.Discover, EnumExtensions.GetEnumName<DiscoverBadges>(0));
        }

        #endregion
    }
}
