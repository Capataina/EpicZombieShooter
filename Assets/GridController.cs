using UnityEngine;

public class GridController : MonoBehaviour
{
    [HideInInspector] public ItemGrid activeGrid;
    [HideInInspector] public InventoryItem heldItem;

    Vector2Int lastPosition;
    ItemGrid lastActiveGrid;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && heldItem != null)
        {
            int t = heldItem.width;
            heldItem.width = heldItem.height;
            heldItem.height = t;

            RectTransform rectTransform = heldItem.GetComponent<RectTransform>();
            if (rectTransform.localRotation == Quaternion.identity)
            {
                rectTransform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                rectTransform.localRotation = Quaternion.identity;
            }
        }

        if (Input.GetMouseButtonUp(0) && heldItem != null)
        {
            if (activeGrid == null)
            {
                print("no grid");
                lastActiveGrid.AddItem(heldItem, lastPosition.x, lastPosition.y);
            }
            else
            {
                heldItem.GetComponent<RectTransform>().pivot = new Vector2(0, 1f);
                if (!activeGrid.CheckOverlapAtMousePosition(heldItem))
                {
                    print("overlap");
                    activeGrid.AddItemToMousePosition(heldItem);
                }
                else
                {
                    print("placing");
                    lastActiveGrid.AddItem(heldItem, lastPosition.x, lastPosition.y);
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
                heldItem.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                lastPosition = activeGrid.GetGridTopLeftCorner(heldItem);
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
}
