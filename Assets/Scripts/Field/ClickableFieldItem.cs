using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableFieldItem : FieldItem,    IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler  {
    public static Action<FieldItem> OnFieldItemClick = delegate { };
    public static Action<FieldItem, Vector2> OnFieldItemDrag = delegate { };
    protected Vector2 drag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        drag = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        drag += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnFieldItemDrag.Invoke(this, drag);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnFieldItemClick.Invoke(this);
    }
}
