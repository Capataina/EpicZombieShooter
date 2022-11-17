using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour
{
    [SerializeField] int inventoryGridWidth;
    [SerializeField] int inventoryGridHeight;

    [SerializeField] ItemBase itemToAddSmall;
    [SerializeField] ItemBase itemToAddBig;
    [SerializeField] ItemBase itemToAddHorizontal;
    [SerializeField] GameObject inventoryItemPrefab;

    InventoryItem[,] inventoryItemSlots;
    const int tileSize = 64;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init();

        InstansiateItem(itemToAddSmall, 1, 1);
        InstansiateItem(itemToAddBig, 3, 1);
        InstansiateItem(itemToAddHorizontal, 5, 1);
    }

    private void InstansiateItem(ItemBase item, int posX, int posY)
    {
        GameObject newInventoryItem = Instantiate(inventoryItemPrefab);
        newInventoryItem.GetComponent<Image>().sprite = item.itemIcon;
        newInventoryItem.GetComponent<RectTransform>().sizeDelta = new Vector2(
            item.inventoryWidth * tileSize,
            item.inventoryHeight * tileSize
        );

        InventoryItem newInvtentoryItemComp = newInventoryItem.GetComponent<InventoryItem>();
        newInvtentoryItemComp.width = item.inventoryWidth;
        newInvtentoryItemComp.height = item.inventoryHeight;

        AddItem(newInvtentoryItemComp, posX, posY);
    }

    private void Init()
    {
        rectTransform.sizeDelta = new Vector2(inventoryGridWidth * tileSize, inventoryGridHeight * tileSize);
        inventoryItemSlots = new InventoryItem[inventoryGridWidth, inventoryGridHeight];
    }

    public void AddItem(InventoryItem item, int posX, int posY)
    {
        for (int i = posX; i < posX + item.width; i++)
        {
            for (int j = posY; j < posY + item.height; j++)
            {
                inventoryItemSlots[i, j] = item;
            }
        }

        RectTransform itemRectTransform = item.GetComponent<RectTransform>();

        itemRectTransform.SetParent(rectTransform);

        itemRectTransform.localPosition = new Vector2(
            posX * tileSize,
            -posY * tileSize
        );
    }

    public void RemoveItem(InventoryItem item, int posX, int posY)
    {
        for (int i = posX; i < posX + item.width; i++)
        {
            for (int j = posY; j < posY + item.height; j++)
            {
                inventoryItemSlots[i, j] = null;
            }
        }
    }

    public InventoryItem GetItemAtMousePosition()
    {
        Vector2Int currentGrid = GetTileGridPosition();

        return inventoryItemSlots[currentGrid.x, currentGrid.y];
    }

    public Vector2Int GetGridTopLeftCorner(InventoryItem item)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        Vector2 pivotPosition = new Vector2(
            rectTransform.position.x + tileSize / 2,
            rectTransform.position.y - tileSize / 2
        );
        return GetTileGridPosition(pivotPosition);
    }

    public Vector2Int GetGridTopLeftCornerMouse(InventoryItem item)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        Vector2 pivotPosition = new Vector2(
            Input.mousePosition.x - rectTransform.sizeDelta.x / 2 + tileSize / 2,
            Input.mousePosition.y + rectTransform.sizeDelta.y / 2 - tileSize / 2
        );
        return GetTileGridPosition(pivotPosition);
    }

    public void AddItemToMousePosition(InventoryItem item)
    {
        Vector2Int currentGrid = GetGridTopLeftCornerMouse(item);

        AddItem(item, currentGrid.x, currentGrid.y);
    }

    public bool CheckOverlapAtMousePosition(InventoryItem item)
    {
        Vector2Int pos = GetGridTopLeftCornerMouse(item);

        if (pos.x < 0 || pos.y < 0 ||
        pos.x + item.width > inventoryItemSlots.GetLength(0) ||
        pos.y + item.height > inventoryItemSlots.GetLength(1))
        {
            return true;
        }

        print((pos.x + item.width, pos.y + item.height));

        for (int i = pos.x; i < pos.x + item.width; i++)
        {
            for (int j = pos.y; j < pos.y + item.height; j++)
            {
                if (inventoryItemSlots[i, j] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Vector2Int GetTileGridPosition(Vector2 position)
    {
        float posOnGridX = position.x - rectTransform.position.x;
        float posOnGridY = rectTransform.position.y - position.y;

        return new Vector2Int(
            (int)(posOnGridX / tileSize),
            (int)(posOnGridY / tileSize)
        );
    }

    public Vector2Int GetTileGridPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        float posOnGridX = mousePosition.x - rectTransform.position.x;
        float posOnGridY = rectTransform.position.y - mousePosition.y;

        return new Vector2Int(
            (int)(posOnGridX / tileSize),
            (int)(posOnGridY / tileSize)
        );
    }
}
