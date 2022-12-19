using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContextMenuScript : ContextMenu
{
    public void RemoveMagazine()
    {
        ProjectileWeaponItemsRuntimeData weaponRD =
        item.itemData.GetRuntimeData<ProjectileWeaponItemsRuntimeData>();

        if (grid != null)
        {
            if (grid.QuickAddToInventory(weaponRD.insertedMagazine))
            {
                Destroy(weaponRD.insertedMagazine.gameObject);
                weaponRD.insertedMagazine = null;
            }
        }
        else if (slot != null)
        {
            if (PlayerData.Instance.inventoryGrid.QuickAddToInventory(weaponRD.insertedMagazine))
            {
                Destroy(weaponRD.insertedMagazine.gameObject);
                weaponRD.insertedMagazine = null;
            }
        }
    }
}
