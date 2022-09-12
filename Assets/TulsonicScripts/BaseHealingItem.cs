using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealingItem : ConsumableItem
{
    [SerializeField] float healthEffect;
    [SerializeField] float staminaEffect;

    public override void ConsumeItem()
    {
        playerData.AddStamina(staminaEffect);
        playerData.Heal(healthEffect);
    }
}
