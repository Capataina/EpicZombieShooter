using UnityEngine;
using System;


public class MagazineAttachmentRuntimeDataInitializer : HasRuntimeData
{
    public MagazineAttachmentRuntimeData runtimeData;

    protected override void AddDataToManager()
    {
        runtimeDataManager.AddToDictionary(guid, runtimeData);
    }
}
