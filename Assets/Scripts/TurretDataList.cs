using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe List", menuName = "New recipe list")]
public class TurretDataList : ScriptableObject
{
    public List<TurretData> turretList = new List<TurretData>();
}
