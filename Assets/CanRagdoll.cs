using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.AI;

public class CanRagdoll : MonoBehaviour
{
    [SerializeField] GameObject ragdollArmature;
    [SerializeField] public Rigidbody spine;
    [SerializeField] Animator animator;
    [SerializeField] CapsuleCollider characterCollider;
    [SerializeField] NavMeshAgent navMeshAgent;

    public void EnableRagdoll()
    {
        animator.enabled = false;
        characterCollider.enabled = false;
        navMeshAgent.enabled = false;
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
    }

    public void DisableRagdoll()
    {
        animator.enabled = true;
        navMeshAgent.enabled = true;
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        characterCollider.enabled = true;
    }
}
