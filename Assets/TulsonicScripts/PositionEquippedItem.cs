using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEquippedItem : MonoBehaviour
{

    [SerializeField] GameObject equippedWeaponPosition;

    PlayerData playerData;
    GameObject equipmentModel;

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

    private void InitializeEquipment(ItemBase item)
    {
        equipmentModel = Instantiate(playerData.equippedItem.itemModel, Vector3.zero, Quaternion.identity);
        equipmentModel.transform.parent = equippedWeaponPosition.transform;
        equipmentModel.transform.localPosition = Vector3.zero;
        equipmentModel.transform.localRotation = Quaternion.identity;
        GetComponent<WeasponShooting>().Initialize(item);
    }

    private void RemoveEquippedItem()
    {
        GameObject.Destroy(equipmentModel);
        equipmentModel = null;
    }
}

