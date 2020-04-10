using System;
using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Metrics
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldLimit : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private int maxValue;

        private InputField inputField;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            inputField = GetComponent<InputField>();
            inputField.onValueChanged.AddListener(LimitValue);
        }

        private void LimitValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            if (Int32.Parse(value) > maxValue)
                inputField.text = maxValue.ToString();
        }

        #endregion
    }
}
