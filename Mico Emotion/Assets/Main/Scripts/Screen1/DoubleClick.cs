using UnityEngine;
using UnityEngine.Events;

public class DoubleClick : MonoBehaviour
{
    #region FIELDS

    private const int SingleClickAmount = 1;
    private const int DoubleClickAmount = 2;

    private float clicked = 0;
    private float clickTime = 0;
    private float clickDelay = 0.5f;

    #endregion

    #region EVENTS

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
            if (clicked == SingleClickAmount)
                clickTime = Time.time;
        }

        if (clicked > SingleClickAmount && Time.time - clickTime < clickDelay)
        {
            clicked = 0;
            clickTime = 0;
            return true;
        }
        else if (clicked > DoubleClickAmount || Time.time - clickTime > 1)
        {
            clicked = 0;
        }

        return false;
    }

    #endregion
}
