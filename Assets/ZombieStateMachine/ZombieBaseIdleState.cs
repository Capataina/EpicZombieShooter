using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Idle")]
public class ZombieBaseIdleState : ZombieCurrentState
{
    private float timeToSwitch = 3;

    private float stateSwitchTimer = 0;

    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("Entered Idle State!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
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
