﻿using System.Collections.Generic;
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

        [SerializeField] private Spider spider;
        [SerializeField] private AnimationClip burpAnimation;
        [SerializeField] private AnimationClip sneezeAnimation;
        [SerializeField] private DragTouchable[] dragables;
        [SerializeField] private DragInteractable interactable;

        [Inject] private InteractableCharacter interactableCharacter;

        private bool isDragging = false;
        private GameObject currentDragable;
        private List<int> dragableIndexes = new List<int>();
        private int currentIndex;

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            currentIndex = dragables.Length - 1;
            for (int i = 0; i < currentIndex; i++)
                dragableIndexes.Add(i);

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

            int oldIndex = currentIndex;
            currentIndex = dragableIndexes[Random.Range(0, dragableIndexes.Count)];
            DragTouchable chosen = dragables[currentIndex];
            dragableIndexes.Remove(currentIndex);
            dragableIndexes.Add(oldIndex);
            currentDragable = ZenjectUtilities.Instantiate<DragTouchable>(chosen, chosen.transform.position, chosen.transform.rotation, null).gameObject;
        }

        private void InstantiateTissue()
        {
            interactableCharacter.PlayAnimation(sneezeAnimation, 0, SneezeId);
            ZenjectUtilities.Instantiate<DragInteractable>(interactable, interactable.transform.position, interactable.transform.rotation, null);
        }

        #endregion
    }
}