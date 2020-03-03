using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Emotion.Screen1
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
            SceneManager.LoadScene(sceneName);
        }

        #endregion
    }
}
