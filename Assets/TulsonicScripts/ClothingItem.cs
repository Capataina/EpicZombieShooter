using System;
using UnityEngine;

public class ClothingItem : ItemBase
{
    float armorValue;
    float itemCapacity;

    public override ContextMenuButton[] ContextMenuRecipe(ItemGrid grid, EquipmentSlot slot, InventoryItem item)
    {
        return null;
    }
}
