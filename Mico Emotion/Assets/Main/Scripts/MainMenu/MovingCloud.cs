using UnityEngine;

using DG.Tweening;

namespace Emotion.MainMenu
{
    public class MovingCloud : MonoBehaviour
    {
        #region FIELDS

        private const float MinSpeed = 25.0f;
        private const float MaxSpeed = 40.0f;
        private const float MaxXPosition = 25.0f;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            float startValue = Random.value > 0.5 ? 1.0f : -1.0f;
            float speed = Random.Range(MinSpeed, MaxSpeed);
            transform.DOMoveX(-MaxXPosition * startValue, speed).From(MaxXPosition * startValue).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).Goto(Random.Range(0, speed), true);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        #endregion
    }
}
