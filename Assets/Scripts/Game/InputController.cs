using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    [SerializeField]
    protected LevelController levelController;
    protected FieldProcessor fieldProcessor;
    public void Awake()
    {
        fieldProcessor = levelController.FieldProcessor;
    }

    protected virtual void Swap(int coloumn1, int row1, int coloumn2, int row2) {
        fieldProcessor.SwapItemsWithChecking(coloumn1, row1, coloumn2, row2);
    }   
}
