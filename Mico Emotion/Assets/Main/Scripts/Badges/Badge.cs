using UnityEngine;

using Utilities.Data;

namespace Emotion.Badges
{
    [CreateAssetMenu(menuName = ScriptableCreationRoute)]
    public class Badge : ScriptableObject
    {
        #region FIELDS

        private const string ScriptableCreationRoute = "Emotion/Badges/Badge";

        [SerializeField] private new string name = null;
        [SerializeField] string id = null;
        [SerializeField] private BadgeType type;
        [SerializeField] private bool acquired = false;
        [SerializeField] private Sprite sprite = null;
        [SerializeField] private AudioClip description = null;
        [SerializeField] private AudioClip title = null;

        private DataManager dataManager = null;
        private string[] keys = null;

        #endregion

        #region PROPERTIES

        public string Name { get => name; }
        public string Id { get => id; }
        public BadgeType Type { get => type; }
        public bool Acquired { get => acquired; }
        public Sprite Sprite { get => sprite; }
        public AudioClip Description { get => description; }
        public AudioClip Title { get => title; }

        #endregion

        #region BEHAVIORS

        public void Load(DataManager dataManager)
        {
            keys = DataManager.GenerateKeys(GameKeys.BadgesKey, Id);
            this.dataManager = dataManager;
            Reload();
        }

        public void Reload()
        {
            acquired = dataManager.GetData<bool>(keys, false);
        }

        public void AcquireBadge()
        {
            acquired = true;
            dataManager.SetData(keys, Acquired);
        }

        #endregion
    }
}
