using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

namespace Emotion.Discover
{
    public class InteractivePage : Page
    {
        #region FIELDS

        private const float MaxInactiveTime = 5.0f;

        [Inject] private StoryManager storyManager;

        [SerializeField] private int maxCorrectAnswers = 0;
        [SerializeField] private List<InteractionAnswer> interactiveButtons = new List<InteractionAnswer>();

        private bool count = false;
        private float counter = 0.0f;
        private Coroutine timer = null;
        private int answers = 0;

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

        private void OnEnable()
        {
            Initialize();
        }

        public override void Initialize()
        {
            foreach (InteractionAnswer button in interactiveButtons)
                button.Initialize();

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
            DisableButtons();
            yield return new WaitForSeconds(1.0f);
            yield return PlayNarrative();
            count = true;
            EnableButtons();
        }

        private IEnumerator PlayNarrative()
        {
            soundManager.PlayVoice(narrativeAudio);
            yield return new WaitForSeconds(narrativeAudio.length);
        }

        public void DisableOtherButtons(InteractionAnswer answer, float audioLength, bool touched, bool correct)
        {
            CancelTimer(audioLength);
            List<InteractionAnswer> otherButtons = interactiveButtons.FindAll(element => element != answer);
            foreach (InteractionAnswer button in otherButtons)
                button.EnableButton(false);

            if (correct)
            {
                if (touched)
                    answers++;
            }
            else
            {
                interactiveButtons.Remove(interactiveButtons.Find(element => element == answer));
            }
        }

        public void EnableButtons()
        {
            if (answers == maxCorrectAnswers)
            {
                storyManager.ChangePage();
                return;
            }

            foreach (InteractionAnswer button in interactiveButtons)
                button.EnableButton(true);
        }


        private void DisableButtons()
        {
            foreach (InteractionAnswer button in interactiveButtons)
                button.EnableButton(false);
        }

        #endregion
    }
}
