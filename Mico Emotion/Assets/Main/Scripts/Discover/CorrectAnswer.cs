using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Emotion.Discover
{
    [RequireComponent(typeof(Button))]
    public class CorrectAnswer : AnswerButton
    {
        #region FIELDS

        [Inject] private StoryManager storyManager;

        #endregion

        #region BEHAVIORS

        public override void AfterPlayNarrative()
        {
            storyManager.ChangePage();
        }

        #endregion
    }
}
