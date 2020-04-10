using UnityEngine;

using Zenject;
using Utilities.Data;

using Emotion.Badges;

namespace Emotion.Data
{
    public class UserManager : MonoBehaviour
    {
        #region FIELDS

        private const string MiedoBadge = "miedo";
        private const string CarinoBadge = "carino";
        private const string GratitudBadge = "gratitud";

        [Inject] private DataManager dataManager;

        #endregion

        #region PROPERTIES

        public bool CompletedStonesGame
        {
            get => dataManager.GetData<bool>(DataManager.GenerateKeys(APIKeys.CompletedStonesGameKey), false);
            private set => dataManager.SetData<bool>(DataManager.GenerateKeys(APIKeys.CompletedStonesGameKey), value);
        }

        public bool CompletedMeditationGame
        {
            get => dataManager.GetData<bool>(DataManager.GenerateKeys(APIKeys.CompletedMeditationGameKey), false);
            private set => dataManager.SetData<bool>(DataManager.GenerateKeys(APIKeys.CompletedMeditationGameKey), value);
        }

        public int LastGamePlayed
        {
            get => dataManager.GetData<int>(DataManager.GenerateKeys(APIKeys.LastGamePlayedKey), default(int));
            private set => dataManager.SetData<int>(DataManager.GenerateKeys(APIKeys.LastGamePlayedKey), value);
        }

        public string LastRecognizeBadgeWon
        {
            get => dataManager.GetData<string>(DataManager.GenerateKeys(APIKeys.LastRecognizeBadgeWonKey), CarinoBadge);
            private set => dataManager.SetData<string>(DataManager.GenerateKeys(APIKeys.LastRecognizeBadgeWonKey), value);
        }

        public string LastDiscoverBadgeWon
        {
            get => dataManager.GetData<string>(DataManager.GenerateKeys(APIKeys.LastDiscoverBadgeWonKey), MiedoBadge);
            private set => dataManager.SetData<string>(DataManager.GenerateKeys(APIKeys.LastDiscoverBadgeWonKey), value);
        }

        public string LastExploreBadgeWon
        {
            get => dataManager.GetData<string>(DataManager.GenerateKeys(APIKeys.LastExploreBadgeWonKey), GratitudBadge);
            private set => dataManager.SetData<string>(DataManager.GenerateKeys(APIKeys.LastExploreBadgeWonKey), value);
        }

        #endregion

        #region BEHAVIORS

        public void UpdateCompletedStonesGame(bool status)
        {
            CompletedStonesGame = status;
        }

        public void UpdateCompletedMeditationGame(bool status)
        {
            CompletedMeditationGame = status;
        }

        public void UpdateLastGamePlayed(int game)
        {
            LastGamePlayed = game;
        }

        public void UpdateLastRecognizeBadgeWon(string id)
        {
            LastRecognizeBadgeWon = id;
        }

        public void UpdateLastDiscoverBadgeWon(string id)
        {
            LastDiscoverBadgeWon = id;
        }

        public void UpdateLastExploreBadgeWon(string id)
        {
            LastExploreBadgeWon = id;
        }

        public void UpdateLastBadgeWon(string id, BadgeType badgeType)
        {
            switch (badgeType)
            {
                case BadgeType.Recognize:
                    UpdateLastRecognizeBadgeWon(id);
                    break;
                case BadgeType.Discover:
                    UpdateLastDiscoverBadgeWon(id);
                    break;
                case BadgeType.Explore:
                    UpdateLastExploreBadgeWon(id);
                    break;
            }
        }

        public string GetLastBadgeWon(BadgeType badgeType)
        {
            switch (badgeType)
            {
                case BadgeType.Recognize:
                    return LastRecognizeBadgeWon;
                case BadgeType.Discover:
                    return LastDiscoverBadgeWon;
                case BadgeType.Explore:
                default:
                    return LastExploreBadgeWon;
            }
        }

        #endregion
    }
}
