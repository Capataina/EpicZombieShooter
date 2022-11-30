using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquippedItemAnimator : MonoBehaviour
{
    PlayerData playerData;
    [SerializeField] Animator animator;

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
        else
        {
            animator.Play("EmptyState", armLayer);
        }
    }

    private void PlayAnimation(string name)
    {
        if (animator.IsInTransition(armLayer))
        {
            animator.CrossFade("Armature|" + name, armLayer);
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
            case "M416":
                PlayAnimation("PoseAimM416");
                print("gun");
                break;

            default:
                //Debug.LogError("Unrecognzied type");
                break;
        }
    }

}
