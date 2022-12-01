using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CanRagdoll : MonoBehaviour
{
    [SerializeField] GameObject ragdollArmature;
    [SerializeField] public Rigidbody spine;
    [SerializeField] Animator animator;
    [SerializeField] CapsuleCollider characterCollider;

    public void EnableRagdoll()
    {
        animator.enabled = false;
        characterCollider.enabled = false;
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
        }
    }

    public void DisableRagdoll()
    {
        animator.enabled = true;
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        characterCollider.enabled = true;
    }
}
