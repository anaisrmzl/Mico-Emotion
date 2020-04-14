using UnityEngine;
using UnityEngine.UI;

namespace Emotion.Metrics
{
    public class AddGame : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private InputField dayInputField;
        [SerializeField] private InputField monthInputField;
        [SerializeField] private InputField minutesField;
        [SerializeField] private InputField notesInputField;
        [SerializeField] private Text dayText;
        [SerializeField] private Text monthText;
        [SerializeField] private Text minutesText;
        [SerializeField] private Text notesText;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button addButton;
        [SerializeField] private CanvasGroup activitiesToggleGroup;
        [SerializeField] private GameObject editableSection;
        [SerializeField] private GameObject savedSection;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            addButton.onClick.AddListener(AddNewInfo);
            saveButton.onClick.AddListener(SaveInfo);
            EnableEditableSection(true);
        }

        private void EnableEditableSection(bool status)
        {
            editableSection.SetActive(status);
            activitiesToggleGroup.blocksRaycasts = status;
            savedSection.SetActive(!status);
        }

        private void AddNewInfo()
        {
            EnableEditableSection(true);
            dayInputField.text = string.Empty;
            monthInputField.text = string.Empty;
            minutesField.text = string.Empty;
            notesInputField.text = string.Empty;
        }

        private void SaveInfo()
        {
            dayText.text = dayInputField.text;
            monthText.text = monthInputField.text;
            minutesText.text = minutesField.text;
            notesText.text = notesInputField.text;
            EnableEditableSection(false);
        }

        #endregion
    }
}
