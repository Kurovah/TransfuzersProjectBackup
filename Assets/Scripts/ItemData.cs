using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newList", menuName = "Custom/ItemDataContainer")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    public List<Item> items = new List<Item>();

    public List<string> returnAllContents()
    {
        List<string> list = new List<string>();
        foreach (var item in items)
        {
            list.Add(item.name);
        }
        return list;
    }
}
