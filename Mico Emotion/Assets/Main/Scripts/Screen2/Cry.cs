using UnityEngine;

using Utilities.Zenject;

namespace Emotion.Screen2
{
    public class Cry : ClickObject
    {
        #region FIELDS

        [SerializeField] private DragInteractable interactable;

        #endregion

        #region BEHAVIORS

        public override void DoDoubleClick()
        {
            base.DoDoubleClick();
            interactableCharacter.WaitForInteraction(true);
            ZenjectUtilities.Instantiate<DragInteractable>(interactable, interactable.transform.position, interactable.transform.rotation, null);
        }

        #endregion
    }
}
