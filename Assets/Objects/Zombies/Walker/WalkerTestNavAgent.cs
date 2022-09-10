using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkerTestNavAgent : MonoBehaviour
{
    [SerializeField]
    Camera PerspectiveCamera;

    [SerializeField]
    private float wanderRadius = 10;

    [SerializeField]
    private float wanderTimer = 10;

    private float timer;

    private NavMeshAgent WalkerTestNavMeshAgent;

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void Awake()
    {
        WalkerTestNavMeshAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray PositionRay = PerspectiveCamera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(PositionRay, out hit))
        //     {
        //         WalkerTestNavMeshAgent.destination = hit.point;
        //     }
        // }

        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            WalkerTestNavMeshAgent.destination = newPos;
            timer = 0;
        }
    }
}
