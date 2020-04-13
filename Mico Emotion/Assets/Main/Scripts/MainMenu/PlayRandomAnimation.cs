using UnityEngine;
using System.Collections;

using Zenject;
using Utilities.Sound;

namespace Emotion.MainMenu
{
    public class PlayRandomAnimation : MonoBehaviour
    {
        #region FIELDS

        private const string PlayingAnimationMethod = "StartPlayingAnimation";
        private const float WaitTime = 4.0f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private Animator[] animators;
        [SerializeField] private AnimationClip[] animations;
        [SerializeField] private Transform[] positions;
        [SerializeField] private bool playOnStart = false;

        private Vector3 resetPosition = new Vector3(100.0f, 100.0f, 0.0f);
        private int characterIndex = 0;

        #endregion 

        #region BEHAVIORS

        private void Awake()
        {
            characterIndex = Random.Range(0, animators.Length);
            ResetAnimations();
            Invoke(PlayingAnimationMethod, playOnStart ? 0.0f : WaitTime);
        }

        private void StartPlayingAnimation()
        {
            StartCoroutine(PlayAnimation());
        }

        private void ResetAnimations()
        {
            foreach (Animator animator in animators)
                animator.transform.position = resetPosition;
        }

        private IEnumerator PlayAnimation()
        {
            characterIndex++;
            characterIndex = characterIndex >= animators.Length ? 0 : characterIndex;
            int positionIndex = Random.Range(0, positions.Length);
            animators[characterIndex].transform.position = positions[positionIndex].position;
            animators[characterIndex].transform.rotation = positions[positionIndex].rotation;
            animators[characterIndex].Play(animations[characterIndex].name);
            yield return new WaitForSeconds(animations[characterIndex].length);
            ResetAnimations();
            Invoke(PlayingAnimationMethod, WaitTime);
        }

        #endregion
    }
}
