using UnityEngine;

using Utilities.Accelerometer;
using Zenject;

namespace Emotion.Recognize
{
    public class Dizziness : MonoBehaviour
    {
        #region FIELDS

        [Inject] private InteractableCharacter interactableCharacter;

        [SerializeField] private AccelerometerShake accelerometerShake;
        [SerializeField] private AnimationClip dizzyAnimation;
        [SerializeField] private AudioClip dizzyAudio;
        [SerializeField] private int value;

        #endregion

        #region  BEHAVIORS

        private void Awake()
        {
            accelerometerShake.shakeDevice += CharacterGoesDizzy;
        }

        private void OnDestroy()
        {
            accelerometerShake.shakeDevice -= CharacterGoesDizzy;
        }

        private void CharacterGoesDizzy()
        {
            interactableCharacter.PlayAnimation(dizzyAnimation, dizzyAudio, value, transform.name);
        }

        #endregion
    }
}
