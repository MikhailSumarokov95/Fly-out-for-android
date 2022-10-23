using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool TouchDown { get; private set; }

    public bool TouchUp { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchDown = true;
        TouchUp = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchUp = true;
        TouchDown = false;
    }
}
