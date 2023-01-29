using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Investigate")]
public class ZombieBaseInvestigateState : ZombieCurrentState
{
    public override void EnterState(ZombieStateMachineController zombie)
    {
        // go to the last seen player position
        zombie.zombieNavAgent.destination = zombie.thePlayer.transform.position;
    }

    private bool IsAlerted(ZombieStateMachineController zombie)
    {
        Vector3 zombPlayerVec =
            zombie.thePlayer.transform.position -
            zombie.transform.position;

        RaycastHit hit;

        if (Physics.Raycast(zombie.transform.position, zombPlayerVec.normalized, out hit, 15, LayerMask.GetMask("Default")) &&
        hit.collider.gameObject.name == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        if (IsAlerted(zombie))
        {
            zombie.SwitchState(zombie.alertState);
        }

        if (Vector3.Distance(zombie.transform.position, zombie.zombieNavAgent.destination) <= 0.7f)
        {
            zombie.SwitchState(zombie.idleState);
        }
    }

}