using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInventoryGrid : MonoBehaviour
{
    void OnEnable()
    {
        PlayerData.Instance.inventoryGrid ??= GetComponent<ItemGrid>();
    }
}
