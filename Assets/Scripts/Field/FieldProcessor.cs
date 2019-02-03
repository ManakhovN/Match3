using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldProcessor {
    int width;
    int height;
    int[,] field;
    int itemsCount;
    public Action<int, int, int, int> OnItemSwap = delegate { };
    public Action<int, int> OnItemRemove = delegate { };
    public Action<int, int> OnNewSpawn = delegate { };

    public FieldProcessor(FieldInfo fieldInfo, int itemsCount)
    {
        this.width = fieldInfo.Width;
        this.height = fieldInfo.Height;
        this.field = fieldInfo.Field;
        this.itemsCount = itemsCount;
    }

    public void SwapItems(int coloumn1, int row1, int coloumn2, int row2)
    {   
        SwapFields(coloumn1, row1, coloumn2, row2);
        OnItemSwap.Invoke(coloumn1, row1, coloumn2, row2);
    }

    public void SwapItemsWithChecking(int coloumn1, int row1, int coloumn2, int row2)
    {
        if (field[coloumn1, row1] == field[coloumn2, row2] || !PointInsideField(coloumn1, row1) || !PointInsideField(coloumn2, row2))
            return;
        SwapFields(coloumn1, row1, coloumn2, row2);
        if (!IsMatchingAround(coloumn1, row1) && !IsMatchingAround(coloumn2, row2))
        {
            SwapFields(coloumn1, row1, coloumn2, row2);
            return;
        }
        OnItemSwap.Invoke(coloumn1, row1, coloumn2, row2);
    }

    private bool PointInsideField(int coloumn, int row)
    {
        return (coloumn >= 0 && coloumn < width && row >= 0 && row < height);
    }

    public void SwapFields(int coloumn1, int row1, int coloumn2, int row2) {
        int t = field[coloumn1, row1];
        field[coloumn1, row1] = field[coloumn2, row2];
        field[coloumn2, row2] = t;
    }

    public bool IsMatchingAround(int coloumn, int row)
    {
        int matches = 0;
        for (int i = -2; i < 2; i++)
            if (coloumn+i>=0 && coloumn + i + 1>=0 && coloumn +i<width && coloumn+i+1<width && field[coloumn + i, row] == field[coloumn + i + 1, row])
            {
                matches++;
                if (matches == 2)
                    return true;
            }
            else matches = 0;
        matches = 0;
        for (int i = -2; i < 2; i++)
            if (row + i>=0 && row + i + 1>=0 && row + i < height && row + i + 1 < height && field[coloumn, row +i] == field[coloumn, row+i+1])
            {
                matches++;
                if (matches == 2)
                    return true;
            }
            else matches = 0;
        return false;
    }

    public void RemoveItem(int coloumn, int row)
    {
        OnItemRemove.Invoke(coloumn, row);
        field[coloumn, row] = -1;
    }

    public void CheckFields()
    {

        bool[,] toRemove = new bool[width, height];
        for (int coloumn = 0; coloumn < width; coloumn++)
            for (int row = 0; row < height; row++)
            {
                if (field[coloumn, row] == -1)
                    continue;
                if (coloumn < width - 2 && field[coloumn, row] == field[coloumn + 1, row] && field[coloumn, row] == field[coloumn + 2, row])
                    toRemove[coloumn, row] = toRemove[coloumn + 1, row] = toRemove[coloumn + 2, row] = true;
                if (row < height - 2 && field[coloumn, row] == field[coloumn, row + 1] && field[coloumn, row] == field[coloumn, row + 2])
                    toRemove[coloumn, row] = toRemove[coloumn, row + 1] = toRemove[coloumn, row + 2] = true;
            }
        for (int coloumn = 0; coloumn < width; coloumn++)
            for (int row = 0; row < height; row++)
            {
                if (toRemove[coloumn, row])
                    RemoveItem(coloumn, row);
            }
    }

    public void CheckForFreeSpaces()
    {
        for (int coloumn = 0; coloumn < width; coloumn++)
            for (int row = height-2; row >= 0; row--)
            {
                if (field[coloumn, row] == -1)
                    continue;
                int offset = 0;
                for (int i = 1; row + i < height; i++)
                    if (field[coloumn, row + i] == -1)
                        offset = i;
                    else
                        break;
                if (offset!=0)
                SwapItems(coloumn, row, coloumn, row + offset);
            }
        CheckFirstRow();
    }

    public void CheckFirstRow()
    {
        for (int coloumn = 0; coloumn < width; coloumn++)
        {
            for (int row = 0; row < height; row++)
            {
                if (field[coloumn, row] == -1)
                {
                    SpawnNew(coloumn, 0);
                }
                else
                    break;
            }
        }
    }

    private void SpawnNew(int coloumn, int row)
    {        
        field[coloumn, row] = UnityEngine.Random.Range(0, itemsCount);
        OnNewSpawn.Invoke(coloumn, row);
    }
}
