using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Emotion.Recognize
{
    public class Spider : MonoBehaviour
    {
        public const string AppearKey = "appear";

        [Inject] InteractableCharacter interactableCharacter;

        [SerializeField] private AnimationClip scaredClip;
        [SerializeField] private AnimationClip reliefClip;
        [SerializeField] private AudioClip scaredAudio;
        [SerializeField] private AudioClip reliefAudio;
        [SerializeField] private Button spiderButton;

        private Animator animator;

        private void Awake()
        {
            interactableCharacter.interacted += CancelSpider;
            animator = GetComponent<Animator>();
            spiderButton.onClick.AddListener(DisappearSpider);
        }

        private void OnDestroy()
        {
            interactableCharacter.interacted -= CancelSpider;
        }

        public void AppearSpider()
        {
            interactableCharacter.WaitForInteraction(true);
            spiderButton.interactable = false;
            interactableCharacter.PlayAnimation(scaredClip, scaredAudio, -1, transform.name);
            animator.SetBool(AppearKey, true);
            StartCoroutine(EnableButton());
        }

        private IEnumerator EnableButton()
        {
            yield return new WaitForSeconds(scaredAudio.length);
            spiderButton.interactable = true;
        }

        private void DisappearSpider()
        {
            interactableCharacter.WaitForInteraction(false);
            animator.SetBool(AppearKey, false);
            interactableCharacter.PlayAnimation(reliefClip, reliefAudio, 1, transform.name);
        }

        private void CancelSpider()
        {
            if (!animator.GetBool(AppearKey))
                return;

            interactableCharacter.WaitForInteraction(false);
            animator.SetBool(AppearKey, false);
        }
    }
}
