using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Accelerometer;
using Zenject;

namespace Emotion.Screen2
{
    public class Dizziness : MonoBehaviour
    {
        [Inject] private InteractableCharacter interactableCharacter;

        [SerializeField] private AccelerometerShake accelerometerShake;
        [SerializeField] private AnimationClip dizzyAnimation;
        [SerializeField] private int value;

        private void Awake()
        {
            accelerometerShake.shakeDevice += CharacterGoesDizzy;
        }

        private void CharacterGoesDizzy()
        {
            interactableCharacter.PlayAnimation(dizzyAnimation, value, transform.name);
        }
    }
}
