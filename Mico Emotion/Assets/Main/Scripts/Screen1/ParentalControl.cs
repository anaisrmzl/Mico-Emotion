using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using DG.Tweening;
using Utilities.Scenes;

namespace Emotion.Screen1
{
    [RequireComponent(typeof(Button))]
    public class ParentalControl : MonoBehaviour
    {
        #region FIELDS

        private const int YearLength = 4;
        private const int MinAgeAllowed = 14;
        private const int MaxAgeAllowed = 120;
        private const float ShakeDuration = 0.5f;
        private const float ShakeStrength = 50.0f;
        private const int MaxAttempts = 3;

        [SerializeField] private GameObject parentalControlPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button sendButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject container;
        [SerializeField] private AnimationClip pressAnimation;
        [SerializeField] private CanvasGroup canvasGroup;

        private Button openButton;
        private int attempts = 0;
        private Animator animator;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            openButton = GetComponent<Button>();
            animator = GetComponent<Animator>();
            openButton.onClick.AddListener(PressButton);
            closeButton.onClick.AddListener(CloseParentalControl);
            inputField.onValueChanged.AddListener(CheckInputFieldLength);
            sendButton.onClick.AddListener(SendAnswer);
        }

        private void PressButton()
        {
            animator.Play(pressAnimation.name);
            canvasGroup.blocksRaycasts = false;
            StartCoroutine(OpenParentalControl());
        }

        private IEnumerator OpenParentalControl()
        {
            yield return new WaitForSeconds(pressAnimation.length);
            parentalControlPanel.SetActive(true);
            canvasGroup.blocksRaycasts = true;
        }

        private void CloseParentalControl()
        {
            parentalControlPanel.SetActive(false);
            inputField.text = string.Empty;
        }

        private void CheckInputFieldLength(string value)
        {
            sendButton.interactable = value.Length > 0;
        }

        private void SendAnswer()
        {
            if (CheckAge())
            {
                AnimationSceneChanger.ChangeScene(SceneNames.ForParents);
            }
            else
            {
                attempts++;
                if (attempts == MaxAttempts)
                {
                    attempts = 0;
                    CloseParentalControl();
                }

                container.transform.DOShakePosition(ShakeDuration, new Vector3(ShakeStrength, 0.0f, 0.0f), randomness: 0, fadeOut: false);
            }
        }

        private bool CheckAge()
        {
            if (inputField.text.Length < YearLength)
                return false;
            else
                return (DateTime.Today.Year - int.Parse(inputField.text) >= MinAgeAllowed && DateTime.Today.Year - int.Parse(inputField.text) <= MaxAgeAllowed);
        }

        #endregion
    }
}
