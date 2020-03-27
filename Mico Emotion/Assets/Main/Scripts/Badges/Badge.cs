using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Emotion.Badges
{
    [CreateAssetMenu(menuName = ScriptableCreationRoute)]
    public class Badge : ScriptableObject
    {
        #region FIELDS

        private const string ScriptableCreationRoute = "Emotion/Badges/Badge";

        [SerializeField] private new string name = null;
        [SerializeField] private int id = default(int);
        [SerializeField] private BadgeType type;
        [SerializeField] private Sprite sprite = null;
        [SerializeField] private AudioClip description = null;

        #endregion

        #region PROPERTIES

        public string Name { get => name; }
        public int Id { get => id; }
        public BadgeType Type { get => type; }
        public Sprite Sprite { get => sprite; }
        public AudioClip Description { get => description; }

        #endregion
    }
}
