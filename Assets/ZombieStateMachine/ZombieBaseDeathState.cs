using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Death")]
public class ZombieBaseDeathState : ZombieCurrentState
{
    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("dead");
        Vector3 velocity = zombie.zombieNavAgent.velocity;
        zombie.GetComponent<CanRagdoll>().EnableRagdoll();
        zombie.GetComponent<CanRagdoll>().spine.AddForce(velocity * 5, ForceMode.VelocityChange);
    }

    public override void UpdateState(ZombieStateMachineController zombie) { }
}
