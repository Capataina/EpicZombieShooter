using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] public float itemId;
    [SerializeField] public string itemName;
    [SerializeField] public GameObject itemObject;
    [SerializeField] public GameObject itemModel;
    [SerializeField] public Sprite itemIcon;
    [SerializeField] public int inventoryWidth;
    [SerializeField] public int inventoryHeight;
    [SerializeField] public GameObject contextMenu;
    protected static PlayerData playerData;

    private void OnEnable()
    {
        playerData = PlayerData.Instance;
    }

}