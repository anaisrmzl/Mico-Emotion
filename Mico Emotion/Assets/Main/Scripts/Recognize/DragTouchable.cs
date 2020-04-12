using UnityEngine;

using DG.Tweening;
using Utilities.Gestures;
using Zenject;

namespace Emotion.Recognize
{
    public class DragTouchable : DragObject
    {
        #region FIELDS

        private const string TouchableTag = "Touchable";
        private const float TweenDuration = 0.5f;

        [Inject] private RandomInteractions randomInteractions;
        [Inject] private InteractableCharacter interactableCharacter;

        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private AnimationClip loopAnimation;
        [SerializeField] private AnimationClip onceAnimation;
        [SerializeField] private AnimationClip idleAnimation;
        [SerializeField] private AudioClip loopAudio;
        [SerializeField] private AudioClip onceAudio;
        [SerializeField] private AudioClip idleAudio;
        [SerializeField] int value;

        private bool touching = false;

        #endregion

        #region BEHAVIORS   

        public override void Awake()
        {
            base.Awake();
            transform.DOMove(spawnPosition, 1.0f);
            interactableCharacter.CountForIdle();
        }

        public override void StoppedDragging()
        {
            randomInteractions.Dragging(dragging);
            if (!DragAllowed)
                return;

            if (touching)
            {
                Destroy(gameObject);
                interactableCharacter.PlayAnimation(onceAnimation, onceAudio, value, transform.name);
            }

            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            transform.DOMove(spawnPosition, TweenDuration);
        }

        public override void StartedDragging()
        {
            randomInteractions.Dragging(dragging);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == TouchableTag)
            {
                touching = true;
                interactableCharacter.PlaySingleAnimation(loopAnimation, loopAudio);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == TouchableTag)
            {
                touching = false;
                interactableCharacter.PlaySingleAnimation(idleAnimation, idleAudio);
            }
        }

        #endregion
    }
}
