using UnityEngine;

namespace Utilities.UI
{
    public class FollowUI : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private GameObject target;

        private float offset;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            offset = Camera.main.ViewportToWorldPoint(Vector2.right).x;
        }

        private void Update()
        {
            transform.position = new Vector3(target.transform.position.x + offset, transform.position.y, transform.position.z);
        }

        #endregion
    }
}
