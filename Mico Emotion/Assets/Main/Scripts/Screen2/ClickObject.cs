using UnityEngine;
using UnityEngine.EventSystems;

namespace Emotion.Screen2
{
    public class ClickObject : MonoBehaviour, IPointerUpHandler
    {
        #region FIELDS

        private const int SingleClickAmount = 1;

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

            if (clicked == 1 && clickTime > clickDelay)
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

        private void DoDoubleClick()
        {
            Debug.Log("double");
        }

        private void DoSingleClick()
        {
            Debug.Log("single");
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
