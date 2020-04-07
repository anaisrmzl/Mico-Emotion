using UnityEngine;
using UnityEngine.UI;

namespace Utilities.URL
{
    [RequireComponent(typeof(Button))]
    public class OpenURL : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private string URL;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(GoToURL);
        }

        private void GoToURL()
        {
            Application.OpenURL(URL);
        }

        #endregion
    }
}
