namespace Emotion.Discover
{
    public class WrongAnswer : AnswerButton
    {
        #region BEHAVIORS

        public override void AfterPlayNarrative()
        {
            otherButton.EnableButton(true);
        }

        #endregion
    }
}
