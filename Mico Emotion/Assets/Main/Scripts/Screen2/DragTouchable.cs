using UnityEngine;

using DG.Tweening;
using Utilities.Gestures;
using Zenject;

namespace Emotion.Screen2
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
        [SerializeField] int value;

        private bool touching = false;

        #endregion

        #region BEHAVIORS   

        public override void Awake()
        {
            base.Awake();
            transform.DOMove(spawnPosition, 1.0f);
            randomInteractions.IsDragableOnScene(true);
            interactableCharacter.CountForIdle();
        }

        private void OnDestroy()
        {
            randomInteractions.IsDragableOnScene(false);
        }

        public override void StoppedDragging()
        {
            if (!DragAllowed)
                return;

            if (touching)
            {
                Destroy(gameObject);
                interactableCharacter.PlayAnimation(onceAnimation, value, transform.name);
            }

            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            transform.DOMove(spawnPosition, TweenDuration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == TouchableTag)
            {
                touching = true;
                interactableCharacter.PlayAnimation(loopAnimation, 0, transform.name);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == TouchableTag)
            {
                touching = false;
                interactableCharacter.PlayAnimation(idleAnimation, 0, transform.name);
            }
        }

        #endregion
    }
}
