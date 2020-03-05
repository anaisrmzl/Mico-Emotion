using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.Scenes
{
    public class FadeSceneChanger : MonoBehaviour
    {
        #region FIELDS

        private const float TransparentAlpha = 0f;
        private const float VisibleAlpha = 1f;
        private const string PathToPrefab = "Scene/FadeSceneChanger";

        [SerializeField] private CanvasGroup canvasGroup = null;

        #endregion

        #region BEHAVIOURS

        public static void ChangeScene(string sceneName, float fadeDuration = 0.5f)
        {
            FadeSceneChanger fadeScenePrefab = Resources.Load<FadeSceneChanger>(PathToPrefab);
            Instantiate(fadeScenePrefab, Vector3.zero, Quaternion.identity).FadeIntoScene(sceneName, fadeDuration);
        }

        public static void FadeCanvas(float fadeDuration = 0.5f, float stayDuration = 0.0f)
        {
            FadeSceneChanger fadeScenePrefab = Resources.Load<FadeSceneChanger>(PathToPrefab);
            Instantiate(fadeScenePrefab, Vector3.zero, Quaternion.identity).FadeOnlyCanvas(fadeDuration, stayDuration);
        }

        private void FadeIntoScene(string sceneName, float fadeDuration)
        {
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(FadeIntoSceneCoroutine(sceneName, fadeDuration));
        }

        private void FadeOnlyCanvas(float fadeDuration, float stayDuration)
        {
            StartCoroutine(FadeOnlyCanvasCoroutine(fadeDuration, stayDuration));
        }

        private IEnumerator FadeIntoSceneCoroutine(string sceneName, float fadeDuration)
        {
            yield return FadeCanvasGroup(TransparentAlpha, VisibleAlpha, fadeDuration);
            SceneManager.LoadScene(sceneName);
            yield return FadeCanvasGroup(VisibleAlpha, TransparentAlpha, fadeDuration);
            Destroy(this.gameObject);
        }

        private IEnumerator FadeOnlyCanvasCoroutine(float fadeDuration, float stayDuration)
        {
            yield return FadeCanvasGroup(TransparentAlpha, VisibleAlpha, fadeDuration);
            yield return new WaitForSeconds(stayDuration);
            yield return FadeCanvasGroup(VisibleAlpha, TransparentAlpha, fadeDuration);
            Destroy(this.gameObject);
        }

        private IEnumerator FadeCanvasGroup(float from, float to, float fadeDuration)
        {
            float elapsedTime = default(float);
            while (elapsedTime < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(from, to, elapsedTime / fadeDuration);
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }
            canvasGroup.alpha = to;
        }

        #endregion
    }
}
