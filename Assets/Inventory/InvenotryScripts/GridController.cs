using System.ComponentModel;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [HideInInspector] public ItemGrid activeGrid;
    [HideInInspector] public EquipmentSlot activeEquipmentSlot;
    [HideInInspector] public InventoryItem heldItem;

    Vector2Int lastPosition;
    ItemGrid lastActiveGrid;
    int lastHeight;
    int lastWidth;
    bool wasRotated;
    EquipmentSlot lastEquipmentSlot;
    Vector2 lastPivot;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && heldItem != null)
        {
            heldItem.ToggleRotation();
        }


        if (Input.GetMouseButtonUp(0) && heldItem != null)
        {
            if (HandleReload()) return;
            if (activeGrid != null)
            {
                heldItem.CorrectPivot();
                if (!activeGrid.CheckOverlapAndOverflowAtMousePosition(heldItem))
                {
                    activeGrid.AddItemToMousePosition(heldItem);
                    heldItem = null;
                }
                else
                {
                    PlaceToPreviousPos();
                }
            }
            else if (activeEquipmentSlot != null
            && activeEquipmentSlot.CheckIfEmpty()
            && heldItem.itemData.itemScript is ProjectileWeaponItems)
            {
                activeEquipmentSlot.PlaceItem(heldItem);
                heldItem = null;
            }
            else
            {
                PlaceToPreviousPos();
            }
        }


        if (Input.GetMouseButtonDown(0) && heldItem == null)
        {
            if (activeGrid != null)
            {
                heldItem = activeGrid.GetItemAtMousePosition();
                if (heldItem != null)
                {
                    heldItem.GetComponent<RectTransform>().SetAsLastSibling();
                    lastPivot = heldItem.GetComponent<RectTransform>().pivot;
                    heldItem.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    heldItem.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>());
                    lastPosition = activeGrid.GetGridTopLeftCorner(heldItem);
                    lastHeight = heldItem.height;
                    lastWidth = heldItem.width;
                    wasRotated = heldItem.rotated;
                    lastActiveGrid = activeGrid;
                    activeGrid.RemoveItem(heldItem, lastPosition.x, lastPosition.y);
                    lastEquipmentSlot = null;
                }
            }
            else if (activeEquipmentSlot != null && !activeEquipmentSlot.CheckIfEmpty())
            {
                heldItem = activeEquipmentSlot.TakeItem();
                lastEquipmentSlot = activeEquipmentSlot;
                lastActiveGrid = null;
            }
        }

        if (heldItem != null)
        {
            DragItem();
        }
    }

    private void DragItem()
    {
        RectTransform itemTransform = heldItem.GetComponent<RectTransform>();
        itemTransform.position = Input.mousePosition;
    }

    private void PlaceToPreviousPos()
    {
        if (lastEquipmentSlot != null)
        {
            lastEquipmentSlot.PlaceItem(heldItem);
            heldItem = null;
        }
        else if (lastActiveGrid != null)
        {
            if (wasRotated)
            {
                heldItem.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                heldItem.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            }
            heldItem.rotated = wasRotated;
            heldItem.GetComponent<RectTransform>().pivot = lastPivot;
            heldItem.width = lastWidth;
            heldItem.height = lastHeight;
            lastActiveGrid.AddItem(heldItem, lastPosition.x, lastPosition.y);
            heldItem = null;
        }
        else
        {
            Debug.LogError("Missing item origin");
        }
    }

    private bool HandleReload()
    {
        if (heldItem.itemData.itemScript is MagazineAttachment)
        {
            if (activeGrid != null && activeGrid.GetItemAtMousePosition() != null)
            {
                InventoryItem item = activeGrid.GetItemAtMousePosition();

                if (item.itemData.itemScript is ProjectileWeaponItems)
                {
                    ItemData insertedMag = (item.itemData.GetRuntimeData()
                    as ProjectileWeaponItemsRuntimeData).insertedMagazine;

                    if (insertedMag != null)
                    {
                        insertedMag.gameObject.SetActive(true);
                        activeGrid.QuickAddToInventory(insertedMag);
                    }

                    (item.itemData.GetRuntimeData() as ProjectileWeaponItemsRuntimeData).insertedMagazine = heldItem.itemData;

                    ItemData newInsertedMag = (item.itemData.GetRuntimeData()
                    as ProjectileWeaponItemsRuntimeData).insertedMagazine;
                    print((newInsertedMag.GetRuntimeData() as MagazineAttachmentRuntimeData).bulletCount);

                    heldItem.gameObject.SetActive(false);
                    heldItem = null;
                    return true;
                }
            }
            else if (activeEquipmentSlot != null
            && !activeEquipmentSlot.CheckIfEmpty()
            && activeEquipmentSlot.equippedItem.itemData.itemScript is ProjectileWeaponItems
            && heldItem.itemData.itemScript is MagazineAttachment)
            {
                ProjectileWeaponItemsRuntimeData runtimeData = activeEquipmentSlot.equippedItem.itemData.GetRuntimeData() as ProjectileWeaponItemsRuntimeData;
                if (runtimeData.insertedMagazine != null)
                {
                    runtimeData.insertedMagazine.gameObject.SetActive(true);
                    lastActiveGrid.QuickAddToInventory(runtimeData.insertedMagazine);
                }
                runtimeData.insertedMagazine = heldItem.itemData;
                heldItem.gameObject.SetActive(false);
                heldItem = null;
                return true;
            }
        }
        return false;

    }
}
