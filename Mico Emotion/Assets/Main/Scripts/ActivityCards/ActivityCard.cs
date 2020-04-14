using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Sound;
using Zenject;

namespace Emotion.ActivityCards
{
    public class ActivityCard : MonoBehaviour
    {
        #region FIELDS

        private const float TimeToAppear = 10.0f;//900.0f;
        private const float TimeToSeeCard = 10.0f;
        private const string InTrigger = "In";
        private const string OutTrigger = "Out";

        [Inject] private SoundManager soundManager;

        [SerializeField] private Sprite[] cards;
        [SerializeField] private Sprite[] closeButtons;
        [SerializeField] private Image cardImage;
        [SerializeField] private Image closeButtonImage;
        [SerializeField] private Animator cardAnimator;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button blackPanel;

        private float counter = 0.0f;
        private bool count = true;
        private int cardIndex = 0;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            cardIndex = Random.Range(0, cards.Length);
            closeButton.onClick.AddListener(CloseCard);
            blackPanel.onClick.AddListener(CloseCard);
        }

        private void Update()
        {
            if (!count)
                return;

            counter += Time.deltaTime;
            if (counter >= TimeToAppear)
                StartCoroutine(ShowCard());
        }

        private IEnumerator ShowCard()
        {
            count = false;
            counter = 0.0f;
            cardIndex++;
            cardIndex = cardIndex >= cards.Length ? 0 : cardIndex;
            cardImage.sprite = cards[cardIndex];
            closeButtonImage.sprite = closeButtons[cardIndex];
            cardAnimator.SetTrigger(InTrigger);
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0;
            soundManager.PauseMusic(true);
            soundManager.StopEffect();
            soundManager.StopVoice();
            yield return new WaitForSeconds(TimeToSeeCard);
            CloseCard();
        }

        private void CloseCard()
        {
            Time.timeScale = 1;
            soundManager.PauseMusic(false);
            StopAllCoroutines();
            cardAnimator.SetTrigger(OutTrigger);
            count = true;
        }

        #endregion
    }
}
