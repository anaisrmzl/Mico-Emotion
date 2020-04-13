using System.Collections;
using UnityEngine;

using Utilities.Scenes;
using Utilities.Sound;
using Zenject;

namespace Emotion.Recognize
{
    public class Goodbye : MonoBehaviour
    {
        #region FIELDS

        [Inject] private SoundManager soundManager;

        #endregion

        #region BEHAVIORS

        public void ReturnToGameSelection(float length)
        {
            soundManager.PlayEffect(soundManager.AudioGeneral);
            StartCoroutine(GoodbyeAnimation(length));
        }

        private IEnumerator GoodbyeAnimation(float length)
        {
            yield return new WaitForSeconds(length / 2.0f);
            AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
        }

        #endregion
    }
}
