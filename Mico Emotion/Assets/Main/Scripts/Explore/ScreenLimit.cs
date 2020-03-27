using UnityEngine;

namespace Emotion.Explore
{
    public class ScreenLimit : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private Vector2 coordinates;
        [SerializeField] private bool offsetScale;

        private float Height { get => Camera.main.orthographicSize * 2.0f; }
        private float Width { get => Height * Camera.main.aspect; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            transform.position = new Vector3((Width + (offsetScale ? transform.localScale.x : 0)) / 2.0f, (Height + transform.localScale.y) / 2.0f, transform.position.z) * coordinates;
        }

        #endregion
    }
}
