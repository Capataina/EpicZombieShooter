using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Alert")]
public class ZombieBaseAlertState : ZombieCurrentState
{
    [SerializeField] float alertSpeed;
    private float playerToZombieDistance;

    // Check if zombie should attack
    private bool InMeleeRange(ZombieStateMachineController zombie)
    {
        Vector3 zombPlayerVec =
            zombie.thePlayer.transform.position -
            zombie.transform.position;

        RaycastHit hit;

        if (Physics.Raycast(zombie.transform.position, zombPlayerVec.normalized, out hit, 2, LayerMask.GetMask("Default")) &&
        hit.collider.gameObject.name == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Check if zombie should be in alert state
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

    public override void EnterState(ZombieStateMachineController zombie)
    {
        zombie.zombieNavAgent.acceleration = 8;
        zombie.zombieNavAgent.speed = alertSpeed;
        zombie.playAnimation("Chase");
        Debug.Log("Entered Alert State!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        zombie.playAnimation("Chase");

        if (InMeleeRange(zombie))
        {
            zombie.SwitchState(zombie.attackState);
        }
        else if (!IsAlerted(zombie))
        {
            zombie.SwitchState(zombie.investigateState);
        }
    }
}
