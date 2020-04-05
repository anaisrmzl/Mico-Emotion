using UnityEngine;
using UnityEngine.EventSystems;

using Utilities.Sound;
using Zenject;

namespace Emotion.Recognize
{
    public class ClickObject : MonoBehaviour, IPointerUpHandler
    {
        #region FIELDS

        private const int SingleClickAmount = 1;

        [Inject] protected InteractableCharacter interactableCharacter;
        [Inject] private SoundManager soundManager;

        [SerializeField] private AnimationClip clickAnimation = null;
        [SerializeField] protected AnimationClip doubleClickAnimation = null;
        [SerializeField] protected AudioClip doubleClickAudio = null;
        [SerializeField] protected AudioClip clickAudio = null;
        [SerializeField] private int clickValue;
        [SerializeField] private int doubleClickValue;

        private float clicked = 0;
        private float clickTime = 0;
        private float clickDelay = 0.5f;
        private bool count = false;
        private Collider2D objectCollider;

        #endregion

        #region BEHAVIORS   

        private void Awake()
        {
            objectCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (count)
                clickTime += Time.deltaTime;

            if (clicked == SingleClickAmount && clickTime > clickDelay)
            {
                ResetClick();
                DoSingleClick();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (clicked == 0)
            {
                clicked++;
                count = true;
                clickTime = 0.0f;
            }
            else if (clicked == SingleClickAmount)
            {
                if (clickTime <= clickDelay)
                {
                    ResetClick();
                    DoDoubleClick();
                }
            }
        }

        public virtual void DoDoubleClick()
        {
            if (doubleClickAnimation == null)
                return;

            interactableCharacter.PlayAnimation(doubleClickAnimation, doubleClickAudio, doubleClickValue, transform.name);
        }

        private void DoSingleClick()
        {
            if (clickAnimation == null)
                return;

            interactableCharacter.PlayAnimation(clickAnimation, clickAudio, clickValue, transform.name);
        }

        private void ResetClick()
        {
            clicked = 0;
            count = false;
            clickTime = 0.0f;
        }

        #endregion
    }
}
