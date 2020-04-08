using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Metrics
{
    [RequireComponent(typeof(Image))]
    public class RandomImage : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private Sprite[] sprites;

        private Image imageElement;

        #endregion

        #region PROPERTIES

        public int Index { get; private set; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            imageElement = GetComponent<Image>();
            Index = Random.Range(0, sprites.Length);
            imageElement.sprite = sprites[Index];
        }

        #endregion
    }
}
