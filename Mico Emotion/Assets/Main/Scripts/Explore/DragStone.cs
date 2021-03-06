﻿using UnityEngine;

using Utilities.Gestures;
using DG.Tweening;
using Utilities.Sound;
using Zenject;

namespace Emotion.Explore
{
    public class DragStone : DragObject
    {
        #region FIELDS

        private const float MinDistance = 1.5f;
        private const float TweenDuration = 0.2f;
        private const string FloorTag = "Floor";
        private const string FixedRockTag = "FixedRock";
        private const string ReleaseTrigger = "release";
        private const string TouchTrigger = "touch";

        [Inject] private SoundManager soundManager;

        [SerializeField] private AnimationClip sleepAnimation;
        [SerializeField] private Color offColor;

        private Animator animator;
        private StonesManager stonesManager;
        private SpriteRenderer spriteRenderer;

        #endregion

        #region BEHAVIORS

        public override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public override void Update()
        {
            if (!DragAllowed)
                return;

            base.Update();
            Vector2 lowPoint = new Vector2(transform.position.x, objectCollider.bounds.min.y);
            Vector2 towerHighestPoint = new Vector2(stonesManager.BaseX, stonesManager.BoundY);
            float distance = Vector2.Distance(lowPoint, towerHighestPoint);
            if (distance > MinDistance)
                return;

            Vector2 finalPosition = new Vector2(stonesManager.BaseX, towerHighestPoint.y + (objectCollider.bounds.center.y - objectCollider.bounds.min.y));
            DragAllowed = false;
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0;
            transform.DOMove(finalPosition, TweenDuration).OnComplete(SetStatic);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.tag == StonesManager.PileTag && transform.parent == null)
            {
                rigidBody.bodyType = RigidbodyType2D.Static;
                spriteRenderer.color = offColor;
                stonesManager.AddToThePile(gameObject);
                DragAllowed = false;
                animator.Play(sleepAnimation.name);
            }

            if (other.transform.tag == FloorTag)
                rigidBody.gravityScale = 1;
        }

        public void Initialize(StonesManager manager)
        {
            stonesManager = manager;
        }

        private void SetStatic()
        {
            rigidBody.gravityScale = 1;
            gameObject.layer = LayerMask.NameToLayer(FixedRockTag);
        }

        public override void StoppedDragging()
        {
            animator.SetTrigger(ReleaseTrigger);
        }

        public override void StartedDragging()
        {
            animator.SetTrigger(TouchTrigger);
            soundManager.PlayEffect(soundManager.AudioTakeStone);
        }

        #endregion
    }
}
