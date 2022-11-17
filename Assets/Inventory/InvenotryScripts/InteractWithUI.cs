using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithUI : MonoBehaviour
{
    [SerializeField] RectTransform staminaBarRectTransform;

    float staminaBarInitialWidth;

    PlayerData playerData;

    private void Awake()
    {
        staminaBarInitialWidth = staminaBarRectTransform.rect.width;
        playerData = PlayerData.Instance;
    }

    void UpdateStaminaBar()
    {
        staminaBarRectTransform.sizeDelta = new Vector2(playerData.Stamina / playerData.maxStamina * staminaBarInitialWidth, staminaBarRectTransform.rect.height);
    }

    private void Update()
    {
        UpdateStaminaBar();
    }
}
