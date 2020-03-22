using UnityEngine;

using Utilities.Zenject;
using Zenject;
using Utilities.Extensions;

namespace Emotion.Screen2
{
    public class RandomInteractions : MonoBehaviour
    {
        #region FIELDS

        private const string BurpId = "burp";
        private const string SneezeId = "sneeze";

        [SerializeField] private Spider spider;
        [SerializeField] private AnimationClip burpAnimation;
        [SerializeField] private AnimationClip sneezeAnimation;
        [SerializeField] private DragTouchable[] dragables;
        [SerializeField] private DragInteractable interactable;

        [Inject] private InteractableCharacter interactableCharacter;

        public bool dragableOnScene = false;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            interactableCharacter.idle += GenerateRandomInteraction;
        }

        private void OnDestroy()
        {
            interactableCharacter.idle -= GenerateRandomInteraction;
        }

        private void GenerateRandomInteraction()
        {
            if (interactableCharacter.WaitingInteraction)
                return;

            switch (EnumExtensions.GetRandomEnum<Interactions>())
            {
                case Interactions.Food:
                    InstantiateFood();
                    break;
                case Interactions.Burp:
                    interactableCharacter.PlayAnimation(burpAnimation, 0, BurpId);
                    break;
                case Interactions.Spider:
                    spider.AppearSpider();
                    break;
                case Interactions.Sneeze:
                    InstantiateTissue();
                    break;
            }
        }

        public void IsDragableOnScene(bool status)
        {
            dragableOnScene = status;
        }

        private void InstantiateFood()
        {
            if (dragableOnScene)
            {
                GenerateRandomInteraction();
            }
            else
            {
                DragTouchable chosen = dragables[Random.Range(0, dragables.Length)];
                ZenjectUtilities.Instantiate<DragTouchable>(chosen, chosen.transform.position, chosen.transform.rotation, null);
            }
        }

        private void InstantiateTissue()
        {
            interactableCharacter.PlayAnimation(sneezeAnimation, 0, SneezeId);
            ZenjectUtilities.Instantiate<DragInteractable>(interactable, interactable.transform.position, interactable.transform.rotation, null);
        }

        #endregion
    }
}
