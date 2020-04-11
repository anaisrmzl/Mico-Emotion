using System.Collections;
using UnityEngine;

using Utilities.Zenject;

namespace Emotion.Recognize
{
    public class Cry : ClickObject
    {
        #region FIELDS

        [SerializeField] private DragInteractable interactable;

        #endregion

        #region BEHAVIORS

        public override void DoDoubleClick()
        {
            StopAllCoroutines();
            if (doubleClickAnimation == null)
                return;

            interactableCharacter.PlayAnimation(doubleClickAnimation, doubleClickAudio, doubleClickValue, transform.name, true);
            interactableCharacter.WaitForInteraction(true);
            StartCoroutine(AppearTissue());
        }

        private IEnumerator AppearTissue()
        {
            yield return new WaitForSeconds(doubleClickAnimation.length);
            ZenjectUtilities.Instantiate<DragInteractable>(interactable, interactable.transform.position, interactable.transform.rotation, null);
        }

        #endregion
    }
}
