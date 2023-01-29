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
    public ZombieBaseDeathState investigateState;

    [SerializeField]
    public ZombieBaseAttackState attackState;

    [SerializeField]
    public ZombieBaseDeathState deathState;

    [HideInInspector] public GameObject thePlayer;

    [SerializeField] public Animator animator;

    void Awake()
    {
        zombieNavAgent = GetComponent<NavMeshAgent>();
        playerData = PlayerData.Instance;
    }

    private void OnEnable()
    {
        playerData.playerAssignedEvent.AddListener(AssignThePlayer);
    }

    private void OnDestroy()
    {
        playerData.playerAssignedEvent.RemoveListener(AssignThePlayer);
    }

    void AssignThePlayer(GameObject player)
    {
        thePlayer = player;
    }

    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        GetComponent<CanRagdoll>().DisableRagdoll();
    }

    void Update()
    {
        currentState.UpdateState(this);
        CheckDeathState();
    }

    void CheckDeathState()
    {
        if (GetComponent<Zombie>().Health <= 0 && currentState != deathState)
        {
            SwitchState(deathState);
        }
    }

    public void SwitchState(ZombieCurrentState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void playAnimation(string name)
    {
        if (!animator.IsInTransition(0))
        {
            animator.CrossFade("Armature|" + name, 0);
        }
    }
}
