using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldItem : MonoBehaviour {
    public int Row { set; get; }
    public int Coloumn { set; get; }
    public virtual void Pick() {
        transform.localScale = Vector3.one * 1.1f;
    }
    public virtual void Depick() {
        transform.localScale = Vector3.one;
    }

    public void SetIndexes(int coloumn, int row)
    {
        this.Row = row;
        this.Coloumn = coloumn;
    }
}
