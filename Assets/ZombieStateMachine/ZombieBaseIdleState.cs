using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Idle")]
public class ZombieBaseIdleState : ZombieCurrentState
{
    private float timeToSwitch = 5;

    private float stateSwitchTimer = 0;

    private bool isAlerted;
    private float playerToZombieDistance;

    private void determineIfAlerted(ZombieStateMachineController zombie)
    {
        playerToZombieDistance = Vector3.Distance(
            zombie.thePlayer.transform.position,
            zombie.transform.position
        );

        if (playerToZombieDistance <= 15)
        {
            isAlerted = true;
        }
        else
        {
            isAlerted = false;
        }
    }

    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("Entered Idle State!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        determineIfAlerted(zombie);
        if (isAlerted)
        {
            zombie.SwitchState(zombie.alertState);
        }
        else
        {
            if (zombie.currentState == zombie.idleState)
            {
                stateSwitchTimer += Time.deltaTime;
            }

            if (stateSwitchTimer >= timeToSwitch)
            {
                zombie.SwitchState(zombie.wanderState);
                stateSwitchTimer = 0;
            }
        }
    }
}
