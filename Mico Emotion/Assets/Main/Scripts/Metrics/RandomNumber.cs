using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Metrics
{
    [RequireComponent(typeof(Text))]
    public class RandomNumber : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;

        private Text textElement;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            textElement = GetComponent<Text>();
            textElement.text = Random.Range(minValue, maxValue).ToString();
        }

        #endregion
    }
}
