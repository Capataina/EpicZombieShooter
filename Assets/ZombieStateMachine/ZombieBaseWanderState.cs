using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Wander")]
public class ZombieBaseWanderState : ZombieCurrentState
{
    private float wanderRadius = 10;
    private float wanderTimer = 4;
    private float timer = 2;
    public float wanderChance = 100;
    public float timesWandered = 0;
    private float randomPercentage = 0;

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void Wander(ZombieStateMachineController zombie)
    {
        Vector3 newPos = RandomNavSphere(zombie.transform.position, wanderRadius, -1);
        zombie.baseWander.destination = newPos;
        timesWandered += 1;
        wanderChance = (100 - (timesWandered * 15));
        timer = 0;
        randomPercentage = Random.Range(0, 100);
    }

    public override void EnterState(ZombieStateMachineController zombie)
    {
        Debug.Log("Entered wandering state!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        Debug.Log(wanderChance);
        Debug.Log(timesWandered);
        if (wanderChance >= randomPercentage)
        {
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                Wander(zombie);
            }
        }
        else
        {
            timer = 2;
            timesWandered = 0;
            wanderChance = 100;
            zombie.SwitchState(zombie.idleState);
        }
    }
}
