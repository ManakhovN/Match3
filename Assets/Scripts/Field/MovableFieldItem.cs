using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableFieldItem : ClickableFieldItem, IMoveable, IRemovable
{
    [SerializeField]
    float moveSpeed = 1f;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    [SerializeField]
    float scaleSpeed = 1000f;

    public void Move(Vector2 destination)
    {
        AnimationsController.Instance.MoveAnchoredPositionTo(transform as RectTransform, destination, moveSpeed);
    }

    public void Remove()
    {
        AnimationsController.Instance.ScaleTo(transform, Vector3.zero, scaleSpeed);
    }

}
