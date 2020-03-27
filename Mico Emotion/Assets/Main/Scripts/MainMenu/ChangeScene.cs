using UnityEngine;
using UnityEngine.UI;

using Utilities.Scenes;

namespace Emotion.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class ChangeScene : MonoBehaviour
    {
        #region FIELDS

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
            AnimationSceneChanger.ChangeScene(sceneName);
        }

        #endregion
    }
}
