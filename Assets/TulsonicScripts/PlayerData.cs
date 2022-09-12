using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : SingletonScriptableObject<PlayerData>
{
    public float maxHealth = 50;
    private float health;
    public float maxStamina = 50;
    private float stamina;
    public float walkSpeed = 4.0f;
    public float sprintSpeed = 8.0f;
    [HideInInspector] public float speed;
    public float staminaGain;

    #region gettersetters
    public float Health
    {
        get
        {
            return health;
        }
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public float Stamina
    {
        get
        {
            return stamina;
        }
        private set
        {
            stamina = Mathf.Clamp(value, 0, maxStamina);
        }
    }

    #endregion

    private void Awake()
    {
        Health = maxHealth;
        Stamina = maxStamina;
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


}
