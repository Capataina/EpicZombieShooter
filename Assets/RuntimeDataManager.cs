using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "manager")]
public class RuntimeDataManager : SingletonScriptableObject<RuntimeDataManager>
{
    private Dictionary<Guid, RuntimeData> runtimeDataDictionary = new Dictionary<Guid, RuntimeData>();

    public void AddToDictionary(Guid guid, RuntimeData runtimeData)
    {
        runtimeDataDictionary.Add(guid, runtimeData);
    }

    public RuntimeData GetRuntimeData(Guid guid)
    {
        return runtimeDataDictionary[guid];
    }

    public void DeleteRuntimeData(Guid guid)
    {
        runtimeDataDictionary.Remove(guid);
    }

    public void ClearRuntimeData()
    {
        runtimeDataDictionary.Clear();
    }
}

[System.Serializable]
public class RuntimeData { }

#if UNITY_EDITOR

[CustomEditor(typeof(RuntimeDataManager))]
public class RuntimeDataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Clear Runtime Data"))
        {
            (target as RuntimeDataManager).ClearRuntimeData();
        }
    }
}

#endif