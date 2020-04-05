using System.Collections;
using UnityEngine;

namespace Emotion.Discover
{
    public class StoryPage : Page
    {
        #region FIELDS

        private const float MaxInactiveTime = 5.0f;

        [SerializeField] private AnswerButton wrongButton;
        [SerializeField] private AnswerButton rightButton;

        private bool count = false;
        private float counter = 0.0f;
        private Coroutine timer = null;

        #endregion

        #region PROPERTIES

        public override float PageLength { get => 0.0f; }

        #endregion

        #region BEHAVIORS

        private void Update()
        {
            if (!count)
                return;

            counter += Time.deltaTime;
            if (counter >= MaxInactiveTime)
                timer = StartCoroutine(ResetTimer());
        }

        private void OnDisable()
        {
            wrongButton.pressedButton -= CancelTimer;
            rightButton.pressedButton -= CancelTimer;
        }

        private void OnEnable()
        {
            Initialize();
        }

        public override void Initialize()
        {
            wrongButton.pressedButton += CancelTimer;
            rightButton.pressedButton += CancelTimer;
            wrongButton.Initialize();
            rightButton.Initialize();
            StartCoroutine(PageSetUp());
        }

        private void CancelTimer(float audioLength)
        {
            count = false;
            counter = 0.0f;
            StartCoroutine(WaitAudio(audioLength));

            if (timer == null)
                return;

            StopCoroutine(timer);
        }

        private IEnumerator WaitAudio(float audioLength)
        {
            yield return new WaitForSeconds(audioLength);
            count = true;
        }

        private IEnumerator ResetTimer()
        {
            count = false;
            counter = 0.0f;
            yield return PlayNarrative();
            count = true;
        }

        private IEnumerator PageSetUp()
        {
            wrongButton.EnableButton(false);
            rightButton.EnableButton(false);
            yield return new WaitForSeconds(1.0f);
            yield return PlayNarrative();
            count = true;
            wrongButton.EnableButton(true);
            rightButton.EnableButton(true);
        }

        private IEnumerator PlayNarrative()
        {
            soundManager.PlayVoice(narrativeAudio);
            yield return new WaitForSeconds(narrativeAudio.length);
        }

        #endregion
    }
}
