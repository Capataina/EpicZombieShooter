using UnityEngine;

public class GridController : MonoBehaviour
{
    [HideInInspector] public ItemGrid activeGrid;
    [HideInInspector] public InventoryItem heldItem;

    Vector2Int lastPosition;
    ItemGrid lastActiveGrid;
    int lastHeight;
    int lastWidth;
    bool wasRotated;
    Vector2 lastPivot;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && heldItem != null)
        {
            heldItem.ToggleRotation();
        }

        if (Input.GetMouseButtonUp(0) && heldItem != null)
        {
            if (activeGrid == null)
            {
                PlaceToPreviousPos();
            }
            else
            {
                heldItem.CorrectPivot();
                if (!activeGrid.CheckOverlapAndOverflowAtMousePosition(heldItem))
                {
                    activeGrid.AddItemToMousePosition(heldItem);
                }
                else
                {
                    PlaceToPreviousPos();
                }
            }
            heldItem = null;
        }


        if (Input.GetMouseButtonDown(0) && heldItem == null && activeGrid != null)
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
    }
}
