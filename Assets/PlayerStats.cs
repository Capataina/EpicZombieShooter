using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;

    void takeDamage(float dmg)
    {
        health -= dmg;
    }

    void heal(float hp)
    {
        health += hp;
    }
}
