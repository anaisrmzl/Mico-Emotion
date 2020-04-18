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
        [SerializeField] private AnimationClip sittingAnimation;
        [SerializeField] private Animator totiAnimator;
        [SerializeField] private GameObject totiSleep;
        [SerializeField] private GameObject totiSitting;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            userManager.UpdateCompletedMeditationGame(false);
            blocker.SetActive(false);
            soundManager.StopMusic();
            soundManager.StopEffect();
            soundManager.StopVoice();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            StartCoroutine(GameSequence());
        }

        private void OnDestroy()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }

        private IEnumerator PlayMeditation()
        {
            yield return new WaitForSeconds(WaitTime);
            soundManager.PlayVoice(introAudio);
            StartCoroutine(PlayAnimations());
            yield return new WaitForSeconds(introAudio.length + WaitTime);
            soundManager.PlayVoice(meditationAudio);
            yield return new WaitForSeconds(meditationAudio.length + WaitTime);
            soundManager.PlayVoice(afterMeditationAudio);
            totiSleep.SetActive(false);
            totiSitting.SetActive(true);
            yield return new WaitForSeconds(WaitTime);
            totiSitting.GetComponent<Animator>().SetTrigger(NamasteKey);
            yield return new WaitForSeconds(afterMeditationAudio.length);
        }

        private IEnumerator PlayAnimations()
        {
            totiAnimator.Play(sittingAnimation.name);
            yield return new WaitForSeconds(sittingAnimation.length);
            totiAnimator.gameObject.SetActive(false);
            totiSleep.SetActive(true);
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
