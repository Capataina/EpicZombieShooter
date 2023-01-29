using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponItemsRuntimeDataInitializer : HasRuntimeData
{
    public ProjectileWeaponItemsRuntimeData runtimeData;

    protected override void AddDataToManager()
    {
        runtimeDataManager.AddToDictionary(guid, runtimeData);
    }

}