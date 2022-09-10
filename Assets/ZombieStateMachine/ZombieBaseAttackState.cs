using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Attack")]
public class ZombieBaseAttackState : ZombieCurrentState
{
    public override void EnterState(ZombieStateMachineController zombie) { }

    public override void UpdateState(ZombieStateMachineController zombie) { }
}
