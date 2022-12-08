using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextMenu : MonoBehaviour
{
    [HideInInspector] protected ItemGrid grid;
    [HideInInspector] protected InventoryItem item;

    public void Initialize(ItemGrid grid, InventoryItem item)
    {
        this.grid = grid;
        this.item = item;
    }

    public void RemoveContextMenu()
    {
        Destroy(gameObject);
    }
}
