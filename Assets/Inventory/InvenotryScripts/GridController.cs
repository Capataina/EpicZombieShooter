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
            else if (activeEquipmentSlot != null && activeEquipmentSlot.CheckIfEmpty())
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
}
