using System.Collections;
using UnityEngine;

namespace Emotion.MainMenu
{
    public class LeavesController : MonoBehaviour
    {
        #region FIELDS

        private const float WaitTime = 5.0f;
        private const float PiecesDuration = 10.0f;

        [SerializeField] private GameObject frontParticles;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            StartCoroutine(PlayParticles());
        }

        private IEnumerator PlayParticles()
        {
            yield return new WaitForSeconds(WaitTime);
            GetAndPlayParticles();
            yield return new WaitForSeconds(PiecesDuration);
            StartCoroutine(PlayParticles());
        }

        private void GetAndPlayParticles()
        {
            foreach (Transform particle in frontParticles.transform)
                particle.GetComponent<ParticleSystem>().Play();
        }

        #endregion
    }
}
