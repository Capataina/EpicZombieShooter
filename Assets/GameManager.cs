using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RuntimeDataManager runtimeDataManager;

    void Awake()
    {
        runtimeDataManager = RuntimeDataManager.Instance;

        //TODO might change later
        runtimeDataManager.ClearRuntimeData();
    }
}
