using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeasponShooting : MonoBehaviour
{

    ProjectileWeaponItems weapon;
    float cooldownTimer;
    bool isAutomatic;
    [HideInInspector] public bool triggerHeld;
    PlayerData playerData;

    float mfTimer;
    [SerializeField] float mfLifeTime;
    GameObject muzzleFlash;

    private void Awake()
    {
        playerData = PlayerData.Instance;
    }

    public void Initialize(ItemBase item)
    {
        weapon = (ProjectileWeaponItems)item;
        cooldownTimer = weapon.cooldown;
    }

    private void Update()
    {
        if (playerData.equippedItem == null || playerData.equippedItem is not ProjectileWeaponItems) return;

        weapon = (ProjectileWeaponItems)playerData.equippedItem;

        //Timing the cooldown
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

        // Timing the muzzleFlash
        if (mfTimer > 0)
        {
            mfTimer -= Time.deltaTime;
        }

        if (mfTimer <= 0)
        {
            GameObject.Destroy(muzzleFlash);
        }

    }

    public void Shoot()
    {
        RaycastHit hit;

        DisplayMuzzleFlash();

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

    private void DisplayMuzzleFlash()
    {
        if (muzzleFlash == null)
        {
            Transform spawnerTransform = playerData.equipmentModel.GetComponent<WeaponModel>().muzzleFlashSpawnMarker.transform;
            muzzleFlash = GameObject.Instantiate(weapon.muzzleFlash, spawnerTransform.position, spawnerTransform.rotation);
            muzzleFlash.transform.parent = spawnerTransform;
        }
        StartMuzzleTimer();
    }

    private void StartMuzzleTimer()
    {
        mfTimer = mfLifeTime;
    }
}
