using UnityEngine;

using Utilities.Zenject;

namespace Emotion.Screen4
{
    public class StonesManager : MonoBehaviour
    {
        #region FIELDS

        public const string PileTag = "Pile";
        private const int MaxStones = 6;

        [SerializeField] private DragStone[] stones;
        [SerializeField] private Vector2 spawningPoint;

        #endregion

        #region PROPERTIES

        public float BoundY { get; private set; }
        public float BaseX { get => transform.GetChild(0).transform.position.x; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            SetBoundY();
        }

        public void AddToThePile(GameObject newStone)
        {
            newStone.transform.SetParent(transform);
            newStone.tag = PileTag;
            newStone.GetComponent<SpriteRenderer>().sortingOrder = 0;
            SetBoundY();
        }

        private void SetBoundY()
        {
            InstantiateStone();
            Collider2D objectCollider = transform.GetChild(transform.childCount - 1).GetComponent<Collider2D>();
            BoundY = objectCollider.bounds.max.y;
        }

        private void InstantiateStone()
        {
            if (transform.childCount >= MaxStones)
                return;

            Vector3 position = new Vector3(Random.Range(-spawningPoint.x, spawningPoint.x), spawningPoint.y, 0.0f);
            DragStone chosen = stones[Random.Range(0, stones.Length)];
            DragStone stone = ZenjectUtilities.Instantiate<DragStone>(chosen, position, chosen.transform.rotation, null);
            stone.transform.localScale = chosen.transform.localScale;
            stone.Initialize(this);
        }

        #endregion
    }
}
