using UnityEngine;
using UnityEngine.EventSystems;

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ItemGrid itemGrid;
    [SerializeField] GridController gridController;

    private void Start()
    {
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gridController.activeGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridController.activeGrid = null;
    }

}
