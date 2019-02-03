using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUIController {
    FieldItem[,] elements;
    LevelGenerator levelGenerator;
    public FieldUIController(LevelGenerator levelGenerator, FieldProcessor fieldProcessor) {
        fieldProcessor.OnItemSwap += SwapItems;
        fieldProcessor.OnItemRemove += RemoveItem;
        fieldProcessor.OnNewSpawn += SpawnNew;
        this.levelGenerator = levelGenerator;
        this.elements = levelGenerator.GenerateField();
        UpdateElementsPositions();
    }

    private void SpawnNew(int coloumn, int row)
    {
        if (elements[coloumn, row] != null)
            GameObject.Destroy(elements[coloumn, row].gameObject);
        elements[coloumn, row] = levelGenerator.GenerateItem(coloumn, row);
        SetRectParams(elements[coloumn, row], elements.GetLength(0), elements.GetLength(1));
        ((RectTransform)elements[coloumn, row].transform).anchoredPosition += Vector2.up * 100;
        ((IMoveable)elements[coloumn, row]).Move(Vector2.zero);
    }

    private void RemoveItem(int coloumn, int row)
    {
        ((IRemovable)elements[coloumn, row]).Remove();
    }
    

    private void UpdateElementsPositions()
    {
        int width = elements.GetLength(0);
        int height = elements.GetLength(1);
        for (int coloumn = 0; coloumn < elements.GetLength(0); coloumn++)
            for (int row = 0; row < elements.GetLength(1); row++)
            {
                SetRectParams(elements[coloumn, row], width, height);
            }
    }

    ~FieldUIController() {
    }

    private void SwapItems(int coloum1, int row1, int coloum2, int row2) {
        // levelController.SwapItems(first, second);
        elements[coloum1, row1].SetIndexes(coloum2, row2);
        elements[coloum2, row2].SetIndexes(coloum1, row1);
        var obj = elements[coloum1, row1];
        elements[coloum1, row1] = elements[coloum2, row2];
        elements[coloum2, row2] = obj;
        SetRectParamsAndMove(elements[coloum1, row1], elements.GetLength(0), elements.GetLength(1), null);
        SetRectParamsAndMove(elements[coloum2, row2], elements.GetLength(0), elements.GetLength(1), null);
    }

    private void SetRectParams(FieldItem fieldItem, int width, int height )
    {
        RectTransform rect = fieldItem.transform as RectTransform;
        SetAnchors(rect, fieldItem, width, height);
        rect.sizeDelta = rect.anchoredPosition = Vector2.zero;
        rect.localScale = Vector3.one;
    }

    private void SetAnchors(RectTransform rect, FieldItem fieldItem, int width, int height)
    {
        rect.anchorMin = new Vector2((float)fieldItem.Coloumn / width, 1 - (float)(fieldItem.Row + 1) / height);
        rect.anchorMax = new Vector2((float)(fieldItem.Coloumn + 1) / width, 1 - (float)(fieldItem.Row) / height);
    }

    private void SetRectParamsAndMove(FieldItem fieldItem, int width, int height, Action onMoveFinished)
    {
        if (fieldItem == null)
            return;
        RectTransform rect = fieldItem.transform as RectTransform;
        Vector3 oldPos = rect.transform.localPosition;
        SetAnchors(rect, fieldItem, width, height);
        rect.transform.localPosition = oldPos;
        ((IMoveable)fieldItem).Move(Vector2.zero);
    }
}
