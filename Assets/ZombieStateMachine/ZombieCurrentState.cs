using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieCurrentState
{
    public abstract void EnterState(ZombieStateMachineController zombie);

    public abstract void UpdateState(ZombieStateMachineController zombie);
}
