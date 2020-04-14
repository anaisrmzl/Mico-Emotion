using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Metrics
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleTransparency : MonoBehaviour
    {
        #region FIELDS

        private Toggle toggle;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(Selected);
            Selected(toggle.isOn);
        }

        private void Selected(bool status)
        {
            toggle.targetGraphic.color = status ? Color.white : new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }

        #endregion
    }
}
