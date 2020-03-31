using UnityEngine;
using System.Collections;

using Zenject;
using Utilities.Sound;
using Utilities.Scenes;

using Emotion.Data;
using Emotion.Badges;

namespace Emotion.Mood
{
    public class MoodManager : MonoBehaviour
    {
        #region FIELDS

        private const float WaitTime = 5.0f;
        private const float InitialDelay = 1.0f;

        [Inject] private SoundManager soundManager;
        [Inject] private UserManager userManager;

        [SerializeField] private AudioClip initialAudio;
        [SerializeField] private AudioClip idleAudio;
        [SerializeField] private AudioClip selectionAudio;
        [SerializeField] private GameObject blocker;

        private float counter = 0.0f;
        private bool selectionMade = false;
        private bool count = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            blocker.SetActive(false);
            StartCoroutine(PlayInitialAudio());
        }

        private void Update()
        {
            if (selectionMade)
                return;

            if (!count)
                return;

            counter += Time.deltaTime;
            if (counter >= WaitTime)
                StartCoroutine(PlayIdleAudio());
        }

        private IEnumerator PlayIdleAudio()
        {
            ResetCounter();
            soundManager.PlayEffect(idleAudio);
            yield return new WaitForSeconds(idleAudio.length);
            count = true;
        }

        private void ResetCounter()
        {
            count = false;
            counter = 0.0f;
        }

        public void Select(float animationLength)
        {
            soundManager.StopEffect();
            StopAllCoroutines();
            blocker.SetActive(true);
            selectionMade = true;
            StartCoroutine(PlaySelectionAudio(animationLength));
        }

        private IEnumerator PlaySelectionAudio(float animationLength)
        {
            yield return new WaitForSeconds(animationLength);
            soundManager.PlayEffect(selectionAudio);
            yield return new WaitForSeconds(selectionAudio.length);
            ChangeScene();
        }

        private void ChangeScene()
        {
            switch (userManager.LastGamePlayed)
            {
                case (int)BadgeType.Recognize:
                    AnimationSceneChanger.ChangeScene(SceneNames.CharacterSelection);
                    break;
                case (int)BadgeType.Discover:
                    AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
                    break;
                case (int)BadgeType.Explore:
                    AnimationSceneChanger.ChangeScene(SceneNames.Map);
                    break;
                default:
                    AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
                    break;
            }
        }

        private IEnumerator PlayInitialAudio()
        {
            yield return new WaitForSeconds(InitialDelay);
            count = true;
            soundManager.PlayEffect(initialAudio);
        }

        #endregion
    }
}
