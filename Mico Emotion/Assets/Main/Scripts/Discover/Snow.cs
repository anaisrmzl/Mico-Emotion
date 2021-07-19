using UnityEngine;

using DG.Tweening;

namespace Emotion.Discover
{
    public class Snow : MonoBehaviour
    {
        #region PROPERTIES

        private float Height { get => Camera.main.orthographicSize * 2.0f; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            MoveDown();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private void ResetPosition()
        {
            transform.position = new Vector3(transform.position.x, Height, transform.position.z);
            MoveDown();
        }

        private void MoveDown()
        {
            transform.DOMoveY(-Height, (transform.position.y + Height)).OnComplete(ResetPosition).SetEase(Ease.Linear);
        }

        #endregion
    }
}
