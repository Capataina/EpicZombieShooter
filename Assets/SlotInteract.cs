using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    EquipmentSlot equipmentSlot;
    [SerializeField] GridController gridController;

    private void Start()
    {
        equipmentSlot = GetComponent<EquipmentSlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gridController.activeEquipmentSlot = equipmentSlot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridController.activeEquipmentSlot = null;
    }
}
