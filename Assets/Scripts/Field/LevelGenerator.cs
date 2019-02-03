using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator
{
    LevelInfo levelInfo;
    RectTransform fieldRectTransform;
    public LevelGenerator(LevelInfo levelInfo, RectTransform fieldRectTransform) {
        this.levelInfo = levelInfo;
        this.fieldRectTransform = fieldRectTransform;
        levelInfo.Init();
    }

    public FieldItem[,] GenerateField()
    {
        FieldInfo fieldInfo = levelInfo.FieldInfo;
        ItemSet itemSet = levelInfo.ItemSet;
        FieldItem[,] result = new FieldItem[fieldInfo.Width, fieldInfo.Height];
        for (int coloumn = 0; coloumn < fieldInfo.Width; coloumn++)
            for (int row = 0; row < fieldInfo.Height; row++)
            {
                 result[coloumn,row] = GenerateItem(coloumn, row, fieldInfo, itemSet);
            }
        return result;
    }

    internal FieldItem GenerateItem(int coloumn, int row)
    {
        return GenerateItem(coloumn, row, levelInfo.FieldInfo, levelInfo.ItemSet);
    }

    public FieldItem GenerateItem(int coloumn, int row, FieldInfo field, ItemSet itemSet)
    {
        GameObject item;
        var prefabs = itemSet.Items;
        int value = field.Field[coloumn, row];
        item = GameObject.Instantiate(prefabs[value], fieldRectTransform) as GameObject;
        FieldItem result = item.transform.GetComponent<FieldItem>();
        result.SetIndexes(coloumn, row);
        return result;
    }

    public Vector2 GetFieldRectSize() {
        return fieldRectTransform.rect.size;
    }
}
