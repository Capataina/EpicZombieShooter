using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMachineController : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent baseWander;

    [HideInInspector]
    public ZombieCurrentState currentState;

    [SerializeField]
    public ZombieBaseIdleState idleState;

    [SerializeField]
    public ZombieBaseWanderState wanderState;

    [SerializeField]
    public ZombieBaseAlertState alertState;

    [SerializeField]
    public ZombieBaseAttackState attackState;

    [SerializeField]
    public ZombieBaseDeathState deathState;

    void Awake()
    {
        baseWander = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ZombieCurrentState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
