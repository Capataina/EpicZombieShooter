using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] ItemGrid inventoryGrid;

    bool inventoryIsActive = false;
    PlayerData playerData;

    private void Start()
    {
        playerData = PlayerData.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryIsActive)
            {
                inventoryCanvas.SetActive(false);
            }
            else
            {
                inventoryCanvas.SetActive(true);
            }
            inventoryIsActive = !inventoryIsActive;
        }

        // pickup item
        if (Input.GetKeyDown(KeyCode.E))
        {
            // get items in pickup range
            Collider[] itemsInRange = Physics.OverlapSphere(
                transform.position,
                playerData.itemPickupRadius,
                LayerMask.GetMask("Item")
            );
            if (itemsInRange.Length == 0 || itemsInRange == null)
                return;

            // find closest item and add to inventory
            GameObject closestItem = null;
            float minDist = Mathf.Infinity;
            foreach (Collider item in itemsInRange)
            {
                float curDist = Vector3.Distance(item.transform.position, transform.position);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    closestItem = item.gameObject;
                }
            }
            print("picked up item");
            inventoryGrid.QuickAddToInventory(closestItem.GetComponent<ItemObject>().itemScript);
            Destroy(closestItem);
        }
    }
}
