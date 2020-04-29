using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Sound;
using Zenject;

namespace Emotion.Discover
{
    [RequireComponent(typeof(Button))]
    public class InteractiveStoryElement : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private Button button;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ToggleSprite);
        }

        private void ToggleSprite()
        {
            soundManager.PlayEffect(soundManager.AudioGeneral);
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }

        #endregion
    }
}
