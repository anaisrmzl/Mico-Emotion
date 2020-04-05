using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Scenes;

namespace Emotion.Discover
{
    public class StoryManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private Page[] storyPages;

        private int currentPage = 0;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            EnableNextPage();
        }

        public void ChangePage()
        {
            currentPage++;
            StartCoroutine(FadePage());
        }

        private void EnableNextPage()
        {
            if (currentPage >= storyPages.Length)
            {
                storyPages[currentPage - 1].gameObject.SetActive(false);
                currentPage = 0;
                return;
            }

            storyPages[currentPage].gameObject.SetActive(true);
            if (storyPages[currentPage].PageLength != 0)
                StartCoroutine(ChangeSimplePage());

            if (currentPage == 0)
                return;

            storyPages[currentPage - 1].gameObject.SetActive(false);
        }

        private IEnumerator FadePage()
        {
            yield return new WaitForSeconds(FadeSceneChanger.FadeCanvas(1.0f, 0.5f));
            EnableNextPage();
        }

        private IEnumerator ChangeSimplePage()
        {
            yield return new WaitForSeconds(storyPages[currentPage].PageLength);
            currentPage++;
            StartCoroutine(FadePage());
        }

        #endregion
    }
}
