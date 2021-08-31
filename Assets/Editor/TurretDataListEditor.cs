using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretDataList))]
public class TurretDataListEditor : Editor
{
    TurretDataList rlist;
    public override void OnInspectorGUI()
    {
        ItemData ml = (ItemData)Resources.Load("ItemDataContainer");
        rlist = (TurretDataList)target;
        //display all the turret data individually
        foreach (var item in rlist.turretList)
        {
            GUILayout.BeginHorizontal();
            item.displayed = EditorGUILayout.Foldout(item.displayed, item.turretName);
            if (GUILayout.Button("remove turret data"))
            {
                rlist.turretList.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
            if (item.displayed)
            {
                //display name spriet and prefab fields
                item.turretName = EditorGUILayout.TextField("name", item.turretName);
                item.turretIcon = (Sprite)EditorGUILayout.ObjectField("Sprite",item.turretIcon, typeof(Sprite), false);
                item.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", item.prefab, typeof(GameObject), false);
                //if folded out display the components needed for the turret to be crafted

                foreach (var component in item.components)
                {
                    GUILayout.BeginHorizontal();
                    component.itemIndex = EditorGUILayout.Popup(component.itemIndex, ml.returnAllContents().ToArray());
                    component.itemAmount = EditorGUILayout.IntField(component.itemAmount);
                    if (GUILayout.Button("X"))
                    {
                        item.components.Remove(component);
                        break;
                    }
                    GUILayout.EndHorizontal();
                   
                }
                if (GUILayout.Button("add component"))
                {
                    item.components.Add(new RecipeComponent());
                }
                
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("add new turret data"))
        {
            rlist.turretList.Add(new TurretData());
        }
        if (GUILayout.Button("save"))
        {
            EditorUtility.SetDirty(target);
        }

    }
}
