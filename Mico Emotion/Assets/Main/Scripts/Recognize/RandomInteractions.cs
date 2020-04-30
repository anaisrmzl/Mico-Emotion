using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Zenject;
using Zenject;
using Utilities.Extensions;

namespace Emotion.Recognize
{
    public class RandomInteractions : MonoBehaviour
    {
        #region FIELDS

        private const string BurpId = "burp";
        private const string SneezeId = "sneeze";
        private const string SurpriseId = "surprise";
        private const string HelpId = "help";
        private const int SockIndex = 2;

        [SerializeField] private Spider spider;
        [SerializeField] private AnimationClip burpAnimation;
        [SerializeField] private AnimationClip helpAnimation;
        [SerializeField] private AnimationClip idleAnimation;
        [SerializeField] private AnimationClip sneezeAnimation;
        [SerializeField] private AnimationClip surpriseAnimation;
        [SerializeField] private AudioClip burpAudio;
        [SerializeField] private AudioClip sneezeAudio;
        [SerializeField] private AudioClip surpriseAudio;
        [SerializeField] private AudioClip smellBadAudio;
        [SerializeField] private AudioClip[] helpAudios;
        [SerializeField] private DragTouchable[] dragables;
        [SerializeField] private DragInteractable interactable;

        [Inject] private InteractableCharacter interactableCharacter;

        private bool isDragging = false;
        private GameObject currentDragable;
        private List<int> dragableIndexes = new List<int>();
        private int currentDragableIndex;

        private List<int> randomInteractionIndexes = new List<int>();
        private int interactionIndex;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            currentDragableIndex = dragables.Length - 1;
            for (int i = 0; i < currentDragableIndex; i++)
                dragableIndexes.Add(i);

            randomInteractionIndexes = EnumExtensions.GetEnumsAsIntList<Interactions>();
            interactionIndex = randomInteractionIndexes.Count - 1;
            randomInteractionIndexes.Remove(interactionIndex);

            interactableCharacter.idle += GenerateRandomInteraction;
        }

        private void OnDestroy()
        {
            interactableCharacter.idle -= GenerateRandomInteraction;
        }

        private void GenerateRandomInteraction()
        {
            if (interactableCharacter.WaitingInteraction || isDragging)
                return;

            switch ((Interactions)interactionIndex)
            {
                case Interactions.Food:
                    InstantiateFood();
                    break;
                case Interactions.Burp:
                    interactableCharacter.PlayAnimation(burpAnimation, burpAudio, 0, HelpId);
                    break;
                case Interactions.Spider:
                    spider.AppearSpider();
                    break;
                case Interactions.Sneeze:
                    InstantiateTissue();
                    break;
                case Interactions.Help:
                    interactableCharacter.PlayAnimation(helpAnimation, helpAudios[Random.Range(0, helpAudios.Length)], 0, HelpId);
                    break;
            }

            int oldIndex = interactionIndex;
            interactionIndex = randomInteractionIndexes[Random.Range(0, randomInteractionIndexes.Count)];
            randomInteractionIndexes.Remove(interactionIndex);
            randomInteractionIndexes.Add(oldIndex);
        }

        private bool IsDragableOnScene()
        {
            return currentDragable != null;
        }

        public void Dragging(bool status)
        {
            isDragging = status;
            if (!status)
                interactableCharacter.CountForIdle();
        }

        private void InstantiateFood()
        {
            if (IsDragableOnScene())
            {
                Destroy(currentDragable);
            }

            int oldIndex = currentDragableIndex;
            currentDragableIndex = dragableIndexes[Random.Range(0, dragableIndexes.Count)];
            DragTouchable chosen = dragables[currentDragableIndex];
            dragableIndexes.Remove(currentDragableIndex);
            dragableIndexes.Add(oldIndex);
            interactableCharacter.PlayAnimation(surpriseAnimation, currentDragableIndex == SockIndex ? smellBadAudio : surpriseAudio, 0, SurpriseId);
            currentDragable = ZenjectUtilities.Instantiate<DragTouchable>(chosen, chosen.transform.position, chosen.transform.rotation, null).gameObject;
        }

        private void InstantiateTissue()
        {
            interactableCharacter.PlayAnimation(sneezeAnimation, sneezeAudio, 0, SneezeId);
            StartCoroutine(AppearTissue());
        }

        private IEnumerator AppearTissue()
        {
            yield return new WaitForSeconds(sneezeAnimation.length);
            ZenjectUtilities.Instantiate<DragInteractable>(interactable, interactable.transform.position, interactable.transform.rotation, null);
        }

        #endregion
    }
}
