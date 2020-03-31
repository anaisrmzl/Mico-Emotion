using UnityEngine;

using Utilities.Scenes;
using Zenject;

using Emotion.Data;

namespace Emotion.Explore
{
    public class StonesGameManager : MonoBehaviour
    {
        #region FIELDS

        private const int MaxFinishedToWin = 3;

        [Inject] private UserManager userManager;

        [SerializeField] StonesManager blueManager;
        [SerializeField] StonesManager pinkManager;
        [SerializeField] StonesManager yellowManager;

        private int finishCounter = 0;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            userManager.UpdateCompletedStonesGame(false);
            blueManager.finished += CountFinished;
            pinkManager.finished += CountFinished;
            yellowManager.finished += CountFinished;
        }

        private void OnDestroy()
        {
            blueManager.finished -= CountFinished;
            pinkManager.finished -= CountFinished;
            yellowManager.finished -= CountFinished;
        }

        private void CountFinished(bool stauts)
        {
            if (!stauts)
                return;

            finishCounter++;
            if (finishCounter != MaxFinishedToWin)
                return;

            userManager.UpdateCompletedStonesGame(true);
            AnimationSceneChanger.ChangeScene(SceneNames.Meditation);
        }

        #endregion
    }
}
