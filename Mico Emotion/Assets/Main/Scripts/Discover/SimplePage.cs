using System.Collections;
using UnityEngine;

namespace Emotion.Discover
{
    public class SimplePage : Page
    {
        #region BEHAVIORS

        public override void Initialize()
        {
            StartCoroutine(PlayNarrative());
        }

        private IEnumerator PlayNarrative()
        {
            yield return new WaitForSeconds(1.0f);
            soundManager.PlayVoice(narrativeAudio);
            yield return new WaitForSeconds(narrativeAudio.length);
        }

        #endregion
    }
}
