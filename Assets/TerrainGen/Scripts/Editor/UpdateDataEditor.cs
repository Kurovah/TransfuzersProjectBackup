using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UpdateData), true)]
public class UpdateDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpdateData updateData = (UpdateData)target;

        if (GUILayout.Button("Gen"))
        {
            updateData.IsUpdated();
            EditorUtility.SetDirty(target);
        }
    }
}
