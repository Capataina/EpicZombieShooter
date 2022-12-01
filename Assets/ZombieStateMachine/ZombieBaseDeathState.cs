using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Death")]
public class ZombieBaseDeathState : ZombieCurrentState
{
    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("dead");
        zombie.GetComponent<CanRagdoll>().EnableRagdoll();
        zombie.GetComponent<CanRagdoll>().spine.AddForce(zombie.zombieNavAgent.velocity * 5, ForceMode.VelocityChange);
        zombie.zombieNavAgent.destination = zombie.transform.position;
    }

    public override void UpdateState(ZombieStateMachineController zombie) { }
}
