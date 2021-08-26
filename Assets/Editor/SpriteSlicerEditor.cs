using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteSlicer))]
public class SpriteSlicerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Slice"))
        {
            SpriteSlicer t = (SpriteSlicer)target;
            t.NineSliceSprite();
        }
    }
}
