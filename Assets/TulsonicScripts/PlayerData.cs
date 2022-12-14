using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : SingletonScriptableObject<PlayerData>
{
    public enum EquipmentSlots
    {
        PrimaryWeapons,
        SecondaryWeapon,
        Head,
        Body,
        Legs,
        Feet,
        BackPack
    }

    [HideInInspector] public ItemData equippedItem;

    public enum PlayerStates
    {
        Idle,
        Walking,
        Sprinting,
        Crouching
    }

    public PlayerStates states;

    [HideInInspector] public ItemGrid inventoryGrid;

    public float maxHealth = 50;
    private float health;
    public float maxStamina = 50;
    private float stamina;
    public float walkSpeed = 4.0f;
    public float sprintSpeed = 8.0f;
    public float aimSpeed = 2.0f;

    private GameObject activePlayerObject;

    [HideInInspector] public float speed;
    public float staminaGain;
    public float itemPickupRadius;

    [HideInInspector] public GameObject equipmentModel;

    [HideInInspector] public UnityEvent<ItemData> itemEquippedEvent;
    [HideInInspector] public UnityEvent<GameObject> playerAssignedEvent;
    [HideInInspector] public UnityEvent itemUnequipEvent;

    [HideInInspector] public bool isAiming;

    #region gettersetters

    public GameObject ActivePlayerObject
    {
        get { return activePlayerObject; }
        set
        {
            playerAssignedEvent.Invoke(value);
            activePlayerObject = value;
        }
    }
    public float Health
    {
        get { return health; }
        private set { health = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float Stamina
    {
        get { return stamina; }
        private set { stamina = Mathf.Clamp(value, 0, maxStamina); }
    }

    #endregion

    private void OnEnable()
    {
        Health = maxHealth;
        Stamina = maxStamina;
        itemEquippedEvent ??= new UnityEvent<ItemData>();
        playerAssignedEvent ??= new UnityEvent<GameObject>();
    }


    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }

    public void Heal(float hp)
    {
        Health += hp;
    }

    public void ReduceStamina(float sp)
    {
        Stamina -= sp;
    }

    public void AddStamina(float sp)
    {
        Stamina += sp;
    }

    public void PickupItem(ItemData item)
    {
        // TODO needs to be changed
        if (item.itemScript is not ProjectileWeaponItems)
        {
            //inventory.Add(item);
            Debug.Log("picked up " + item.itemScript.itemName);
        }
        else
        {
            EquipItem(item);
            Debug.Log("equipped " + item.itemScript.itemName);
        }
    }

    public void EquipItem(ItemData item)
    {
        equippedItem = item;

        itemEquippedEvent.Invoke(item);
    }

    public void UnequipItem()
    {
        equippedItem = null;
        itemUnequipEvent.Invoke();
    }
}
