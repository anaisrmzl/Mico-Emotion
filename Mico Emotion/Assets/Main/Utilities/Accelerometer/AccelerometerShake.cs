using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Accelerometer
{
    public class AccelerometerShake : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private float shakeDetectionTreshold = 2.0f;

        private float accelerometerUpdateInterval = 1.0f / 60.0f;
        private float lowPassKernelWithinSeconds = 1.0f;
        private float lowPassFilterFactor;
        private Vector3 lowPassValue;

        #endregion

        #region EVENTS

        public event UnityAction shakeDevice;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWithinSeconds;
            shakeDetectionTreshold *= shakeDetectionTreshold;
            lowPassValue = Input.acceleration;
        }

        private void Update()
        {
            Vector3 acceleration = Input.acceleration;
            lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
            Vector3 deltaAcceleration = acceleration - lowPassValue;

            if (deltaAcceleration.sqrMagnitude >= shakeDetectionTreshold)
                shakeDevice?.Invoke();
        }

        #endregion
    }
}
