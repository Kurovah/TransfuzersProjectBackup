using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LightingOption", menuName = "New Objects/ Lighting", order = 1)]
public class LightingCond : ScriptableObject
{
    
    public Gradient ambiColor, dirColor, fogColor;
    public float fogDensity;

}
