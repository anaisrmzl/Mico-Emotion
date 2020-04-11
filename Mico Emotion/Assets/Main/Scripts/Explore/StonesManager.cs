using UnityEngine;
using UnityEngine.Events;

using Utilities.Zenject;
using Utilities.Sound;
using Zenject;

namespace Emotion.Explore
{
    public class StonesManager : MonoBehaviour
    {
        #region FIELDS

        public const string PileTag = "Pile";
        private const int MaxStones = 7;
        private const int MaxHeight = 7;
        private const int RockSortOrder = 1;
        private const int FaceSortOrder = 2;
        private const float MaxScale = 1.0f;
        private const float MinScale = 0.5f;

        [Inject] private SoundManager soundManager;

        [SerializeField] private DragStone[] stones;
        [SerializeField] private AudioClip stonesAudio;
        [SerializeField] private Vector2 spawningPoint;

        #endregion

        #region EVENTS

        public event UnityAction<bool> finished;

        #endregion

        #region PROPERTIES

        public float BoundY { get; private set; }
        public float BaseX { get => transform.GetChild(0).transform.position.x; }
        public bool FinishedPile { get => (transform.childCount >= MaxStones || BoundY >= MaxHeight); }

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
            newStone.GetComponent<SpriteRenderer>().sortingOrder = RockSortOrder;
            newStone.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = FaceSortOrder;
            soundManager.PlayEffect(stonesAudio);
            SetBoundY();
        }

        private void SetBoundY()
        {
            Collider2D objectCollider = transform.GetChild(transform.childCount - 1).GetComponent<Collider2D>();
            BoundY = objectCollider.bounds.max.y;
            finished?.Invoke(FinishedPile);
            InstantiateStone();
        }

        private void InstantiateStone()
        {
            if (FinishedPile)
                return;

            Vector3 position = new Vector3(Random.Range(-spawningPoint.x, spawningPoint.x), spawningPoint.y, 0.0f);
            DragStone chosen = stones[Random.Range(0, stones.Length)];
            DragStone stone = ZenjectUtilities.Instantiate<DragStone>(chosen, position, chosen.transform.rotation, null);
            stone.Initialize(this);
            stone.transform.localScale = Vector3.one * Random.Range(MinScale, MaxScale);
        }

        #endregion
    }
}
