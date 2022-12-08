using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

public class ItemData : MonoBehaviour
{
    [SerializeReference] public ItemBase itemScript;
    [HideInInspector] public Guid runTimeDataID;

    public T GetRuntimeData<T>() where T : RuntimeData
    {
        return RuntimeDataManager.Instance.GetRuntimeData(runTimeDataID) as T;
    }
}

