using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContextMenuScript : ContextMenu
{
    public void RemoveMagazine()
    {
        ProjectileWeaponItemsRuntimeData weaponRD =
        item.itemData.GetRuntimeData<ProjectileWeaponItemsRuntimeData>();

        if (grid.QuickAddToInventory(weaponRD.insertedMagazine))
        {
            Destroy(weaponRD.insertedMagazine.gameObject);
            weaponRD.insertedMagazine = null;
        }
    }
}
