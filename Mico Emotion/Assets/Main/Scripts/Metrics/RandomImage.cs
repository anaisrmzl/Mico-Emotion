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

        #region BEHAVIORS

        private void Awake()
        {
            imageElement = GetComponent<Image>();
            imageElement.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        #endregion
    }
}
