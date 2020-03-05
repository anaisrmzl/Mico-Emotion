﻿using UnityEngine;

namespace Emotion.Screen2
{
    public class DragObject : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private float speed = 20.0f;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private Collider2D objectCollider;

        private bool draggin = false;
        int finger = 0;

        #endregion

        #region BEHAVIORS

        private void Update()
        {
            var tapCount = Input.touchCount;
            if (tapCount == 0)
                return;

            for (var i = 0; i < tapCount; i++)
            {
                var touch = Input.GetTouch(i);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (CheckIfObjectIsTouched(touch.position))
                        {
                            finger = touch.fingerId;
                            draggin = true;
                        }

                        break;
                    case TouchPhase.Moved:
                        if (draggin)
                        {
                            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(finger).position);
                            Vector3 fingerPoint = new Vector3(worldPoint.x, worldPoint.y, 0.0f);
                            rigidBody.velocity = (fingerPoint - transform.position) * speed;
                        }

                        break;
                    case TouchPhase.Ended:
                        if (finger == touch.fingerId)
                            draggin = false;

                        break;
                }
            }
        }

        private bool CheckIfObjectIsTouched(Vector3 touchPosition)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            return (hit.collider == objectCollider);
        }

        #endregion
    }
}