﻿using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Emotion.Badges
{
    public class BadgeUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private BadgeCreator badgeCreator;

        [SerializeField] private Image badgeImage;
        [SerializeField] private Image blocked;
        [SerializeField] private Button openButton;

        private Badge badge;

        #endregion

        #region BEHAVIORS   

        private void Awake()
        {
            openButton.onClick.AddListener(OpenBadge);
        }

        public void Initialize(Badge newBadge)
        {
            badge = newBadge;
            badgeImage.sprite = badge.Sprite;
            blocked.gameObject.SetActive(!badge.Acquired);
        }

        private void OpenBadge()
        {
            badgeCreator.InspectBadge(badge.Sprite, badge.Description);
        }

        #endregion
    }
}