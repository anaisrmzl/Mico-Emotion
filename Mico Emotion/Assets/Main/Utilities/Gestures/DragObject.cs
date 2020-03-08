using UnityEngine;

namespace Utilities.Gestures
{
    public class DragObject : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private float speed = 20.0f;

        protected Rigidbody2D rigidBody;
        protected Collider2D objectCollider;
        private bool draggin = false;
        private int finger = 0;

        #endregion

        #region PROPERTIES

        private int TapCount { get => Input.touchCount; }
        protected bool DragAllowed { get; set; }
        private float YLimit { get => -8.0f + (objectCollider.bounds.center.y - objectCollider.bounds.min.y); }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            DragAllowed = true;
            rigidBody = GetComponent<Rigidbody2D>();
            objectCollider = GetComponent<Collider2D>();
        }

        public virtual void Update()
        {
            if (!DragAllowed)
                return;

            if (Input.touchSupported)
                HandleTouch();
            else
                HandleMouse();
        }

        private void HandleTouch()
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
                            rigidBody.gravityScale = 1;
                            draggin = true;
                        }

                        break;
                    case TouchPhase.Moved:
                        if (draggin)
                        {
                            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(finger).position);
                            Vector3 fingerPoint = new Vector3(worldPoint.x, Mathf.Max(worldPoint.y, YLimit), 0.0f);
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

        private void HandleMouse()
        {
            if (Input.GetMouseButtonDown(0) && CheckIfObjectIsTouched(Input.mousePosition))
            {
                rigidBody.gravityScale = 1;
                draggin = true;
            }
            else if (Input.GetMouseButton(0) && draggin)
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 fingerPoint = new Vector3(worldPoint.x, Mathf.Max(worldPoint.y, YLimit), 0.0f);
                rigidBody.velocity = (fingerPoint - transform.position) * speed;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                draggin = false;
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
