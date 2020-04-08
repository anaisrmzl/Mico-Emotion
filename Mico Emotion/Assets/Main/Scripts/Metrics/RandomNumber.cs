using UnityEngine;

using TMPro;

namespace Emotion.Metrics
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RandomNumber : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;

        private TextMeshProUGUI textElement;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            textElement = GetComponent<TextMeshProUGUI>();
            textElement.text = Random.Range(minValue, maxValue).ToString();
        }

        #endregion
    }
}
