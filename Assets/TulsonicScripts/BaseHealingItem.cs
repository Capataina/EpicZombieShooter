using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Healing Item", menuName = "Items/Consumable/Base Healing Item")]
public class BaseHealingItem : ConsumableItem
{
    [SerializeField] float healthEffect;
    [SerializeField] float staminaEffect;

    public override void ConsumeItem()
    {
        playerData.AddStamina(staminaEffect);
        playerData.Heal(healthEffect);
        Debug.Log("used " + itemName);
    }
}
