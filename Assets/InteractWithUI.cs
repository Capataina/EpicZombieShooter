using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class InteractWithUI : MonoBehaviour
{
    [SerializeField] RectTransform staminaBarRectTransform;

    float staminaBarInitialWidth;

    PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        staminaBarInitialWidth = staminaBarRectTransform.rect.width;
    }

    void UpdateStaminaBar()
    {
        staminaBarRectTransform.sizeDelta = new Vector2(playerStats.stamina / playerStats.maxStamina * staminaBarInitialWidth, staminaBarRectTransform.rect.height);
    }

    private void Update()
    {
        UpdateStaminaBar();
    }
}
