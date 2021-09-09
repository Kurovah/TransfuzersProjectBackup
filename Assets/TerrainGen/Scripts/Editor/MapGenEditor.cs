using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(MapGen))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGen mapGen = (MapGen)target;
        //DrawDefaultInspector();
        if (DrawDefaultInspector()) //if anything changes
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenMap();
            }
        }

        if (GUILayout.Button("Gen")) 
        {
            mapGen.GenMap();
        }
    }
}
