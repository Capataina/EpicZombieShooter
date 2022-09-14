using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] public float itemId;
    [SerializeField] public string itemName;
    [SerializeField] public GameObject itemModel;
    [SerializeField] public Sprite itemIcon;
    protected static PlayerData playerData;

    private void OnEnable()
    {
        playerData = PlayerData.Instance;
    }
}
