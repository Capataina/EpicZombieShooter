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
}
