using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Death")]
public class ZombieBaseDeathState : ZombieCurrentState
{
    public override void EnterState(ZombieStateMachineController zombie) { }

    public override void UpdateState(ZombieStateMachineController zombie) { }
}
