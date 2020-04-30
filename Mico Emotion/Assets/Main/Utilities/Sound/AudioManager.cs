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
        [SerializeField] private AudioClip audioCompleteBar;
        [SerializeField] private AudioClip audioWin;
        [SerializeField] private AudioClip audioTietoEmotions;
        [SerializeField] private AudioClip audioTakeStone;

        [Header("Music")]
        [SerializeField] private AudioClip musicJungle;
        [SerializeField] private AudioClip musicSky;
        [SerializeField] private AudioClip musicDiscover;
        [SerializeField] private AudioClip musicBadges;
        [SerializeField] private AudioClip musicStones;

        #endregion

        #region PROPERTIES

        public AudioClip AudioGeneral { get => audioGeneral; }
        public AudioClip AudioIncrease { get => audioIncrease; }
        public AudioClip AudioDecrease { get => audioDecrease; }
        public AudioClip AudioCompleteBar { get => audioCompleteBar; }
        public AudioClip AudioTietoEmotions { get => audioTietoEmotions; }
        public AudioClip AudioWin { get => audioWin; }
        public AudioClip AudioTakeStone { get => audioTakeStone; }
        public AudioClip MusicJungle { get => musicJungle; }
        public AudioClip MusicStones { get => musicStones; }
        public AudioClip MusicSky { get => musicSky; }
        public AudioClip MusicDiscover { get => musicDiscover; }
        public AudioClip MusicBadges { get => musicBadges; }

        #endregion
    }
}
