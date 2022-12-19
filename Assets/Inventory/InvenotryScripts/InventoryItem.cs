using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemData))]
public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public int width;
    [HideInInspector] public int height;
    [HideInInspector] public bool rotated = false;
    [HideInInspector] public ItemData itemData;

    RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform ??= GetComponent<RectTransform>();
        itemData ??= GetComponent<ItemData>();
    }

    public void ToggleRotation()
    {
        if (rotated)
        {
            rectTransform.rotation = Quaternion.identity;
        }
        else
        {
            rectTransform.rotation = Quaternion.Euler(0, 0, -90);
        }
        rotated = !rotated;
        int t = width;
        width = height;
        height = t;
    }

    public void CorrectPivot()
    {
        // Correct the pivot positoin from top left to bottom left while
        // rotating
        Vector2 newPivot;
        if (rotated)
        {
            newPivot = new Vector2(0, 0);
        }
        else
        {
            newPivot = new Vector2(0, 1);
        }
        rectTransform.pivot = newPivot;
    }

    public void PrepareForEquipmentSlot(EquipmentSlot slot)
    {
        if (rotated)
        {
            ToggleRotation();
        }

        rectTransform.SetParent(slot.GetComponent<RectTransform>());
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.localPosition = new Vector2(0, 0);
    }

}
