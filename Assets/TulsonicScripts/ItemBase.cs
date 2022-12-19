using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [SerializeField] public float itemId;
    [SerializeField] public string itemName;
    [SerializeField] public GameObject itemObject;
    [SerializeField] public GameObject itemModel;
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public int inventoryWidth;
    [SerializeField] public int inventoryHeight;
    protected static PlayerData playerData;

    private void OnEnable()
    {
        playerData = PlayerData.Instance;
    }

    public abstract ContextMenuButton[] ContextMenuRecipe(
        ItemGrid grid,
        EquipmentSlot slot,
        InventoryItem item);

    public GameObject CreateContextMenu(ItemGrid grid, EquipmentSlot slot, InventoryItem item)
    {
        ContextMenuButton[] recipe = ContextMenuRecipe(grid, slot, item);
        if (recipe != null)
        {
            return ContextMenuConstructor.Instance.ConstructContextMenu(
                recipe
            );
        }
        else
        {
            return null;
        }
    }

}