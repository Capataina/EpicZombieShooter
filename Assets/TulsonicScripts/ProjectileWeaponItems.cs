using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Weapon", menuName = "Items/Projectile Weapon")]
public class ProjectileWeaponItems : EquipableItems
{
    public float damage;
    public float cooldown;
    public bool isAutomatic;
    public GameObject muzzleFlash;

    public override ContextMenuButton[] ContextMenuRecipe(ItemGrid grid, EquipmentSlot slot, InventoryItem item)
    {
        return new ContextMenuButton[] {
                new ContextMenuButton("Remove Magazine", () => RemoveMagainzeButton(item, grid, slot)),
                new ContextMenuButton("Say hey", () => Debug.Log("hey"))
            };
    }

    private void RemoveMagainzeButton(InventoryItem item, ItemGrid grid, EquipmentSlot slot)
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

[System.Serializable]
public class ProjectileWeaponItemsRuntimeData : RuntimeData
{
    public ItemData insertedMagazine;
}