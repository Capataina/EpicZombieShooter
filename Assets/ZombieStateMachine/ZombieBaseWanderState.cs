using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "StateMachine/Zombie/BaseStates/Wander")]
public class ZombieBaseWanderState : ZombieCurrentState
{

    [SerializeField] float wanderSpeed;

    private float wanderRadius = 10;
    public float wanderChance = 100;
    public float timesWandered = 0;
    private float randomPercentage = 0;
    private float playerToZombieDistance;
    private float zombieDestinationDistance;
    private Vector3 newPos;
    private float staticTime;

    private void StaticCheck(ZombieStateMachineController zombie)
    {
        if (zombie.zombieNavAgent.velocity.magnitude <= 1)
        {
            staticTime += Time.deltaTime;
            if (staticTime >= 5)
            {
                Wander(zombie);
                staticTime = 0;
                //Debug.Log("Static Check Triggered! This is a certified bruh moment.");
            }
        }
    }

    private void getDistance(ZombieStateMachineController zombie)
    {
        zombieDestinationDistance = Vector3.Distance(
            zombie.transform.position,
            new Vector3(newPos.x, zombie.transform.position.y, newPos.z)
        );
    }

    private bool isAlerted(ZombieStateMachineController zombie)
    {
        playerToZombieDistance = Vector3.Distance(
            zombie.thePlayer.transform.position,
            zombie.transform.position
        );

        if (playerToZombieDistance <= 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, 20f, layermask);

        return navHit.position;
    }

    private void Wander(ZombieStateMachineController zombie)
    {
        newPos = RandomNavSphere(zombie.transform.position, wanderRadius, NavMesh.AllAreas);
        zombie.zombieNavAgent.destination = newPos;
        timesWandered += 1;
        wanderChance = (100 - (timesWandered * 15));
        randomPercentage = Random.Range(0, 100);
    }

    public override void EnterState(ZombieStateMachineController zombie)
    {
        timesWandered = 0;
        wanderChance = 100;
        zombie.zombieNavAgent.acceleration = 3;
        zombie.zombieNavAgent.speed = wanderSpeed;
        Wander(zombie);
        Debug.Log("Entered wandering state!");
    }

    public override void UpdateState(ZombieStateMachineController zombie)
    {
        // Debug.Log(
        //     (
        //         zombie.zombieNavAgent.speed,
        //         zombie.zombieNavAgent.acceleration,
        //         zombie.zombieNavAgent.velocity,
        //         zombie.zombieNavAgent.velocity.magnitude
        //     )
        // );

        zombie.playAnimation("Walk");
        isAlerted(zombie);
        getDistance(zombie);
        StaticCheck(zombie);

        // Debug.Log((zombieDestinationDistance, zombie.transform.position, newPos));

        if (isAlerted(zombie))
        {
            zombie.SwitchState(zombie.alertState);
        }
        else
        {
            if (zombieDestinationDistance <= 0.51)
            {
                if (wanderChance >= randomPercentage)
                {
                    Wander(zombie);
                }
                else
                {
                    timesWandered = 0;
                    wanderChance = 100;
                    zombie.SwitchState(zombie.idleState);
                }
            }
        }

        // if (isAlerted)
        // {
        //     zombie.SwitchState(zombie.alertState);
        // }
        // else
        // {
        //     if (wanderChance >= randomPercentage)
        //     {
        //         timer += Time.deltaTime;
        //         if (timer >= wanderTimer)
        //         {
        //             Wander(zombie);
        //         }
        //     }
        //     else
        //     {
        //         timer = 2;
        //         timesWandered = 0;
        //         wanderChance = 100;
        //         zombie.SwitchState(zombie.idleState);
        //     }
        // }
    }
}
