using UnityEngine;

namespace Utilities.Sound
{
    public partial class SoundManager : MonoBehaviour
    {
        #region FIELDS

        [Header("SFX")]
        [SerializeField] private AudioClip audioGeneral;

        [Header("Music")]
        [SerializeField] private AudioClip musicJungle;

        #endregion

        #region PROPERTIES

        public AudioClip AudioGeneral { get => audioGeneral; }
        public AudioClip MusicJungle { get => musicJungle; }

        #endregion
    }
}
