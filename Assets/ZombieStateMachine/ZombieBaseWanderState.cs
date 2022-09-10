using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Wander")]
public class ZombieBaseWanderState : ZombieCurrentState
{
    private float wanderRadius = 10;
    private float wanderTimer = 5;
    private float timer = 0;
    private float continueWander;

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("Entered wandering state!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(zombie.transform.position, wanderRadius, -1);
            zombie.baseWander.destination = newPos;
            timer = 0;
            zombie.SwitchState(zombie.idleState);
            // continueWander = Random.Range(0, 100);
            // if (continueWander >= 70)
            // {
            //     Vector3 newPos = RandomNavSphere(zombie.transform.position, wanderRadius, -1);
            //     zombie.baseWander.destination = newPos;
            //     timer = 0;
            // }
        }
    }
}
