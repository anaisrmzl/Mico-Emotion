using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.Scenes
{
    public class AnimationSceneChanger : MonoBehaviour
    {
        #region FIELDS

        private const string PathToPrefab = "Scene/AnimationSceneChanger";

        [SerializeField] private Animator animator = null;
        [SerializeField] private AnimationClip transitionAnimation = null;

        #endregion

        #region BEHAVIORS

        public static void ChangeScene(string sceneName)
        {
            AnimationSceneChanger fadeScenePrefab = Resources.Load<AnimationSceneChanger>(PathToPrefab);
            Instantiate(fadeScenePrefab, Vector3.zero, Quaternion.identity).AnimateIntoScene(sceneName);
        }

        private void AnimateIntoScene(string sceneName)
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
