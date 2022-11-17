using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public int width;
    [HideInInspector] public int height;
    [HideInInspector] public bool rotated = false;

    RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
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
}
