using UnityEngine;

namespace Utilities.Sound
{
    public partial class SoundManager : MonoBehaviour
    {
        #region FIELDS

        [Header("SFX")]
        [SerializeField] private AudioClip audioGeneral;
        [SerializeField] private AudioClip audioIncrease;
        [SerializeField] private AudioClip audioDecrease;

        [Header("Music")]
        [SerializeField] private AudioClip musicJungle;

        #endregion

        #region PROPERTIES

        public AudioClip AudioGeneral { get => audioGeneral; }
        public AudioClip AudioIncrease { get => audioIncrease; }
        public AudioClip AudioDecrease { get => audioDecrease; }
        public AudioClip MusicJungle { get => musicJungle; }

        #endregion
    }
}
