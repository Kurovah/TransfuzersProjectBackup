using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newList", menuName = "Custom/masterList")]
public class MasterItemList : ScriptableObject
{
    [SerializeField]
    public List<Item> items = new List<Item>();
}
