using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItems : ItemBase
{
    [SerializeField] protected PlayerData.EquipmentSlots inventorySlot;
}
