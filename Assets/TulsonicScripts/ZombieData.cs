using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie Data", menuName = "Enemies/Zombie")]
public class ZombieData : ScriptableObject
{
    [SerializeField] public float maxHealth;

}
