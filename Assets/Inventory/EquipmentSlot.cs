using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class EquipmentSlot : MonoBehaviour
{
    [HideInInspector] public InventoryItem equippedItem;

    PlayerData playerData;

    private void OnEnable()
    {
        playerData = PlayerData.Instance;
    }

    public bool CheckIfEmpty()
    {
        return equippedItem == null;
    }

    public void PlaceItem(InventoryItem item)
    {
        item.PrepareForEquipmentSlot(this);
        equippedItem = item;

        playerData.EquipItem(item.itemData);
    }

    public InventoryItem TakeItem()
    {
        InventoryItem t = equippedItem;
        equippedItem = null;
        playerData.UnequipItem();
        return t;
    }
}
