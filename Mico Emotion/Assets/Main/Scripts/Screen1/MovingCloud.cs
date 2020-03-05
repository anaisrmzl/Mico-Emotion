using UnityEngine;

using DG.Tweening;

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
        Repeat();
    }

    private void Repeat()
    {
        float startValue = Random.value > 0.5 ? 1.0f : -1.0f;
        transform.DOMoveX(-MaxXPosition * startValue, Random.Range(MinSpeed, MaxSpeed)).From(MaxXPosition * startValue).OnComplete(Repeat);
    }

    #endregion
}
