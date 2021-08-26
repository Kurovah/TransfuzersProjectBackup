using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe List", menuName = "New recipe list")]
public class RecipieList : ScriptableObject
{
    public List<Recipe> list = new List<Recipe>();
}
