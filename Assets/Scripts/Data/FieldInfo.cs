using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FieldInfo", menuName = "Match3/FieldInfo")]
public class FieldInfo : ScriptableObject
{
    [SerializeField]
    private int[,] field;
    public int[,] Field
    {
        get
        {
            return field;
        }

        set
        {
            this.field = value;
        }
    }

    [SerializeField]
    private int width = 10;
    [SerializeField]
    private int height = 10;

    public int Height { get { return height; } }

    public int Width { get { return width; } }

    public void Init(int itemsCount)
    {
        field = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                field[i, j] = GetPossibleValue(i,j,itemsCount);
            }
        }
    }

    private int GetPossibleValue(int i, int j, int itemsCount)
    {
        List<int> possibleValues = new List<int>();
        for (int ii = 0; ii < itemsCount; ii++)
            possibleValues.Add(ii);

        if (i > 0)
            possibleValues.Remove(field[i - 1, j ]);
        if (j > 0)
            possibleValues.Remove(field[i , j - 1]);
        return possibleValues[UnityEngine.Random.Range(0, possibleValues.Count)];
    }
}

