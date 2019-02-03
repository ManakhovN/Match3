using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Match3/LevelInfo")]
public class LevelInfo : ScriptableObject {
    [SerializeField]
    private FieldInfo fieldInfo;
    [SerializeField]
    private ItemSet itemSet;

    public FieldInfo FieldInfo
    {
        get
        {
            return fieldInfo;
        }

        set
        {
            fieldInfo = value;
        }
    }

    public ItemSet ItemSet
    {
        get
        {
            return itemSet;
        }

        set
        {
            itemSet = value;
        }
    }

    public void Init() {
        FieldInfo.Init(ItemSet.Items.Length);
    }
}
