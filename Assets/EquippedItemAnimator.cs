using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class EquippedItemAnimator : MonoBehaviour
{
    PlayerData playerData;
    [SerializeField] Animator animator;
    [SerializeField] float animationTransitionTime;

    int armLayer;

    bool isReloading = false;
    bool staratedReloading = true;
    float fadeTimer = Mathf.Infinity;
    ItemBase item;

    private void Awake()
    {
        playerData = PlayerData.Instance;
        armLayer = animator.GetLayerIndex("Arm Layer");
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(armLayer).length >
               animator.GetCurrentAnimatorStateInfo(armLayer).normalizedTime;
    }


    private void Update()
    {
        if (playerData.equippedItem != null)
        {
            item = playerData.equippedItem.itemScript;
        }

        if (fadeTimer < animationTransitionTime)
        {
            fadeTimer += Time.deltaTime;
        }

        if (!isReloading)
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
        else
        {
            if (!AnimatorIsPlaying())
            {
                isReloading = false;
                animator.SetBool("isReloading", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            animator.SetBool("isReloading", true);
            animator.Play("Armature|ReloadM1911", armLayer);
            fadeTimer = 0;
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
