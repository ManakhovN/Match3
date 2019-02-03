using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSet", menuName = "Match3/ItemSet")]
public class ItemSet : ScriptableObject {
    [SerializeField]
    GameObject[] items;
    public GameObject[] Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }

    public int ItemsCount { get { return items.Length;  } }
}
