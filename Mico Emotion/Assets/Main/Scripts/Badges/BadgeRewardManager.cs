using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

using Utilities.Scenes;
using Utilities.Zenject;
using Utilities.Sound;
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
        [Inject] private SoundManager soundManager;

        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private Transform badgeHolder;
        [SerializeField] private BadgeUI badgePrefab;
        [SerializeField] private AudioClip badgeAnnouncement;

        private AudioClip badgeClip;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            soundManager.StopMusic();
            soundManager.StopVoice();
            soundManager.StopEffect();
        }

        public void CreateRandomBadge(BadgeType badgeType)
        {
            Badge badge = badgesManager.UnlockRandomBadge(badgeType);
            badgeClip = badge.Title;
            userManager.UpdateLastBadgeWon(badge.Id, badgeType);
            CreateBadge(badge, (int)badgeType);
        }

        public void CreateSpecificBadge(BadgeType badgeType, string id)
        {
            Badge badge = badgesManager.UnlockBadge(id);
            badgeClip = badge.Title;
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

        public void BadgeAnnouncement()
        {
            StartCoroutine(PlayBadgeAnnouncement());
        }

        private IEnumerator PlayBadgeAnnouncement()
        {
            soundManager.PlayVoice(badgeAnnouncement);
            yield return new WaitForSeconds(badgeAnnouncement.length);
            soundManager.PlayVoice(badgeClip);
        }

        private IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds((float)playableDirector.duration - WaitTime);
            AnimationSceneChanger.ChangeScene(SceneNames.Mood);
        }

        #endregion
    }
}
