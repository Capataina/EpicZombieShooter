using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

public abstract class HasRuntimeData : MonoBehaviour
{
    public Guid guid;

    protected RuntimeDataManager runtimeDataManager;

    protected abstract void AddDataToManager();

    private void OnEnable()
    {
        if (guid == Guid.Empty)
        {
            guid = Guid.NewGuid();
            runtimeDataManager = RuntimeDataManager.Instance;
            GetComponent<ItemData>().runTimeDataID = guid;
            AddDataToManager();
        }
    }
}
