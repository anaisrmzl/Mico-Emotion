using UnityEngine;

using DG.Tweening;
using Utilities.Gestures;
using Zenject;

namespace Emotion.Recognize
{
    public class DragInteractable : DragObject
    {
        #region FIELDS

        private const string InteractableTag = "Interactable";
        private const string DestroyableTag = "Destroyable";
        private const float TweenDuration = 0.5f;

        [Inject] private InteractableCharacter interactableCharacter;

        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private AnimationClip loopAnimation;
        [SerializeField] private AnimationClip idleAnimation;
        [SerializeField] private AudioClip loopAudio;
        [SerializeField] private AudioClip idleAudio;
        [SerializeField] int value;

        private bool interacting = false;

        #endregion

        #region BEHAVIORS   

        public override void Awake()
        {
            base.Awake();
            transform.DOMove(spawnPosition, 1.0f);
            interactableCharacter.WaitForInteraction(true);
            interactableCharacter.interacted += CancelInteraction;
        }

        private void OnDestroy()
        {
            interactableCharacter.interacted -= CancelInteraction;
            transform.DOKill();
        }

        public override void StoppedDragging()
        {
            if (!DragAllowed)
                return;

            if (interacting)
            {
                StopInteraction();
                return;
            }

            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            transform.DOMove(spawnPosition, TweenDuration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == InteractableTag)
            {
                interacting = true;
                interactableCharacter.PlayAnimation(loopAnimation, loopAudio, value, transform.name, true);
            }

            if (other.tag == DestroyableTag)
                Destroy(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == InteractableTag && interacting)
                StopInteraction();
        }

        private void CancelInteraction()
        {
            if (interactableCharacter.LastInteractionId == loopAnimation.name + transform.name)
                return;

            ResetInteraction();
        }

        private void StopInteraction()
        {
            ResetInteraction();
            interactableCharacter.PlaySingleAnimation(idleAnimation, idleAudio);
        }

        private void ResetInteraction()
        {
            interactableCharacter.interacted -= CancelInteraction;
            interactableCharacter.WaitForInteraction(false);
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 1;
            DragAllowed = false;
            interacting = false;
        }

        public override void StartedDragging()
        {

        }

        #endregion
    }
}
