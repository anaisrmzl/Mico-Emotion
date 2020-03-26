using UnityEngine;

namespace Utilities.Gestures
{
    public abstract class DragObject : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private float speed = 20.0f;
        [SerializeField] private float offset = 0.0f;

        protected Rigidbody2D rigidBody;
        protected Collider2D objectCollider;
        protected bool dragging = false;
        private int finger = 0;

        #endregion

        #region PROPERTIES

        private int TapCount { get => Input.touchCount; }
        protected bool DragAllowed { get; set; }
        private float Height { get => Camera.main.orthographicSize * 2.0f; }
        private float Width { get => Height * Camera.main.aspect; }
        private float YLimit { get => (Height / 2.0f) - (objectCollider.bounds.center.y - objectCollider.bounds.min.y); }
        private float XLimit { get => (Width / 2.0f) - (objectCollider.bounds.center.x - objectCollider.bounds.min.x); }

        #endregion

        #region BEHAVIORS

        public virtual void Awake()
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
                            dragging = true;
                            StartedDragging();
                        }

                        break;
                    case TouchPhase.Moved:
                        if (dragging)
                        {
                            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(finger).position);
                            Vector3 fingerPoint = new Vector3(Mathf.Clamp(worldPoint.x, -XLimit, XLimit), Mathf.Clamp(worldPoint.y, -YLimit + offset, YLimit), 0.0f);
                            rigidBody.velocity = (fingerPoint - transform.position) * speed;
                        }

                        break;
                    case TouchPhase.Ended:
                        if (finger == touch.fingerId && dragging)
                        {
                            dragging = false;
                            StoppedDragging();
                        }

                        break;
                }
            }
        }

        public abstract void StoppedDragging();
        public abstract void StartedDragging();

        private void HandleMouse()
        {
            if (Input.GetMouseButtonDown(0) && CheckIfObjectIsTouched(Input.mousePosition))
            {
                rigidBody.gravityScale = 1;
                dragging = true;
                StartedDragging();
            }
            else if (Input.GetMouseButton(0) && dragging)
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 fingerPoint = new Vector3(Mathf.Clamp(worldPoint.x, -XLimit, XLimit), Mathf.Clamp(worldPoint.y, -YLimit + offset, YLimit), 0.0f);
                rigidBody.velocity = (fingerPoint - transform.position) * speed;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (dragging)
                {
                    dragging = false;
                    StoppedDragging();
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
