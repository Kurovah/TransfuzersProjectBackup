using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateData : ScriptableObject
{
    public event System.Action OnvaluesUpdate;
    public bool autoUpdate;

    protected virtual void OnValidate()
    {
        if (autoUpdate) 
        {
            //IsUpdated();
            UnityEditor.EditorApplication.update += IsUpdated;
        }
    }
    public void IsUpdated() 
    {
        UnityEditor.EditorApplication.update -= IsUpdated;
        if (OnvaluesUpdate != null) 
        {
            OnvaluesUpdate();
        }
    }
}
