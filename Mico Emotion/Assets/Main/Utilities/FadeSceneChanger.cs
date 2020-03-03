using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.Scenes
{
    public class FadeSceneChanger : MonoBehaviour
    {
        #region FIELDS

        private const string PathToPrefab = "Scene/FadeSceneChanger";

        [SerializeField] private Animator animator = null;
        [SerializeField] private AnimationClip transitionAnimation = null;

        #endregion

        #region BEHAVIOURS

        public static void ChangeScene(string sceneName)
        {
            FadeSceneChanger fadeScenePrefab = Resources.Load<FadeSceneChanger>(PathToPrefab);
            Instantiate(fadeScenePrefab, Vector3.zero, Quaternion.identity).FadeIntoScene(sceneName);
        }

        private void FadeIntoScene(string sceneName)
        {
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(PlayTransitionAnimation(sceneName));
        }

        private IEnumerator PlayTransitionAnimation(string sceneName)
        {
            animator.Play(transitionAnimation.name);
            yield return new WaitForSeconds(transitionAnimation.length);
            SceneManager.LoadScene(sceneName);
            yield return new WaitForSeconds(transitionAnimation.length);
            Destroy(this.gameObject);
        }

        #endregion
    }
}
