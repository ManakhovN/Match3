using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController {
    void OnEnable()
    {
        ClickableFieldItem.OnFieldItemClick += ItemClick;
        ClickableFieldItem.OnFieldItemDrag += ItemDrag;
    }

    private void OnDisable()
    {
        ClickableFieldItem.OnFieldItemClick -= ItemClick;
        ClickableFieldItem.OnFieldItemDrag -= ItemDrag;
    }

    FieldItem lastPicked;
    private void ItemClick(FieldItem obj)
    {
        if (obj is IMoveable)
        {
            if (lastPicked == null)
            {
                lastPicked = obj;
                lastPicked.Pick();
            }
            else
            {
                int rowDelta = Mathf.Abs(lastPicked.Row - obj.Row);
                int coloumnDelta = Mathf.Abs(lastPicked.Coloumn - obj.Coloumn);
                lastPicked.Depick();
                if ((rowDelta == 1 && coloumnDelta == 0) || (rowDelta == 0 && coloumnDelta == 1))
                {
                    base.Swap(lastPicked.Coloumn, lastPicked.Row, obj.Coloumn, obj.Row);
                    lastPicked = null;
                }
                else
                {
                    lastPicked = obj;
                    lastPicked.Pick();
                }
            }
        }
    }

    private void ItemDrag(FieldItem obj, Vector2 drag)
    {
        if (obj is IMoveable) {
            if (Mathf.Abs(drag.x) > Mathf.Abs(drag.y))
            {
                int coloumnDelta = (drag.x) >= 0 ? 1 : -1;
                base.Swap(obj.Coloumn, obj.Row, obj.Coloumn + coloumnDelta, obj.Row);
            }
            else {
                int rowDelta = (drag.y) <= 0 ? 1 : -1;
                base.Swap(obj.Coloumn, obj.Row, obj.Coloumn, obj.Row + rowDelta);
            }
        }
    }
}
