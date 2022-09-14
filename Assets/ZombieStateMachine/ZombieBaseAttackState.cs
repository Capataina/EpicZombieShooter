using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Attack")]
public class ZombieBaseAttackState : ZombieCurrentState
{
    [SerializeField]
    private float baseZombieDamage = 10;
    private float attackCooldown = 0.5f;
    private float attackCooldownTimer = 1;
    private float playerToZombieDistance;

    private bool InMeleeRange(ZombieStateMachineController zombie)
    {
        playerToZombieDistance = Vector3.Distance(
            zombie.thePlayer.transform.position,
            zombie.transform.position
        );

        if (playerToZombieDistance <= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("Entered Attack State!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        if (InMeleeRange(zombie))
        {
            attackCooldown += Time.deltaTime;
            if (attackCooldown >= attackCooldownTimer)
            {
                zombie.playerData.TakeDamage(baseZombieDamage);
                Debug.Log("Attacked Player!");
                attackCooldown = 0;
            }
        }
        else
        {
            zombie.SwitchState(zombie.alertState);
        }
    }
}
