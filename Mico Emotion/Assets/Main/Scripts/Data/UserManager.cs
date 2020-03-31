using UnityEngine;

using Zenject;
using Utilities.Data;

namespace Emotion.Data
{
    public class UserManager : MonoBehaviour
    {
        #region FIELDS

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

        #endregion
    }
}
