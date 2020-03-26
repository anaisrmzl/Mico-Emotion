using System.Collections;
using UnityEngine;

using Utilities.Scenes;

namespace Emotion.Screen2
{
    public class Goodbye : MonoBehaviour
    {
        #region BEHAVIORS

        public void ReturnToGameSelection(float length)
        {
            StartCoroutine(GoodbyeAnimation(length));
        }

        private IEnumerator GoodbyeAnimation(float length)
        {
            yield return new WaitForSeconds(length);
            AnimationSceneChanger.ChangeScene(SceneNames.GameSelection);
        }

        #endregion
    }
}
