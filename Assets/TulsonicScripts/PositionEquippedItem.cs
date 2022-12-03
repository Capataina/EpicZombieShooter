using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEquippedItem : MonoBehaviour
{

    [SerializeField] GameObject handIK;
    [SerializeField] Transform hand;

    PlayerData playerData;

    private void OnEnable()
    {
        playerData.itemEquippedEvent.AddListener(InitializeEquipment);
        playerData.itemUnequipEvent.AddListener(RemoveEquippedItem);
    }

    private void OnDisable()
    {
        playerData.itemEquippedEvent.RemoveListener(InitializeEquipment);
        playerData.itemUnequipEvent.RemoveListener(RemoveEquippedItem);
    }

    private void Awake()
    {
        playerData = PlayerData.Instance;
    }

    private void Update()
    {
        if (playerData.equipmentModel != null)
        {
            playerData.equipmentModel.transform.position = hand.position;
        }
    }

    private void InitializeEquipment(ItemBase item)
    {
        playerData.equipmentModel = Instantiate(playerData.equippedItem.itemModel, Vector3.zero, Quaternion.identity);
        playerData.equipmentModel.transform.parent = handIK.transform;
        playerData.equipmentModel.transform.localPosition = Vector3.zero;
        playerData.equipmentModel.transform.localRotation = Quaternion.identity;
        GetComponent<WeasponShooting>().Initialize(item);
    }

    private void RemoveEquippedItem()
    {
        GameObject.Destroy(playerData.equipmentModel);
        playerData.equipmentModel = null;
    }
}

