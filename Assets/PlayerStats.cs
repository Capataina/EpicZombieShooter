using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float health;
    public float maxStamina = 50;
    [HideInInspector] public float stamina;
    public float walkSpeed = 4.0f;
    public float sprintSpeed = 8.0f;
    [HideInInspector] public float speed;
    private PlayerController playerController;
    [SerializeField] float staminaGain;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        health = maxHealth;
        stamina = maxStamina;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    public void Heal(float hp)
    {
        health += hp;
    }

    public void ReduceStamina(float sp)
    {
        stamina -= sp;
    }

    public void AddStamina(float sp)
    {
        stamina += sp;
    }

    private void Update()
    {
        // Add stamina if not sprinting
        if (!playerController.isSprinting && stamina < maxStamina) AddStamina(staminaGain);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
