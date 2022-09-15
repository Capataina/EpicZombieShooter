using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEquippedItem : MonoBehaviour
{

    [SerializeField] GameObject equippedWeaponPosition;

    PlayerData playerData;

    private void OnEnable()
    {
        playerData.itemEquippedEvent.AddListener(InitializeEquipment);
    }

    private void OnDisable()
    {
        playerData.itemEquippedEvent.RemoveListener(InitializeEquipment);
    }

    private void Awake()
    {
        playerData = PlayerData.Instance;
    }

    private void InitializeEquipment(ItemBase item)
    {
        GameObject equipmentModel = Instantiate(playerData.equippedItem.itemModel, Vector3.zero, Quaternion.identity);
        equipmentModel.transform.parent = equippedWeaponPosition.transform;
        equipmentModel.transform.localPosition = Vector3.zero;
        equipmentModel.transform.localRotation = Quaternion.identity;
        GetComponent<WeasponShooting>().Initialize(item);
    }
}
