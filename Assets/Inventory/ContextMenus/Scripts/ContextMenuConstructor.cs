using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    menuName = "Singletons/Context Menu Constructor",
    fileName = "Context Menu Constructor"
    )]
public class ContextMenuConstructor : SingletonScriptableObject<ContextMenuConstructor>
{
    [SerializeField] GameObject contextMenuBasePrefab;
    [SerializeField] GameObject contextMenuButtonPrefab;

    public GameObject ConstructContextMenu(ContextMenuButton[] buttonArray)
    {
        GameObject contextMenuBase = Instantiate(contextMenuBasePrefab);

        foreach (var b in buttonArray)
        {
            GameObject newButton = Instantiate(contextMenuButtonPrefab);
            newButton.transform.parent = contextMenuBase.transform;
            newButton.GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    b.action();
                    Destroy(contextMenuBase);
                });
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = b.text;
        }

        return contextMenuBase;
    }
}

public class ContextMenuButton
{
    public string text;
    public Action action;

    public ContextMenuButton(string text, Action action)
    {
        this.text = text;
        this.action = action;
    }
}

