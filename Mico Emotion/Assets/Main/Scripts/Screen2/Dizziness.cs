using UnityEngine;

using Utilities.Accelerometer;
using Zenject;

namespace Emotion.Screen2
{
    public class Dizziness : MonoBehaviour
    {
        #region FIELDS

        [Inject] private InteractableCharacter interactableCharacter;

        [SerializeField] private AccelerometerShake accelerometerShake;
        [SerializeField] private AnimationClip dizzyAnimation;
        [SerializeField] private int value;

        #endregion

        #region  BEHAVIORS

        private void Awake()
        {
            accelerometerShake.shakeDevice += CharacterGoesDizzy;
        }

        private void CharacterGoesDizzy()
        {
            interactableCharacter.PlayAnimation(dizzyAnimation, value, transform.name);
        }

        #endregion
    }
}
