using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Alert")]
public class ZombieBaseAlertState : ZombieCurrentState
{
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

    public override void EnterState(ZombieStateMachineController zombie) { }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        determineIfAlerted(zombie);

        if (isAlerted)
        {
            zombie.zombieNavAgent.destination = zombie.thePlayer.transform.position;
        }
        else
        {
            zombie.SwitchState(zombie.idleState);
        }
    }
}
