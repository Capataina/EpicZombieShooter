using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateMachineController : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent zombieNavAgent;

    [HideInInspector]
    public ZombieCurrentState currentState;

    [HideInInspector]
    public PlayerData playerData;

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

    [SerializeField]
    public GameObject thePlayer;

    void Awake()
    {
        zombieNavAgent = GetComponent<NavMeshAgent>();
        playerData = PlayerData.Instance;
    }

    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentState);
        currentState.UpdateState(this);
    }

    public void SwitchState(ZombieCurrentState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
