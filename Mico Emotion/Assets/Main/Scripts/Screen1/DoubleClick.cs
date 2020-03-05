using UnityEngine;
using UnityEngine.Events;

public class DoubleClick : MonoBehaviour
{
    #region FIELDS

    private float clicked = 0;
    private float clickTime = 0;
    private float clickDelay = 0.5f;

    #endregion

    #region  EVENTS

    public event UnityAction doubleClicked;

    #endregion

    #region BEHAVIORS

    private void Update()
    {
        if (DetectDoubleClick())
            doubleClicked?.Invoke();
    }

    private bool DetectDoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1)
                clickTime = Time.time;
        }

        if (clicked > 1 && Time.time - clickTime < clickDelay)
        {
            clicked = 0;
            clickTime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clickTime > 1)
        {
            clicked = 0;
        }

        return false;
    }

    #endregion
}
