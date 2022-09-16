using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Alert")]
public class ZombieBaseAlertState : ZombieCurrentState
{
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

    private bool isAlerted(ZombieStateMachineController zombie)
    {
        playerToZombieDistance = Vector3.Distance(
            zombie.thePlayer.transform.position,
            zombie.transform.position
        );

        if (playerToZombieDistance <= 15)
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
        zombie.zombieNavAgent.acceleration = 8;
        zombie.zombieNavAgent.speed = 4.5f;
        Debug.Log("Entered Alert State!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        isAlerted(zombie);
        InMeleeRange(zombie);

        if (InMeleeRange(zombie))
        {
            zombie.SwitchState(zombie.attackState);
        }
        else
        {
            if (isAlerted(zombie))
            {
                zombie.zombieNavAgent.destination = zombie.thePlayer.transform.position;
            }
            else
            {
                zombie.SwitchState(zombie.idleState);
            }
        }
    }
}
