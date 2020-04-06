using System.Collections;
using UnityEngine;

using Zenject;
using Utilities.Zenject;
using Utilities.Scenes;
using Utilities.Sound;

using Emotion.Data;
using Emotion.Badges;

namespace Emotion.Explore
{
    public class MeditationManager : MonoBehaviour
    {
        #region FIELDS

        private const float WaitTime = 1.0f;
        private const string NamasteKey = "namaste";

        [Inject] private UserManager userManager;
        [Inject] private SoundManager soundManager;

        [SerializeField] private BadgeRewardManager badgeRewardManagerPrefab;
        [SerializeField] private GameObject blocker;
        [SerializeField] private AudioClip introAudio;
        [SerializeField] private AudioClip meditationAudio;
        [SerializeField] private AudioClip afterMeditationAudio;
        [SerializeField] private AnimationClip meditationAnimation;
        [SerializeField] private AnimationClip sittingAnimation;
        [SerializeField] private Animator totiAnimator;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            userManager.UpdateCompletedMeditationGame(false);
            blocker.SetActive(false);
            StartCoroutine(GameSequence());
        }

        private IEnumerator PlayMeditation()
        {
            yield return new WaitForSeconds(WaitTime);
            totiAnimator.Play(sittingAnimation.name);
            soundManager.PlayVoice(introAudio);
            yield return new WaitForSeconds(introAudio.length + WaitTime);
            totiAnimator.Play(meditationAnimation.name);
            soundManager.PlayVoice(meditationAudio);
            yield return new WaitForSeconds(meditationAudio.length + WaitTime);
            totiAnimator.SetTrigger(NamasteKey);
            soundManager.PlayVoice(afterMeditationAudio);
            yield return new WaitForSeconds(afterMeditationAudio.length + WaitTime);
        }

        private IEnumerator GameSequence()
        {
            yield return PlayMeditation();
            userManager.UpdateCompletedMeditationGame(true);
            blocker.SetActive(true);
            if (userManager.CompletedMeditationGame && userManager.CompletedStonesGame)
            {
                yield return new WaitForSeconds(AnimationSceneChanger.Animate());
                BadgeRewardManager badgeRewardManager = ZenjectUtilities.Instantiate<BadgeRewardManager>(badgeRewardManagerPrefab, Vector3.zero, Quaternion.identity, null);
                badgeRewardManager.CreateRandomBadge(BadgeType.Explore);
            }
            else
            {
                AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
            }
        }

        #endregion
    }
}
