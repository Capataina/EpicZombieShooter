using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Weapon", menuName = "Items/Projectile Weapon")]
public class ProjectileWeaponItems : EquipableItems
{
    [SerializeField] float damage;
    [SerializeField] float cooldown;
    [SerializeField] bool isAutomatic;
}
