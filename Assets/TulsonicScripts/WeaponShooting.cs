using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{

    ItemData weaponData;
    ProjectileWeaponItems weaponScript;
    float cooldownTimer;
    bool isAutomatic;
    [HideInInspector] public bool triggerHeld;
    PlayerData playerData;
    ProjectileWeaponItemsRuntimeData runtimeData;

    float mfTimer;
    [SerializeField] float mfLifeTime;
    GameObject muzzleFlash;

    EquippedItemAnimator equippedItemAnimator;

    private void Awake()
    {
        playerData = PlayerData.Instance;
    }

    private void Start()
    {
        equippedItemAnimator = GetComponent<EquippedItemAnimator>();
    }

    public void Initialize(ItemData weaponData)
    {
        this.weaponData = weaponData;
        weaponScript = weaponData.itemScript as ProjectileWeaponItems;
        runtimeData = weaponData.GetRuntimeData<ProjectileWeaponItemsRuntimeData>();
        cooldownTimer = weaponScript.cooldown;
    }

    private void Update()
    {
        if (playerData.equippedItem == null || playerData.equippedItem.itemScript is not ProjectileWeaponItems) return;

        weaponScript = playerData.equippedItem.itemScript as ProjectileWeaponItems;

        //Timing the cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0 && Input.GetMouseButton(1))
        {
            if (runtimeData.insertedMagazine != null
            && runtimeData.insertedMagazine.
            GetRuntimeData<MagazineAttachmentRuntimeData>().bulletCount > 0)
            {
                if (weaponScript.isAutomatic)
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
            else
            {
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
        equippedItemAnimator.SwitchShootingArmAnimation();

        RaycastHit hit;

        DisplayMuzzleFlash();

        weaponData.GetRuntimeData<ProjectileWeaponItemsRuntimeData>().
        insertedMagazine.GetRuntimeData<MagazineAttachmentRuntimeData>().
        bulletCount -= 1;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, LayerMask.GetMask("Default")))
        {
            Zombie zombie = hit.transform.gameObject.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(weaponScript.damage);
            }
        }

        cooldownTimer = weaponScript.cooldown;
    }

    private void DisplayMuzzleFlash()
    {
        if (muzzleFlash == null)
        {
            Transform spawnerTransform = playerData.equipmentModel.GetComponent<WeaponModel>().muzzleFlashSpawnMarker.transform;
            muzzleFlash = GameObject.Instantiate(weaponScript.muzzleFlash, spawnerTransform.position, spawnerTransform.rotation);
            muzzleFlash.transform.parent = spawnerTransform;
        }
        StartMuzzleTimer();
    }

    private void StartMuzzleTimer()
    {
        mfTimer = mfLifeTime;
    }
}
