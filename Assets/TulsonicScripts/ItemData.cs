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

    public RuntimeData GetRuntimeData()
    {
        return RuntimeDataManager.Instance.GetRuntimeData(runTimeDataID);
    }
}

