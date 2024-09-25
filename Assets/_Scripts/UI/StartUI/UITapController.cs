using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITapController : MonoBehaviour , IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action OnTap;
    public void OnPointerClick(PointerEventData eventData)
    {
       // OnTap?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTap?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       // OnTap?.Invoke();
    }
}
