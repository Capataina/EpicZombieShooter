using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeasponShooting : MonoBehaviour
{
    ProjectileWeaponItems weapon;
    float cooldownTimer;
    bool isAutomatic;
    [HideInInspector] public bool triggerHeld;

    public void Initialize(ItemBase item)
    {
        weapon = (ProjectileWeaponItems)item;
        cooldownTimer = weapon.cooldown;
    }

    private void Update()
    {
        if (weapon == null) return;

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0 && Input.GetMouseButton(1))
        {
            if (weapon.isAutomatic)
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
        }

    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, LayerMask.GetMask("Default")))
        {
            Zombie zombie = hit.transform.gameObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(weapon.damage);
            }
        }

        cooldownTimer = weapon.cooldown;
    }

}
