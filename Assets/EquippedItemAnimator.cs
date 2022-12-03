using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquippedItemAnimator : MonoBehaviour
{
    PlayerData playerData;
    [SerializeField] Animator animator;
    [SerializeField] float animationTransitionTime;

    int armLayer;

    private void Awake()
    {
        playerData = PlayerData.Instance;
        armLayer = animator.GetLayerIndex("Arm Layer");
    }

    private void Update()
    {
        if (playerData.isAiming)
        {
            SwitchAimingArmAnimation();
        }
        else if (playerData.equippedItem != null)
        {
            SwitchIdleArmAnimation();
        }
        else
        {
            if (!animator.IsInTransition(armLayer))
            {
                animator.CrossFadeInFixedTime("EmptyState", animationTransitionTime, armLayer);
            }
        }
    }

    private void PlayAnimation(string name)
    {
        if (!animator.IsInTransition(armLayer))
        {
            animator.CrossFadeInFixedTime("Armature|" + name, animationTransitionTime, armLayer);
        }
    }

    private void SwitchIdleArmAnimation()
    {
        ItemBase item = playerData.equippedItem;


        // no item is equipped
        if (item == null)
        {
            PlayAnimation("EmptyState");
            return;
        }

        switch (item.name)
        {
            case "m416":
                PlayAnimation("HoldingM416");
                break;

            case "M1911":
                PlayAnimation("HoldingM1911");
                break;

            default:
                //Debug.LogError("Unrecognzied type");
                break;
        }
    }

    private void SwitchAimingArmAnimation()
    {
        ItemBase item = playerData.equippedItem;

        // no item is equipped
        if (item == null)
        {
            PlayAnimation("PoseAimUnArmed");
            return;
        }

        switch (item.name)
        {
            case "m416":
                PlayAnimation("PoseAimM416");
                break;
            case "M1911":
                PlayAnimation("PoseAimM1911");
                break;

            default:
                //Debug.LogError("Unrecognzied type");
                break;
        }
    }

    private void PlayAnimationNoTransition(string name)
    {
        animator.Play("Armature|" + name, armLayer);
    }

    public void SwitchShootingArmAnimation()
    {
        ItemBase item = playerData.equippedItem;

        // no item is equipped
        if (item == null)
        {
            //PlayAnimation("PoseAimUnArmed");
            return;
        }

        switch (item.name)
        {
            case "m416":
                PlayAnimationNoTransition("ShootingM416");
                break;
            case "M1911":
                PlayAnimationNoTransition("ShootingM1911");
                break;
            default:
                //Debug.LogError("Unrecognzied type");
                break;
        }
    }
}
