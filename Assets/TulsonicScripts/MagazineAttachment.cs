using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Attachements/Magazine")]
public class MagazineAttachment : WeaponAttachmentItem
{
    public int bulletCapacity;
    public string weaponName;

    public override ContextMenuButton[] ContextMenuRecipe(ItemGrid grid, EquipmentSlot slot, InventoryItem item)
    {
        return null;
    }
}


[System.Serializable]
public class MagazineAttachmentRuntimeData : RuntimeData
{
    [SerializeField] public int bulletCount;
}
