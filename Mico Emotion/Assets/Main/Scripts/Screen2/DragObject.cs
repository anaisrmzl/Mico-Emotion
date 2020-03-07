using UnityEngine;

namespace Emotion.Screen2
{
    public class DragObject : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private float speed = 20.0f;

        private Rigidbody2D rigidBody;
        private Collider2D objectCollider;
        private bool draggin = false;
        private int finger = 0;

        #endregion

        #region PROPERTIES

        private int TapCount { get => Input.touchCount; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            objectCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (TapCount == 0)
                return;

            for (int i = 0; i < TapCount; i++)
            {
                Touch touch = Input.GetTouch(i);
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
            Vector3 position = Camera.main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
            return (hit.collider == objectCollider);
        }

        #endregion
    }
}
