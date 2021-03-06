﻿using UnityEngine;
using UnityEngine.UI;

using Utilities.Scenes;
using Utilities.Sound;
using Zenject;

namespace Emotion.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ChangeScene : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        [SerializeField] private string sceneName;

        private Button actionButton;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            actionButton = GetComponent<Button>();
            actionButton.onClick.AddListener(GoToScene);
        }

        private void GoToScene()
        {
            actionButton.interactable = false;
            soundManager.StopVoice();
            AnimationSceneChanger.ChangeScene(sceneName);
        }

        #endregion
    }
}
