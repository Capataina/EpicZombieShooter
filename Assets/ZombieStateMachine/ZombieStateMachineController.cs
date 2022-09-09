using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachineController : MonoBehaviour
{
    ZombieCurrentState currentState;

    [SerializeField]
    ZombieBaseIdleState idleState = new ZombieBaseIdleState();

    [SerializeField]
    ZombieBaseWanderState wanderState = new ZombieBaseWanderState();

    [SerializeField]
    ZombieBaseAlertState alertState = new ZombieBaseAlertState();

    [SerializeField]
    ZombieBaseAttackState attackState = new ZombieBaseAttackState();

    [SerializeField]
    ZombieBaseDeathState deathState = new ZombieBaseDeathState();

    void Start()
    {
        currentState = idleState;
    }

    // Update is called once per frame
    void Update() { }
}
