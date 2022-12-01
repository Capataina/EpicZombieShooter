using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] ZombieData zombieData;

    public float health;

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = Mathf.Clamp(value, 0, zombieData.maxHealth);
        }

    }

    private void Awake()
    {
        Health = zombieData.maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }
}
