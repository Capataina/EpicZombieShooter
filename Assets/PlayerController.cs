using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;
    private CharacterController playerControls;
    private Vector3 PlayerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    [SerializeField]
    bool affectedByGravity = true;

    [SerializeField]
    private float jumpHeight = 4.0f;

    [SerializeField]
    LayerMask defaultLayer;

    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float sprintStaminaCost;

    [HideInInspector]
    public bool isSprinting;

    private float speed;

    private bool isGrounded()
    {
        RaycastHit hit;

        return Physics.SphereCast(
            transform.position - (Vector3.up * 1f),
            0.2f,
            Vector3.down,
            out hit,
            0.2f,
            defaultLayer
        );
    }

    // Detect rc, find location, rotate character controller

    // Start is called before the first frame update
    void Start()
    {
        playerData = PlayerData.Instance;
        playerControls = GetComponent<CharacterController>();
        playerCamera.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical player inputs

        Vector3 playerMovement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // Check if sprinting and adjust speed
        if (
            Input.GetKey(KeyCode.LeftShift)
            && playerData.Stamina > 0
            && playerMovement != Vector3.zero
        )
        {
            speed = playerData.sprintSpeed;
            isSprinting = true;
            playerData.ReduceStamina(sprintStaminaCost);
        }
        else
        {
            speed = playerData.walkSpeed;
            isSprinting = false;
            if (playerData.Stamina < playerData.maxStamina)
                playerData.AddStamina(playerData.staminaGain);
        }

        // Right click rottaion
        Vector3 mousePosition = Input.mousePosition;
        Ray mousePositionRay = playerCamera.ScreenPointToRay(mousePosition);
        Plane mousePositionPlane = new Plane(Vector3.up, Vector3.up * transform.position.y);
        float mouseDistance;
        if (
            mousePositionPlane.Raycast(mousePositionRay, out mouseDistance)
            && Input.GetMouseButton(1)
        )
        {
            Vector3 rotaionTarget = mousePositionRay.GetPoint(mouseDistance);
            Vector3 rotationDirection = rotaionTarget - transform.position;
            float playerRotation =
                Mathf.Atan2(rotationDirection.x, rotationDirection.z) * Mathf.Rad2Deg;

            Quaternion desiredRotation = Quaternion.Euler(0, playerRotation, 0);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                desiredRotation,
                Time.deltaTime * rotationSpeed
            );

            // transform.rotation = Quaternion.Euler(0, playerRotation, 0);
        }

        // is player grounded
        groundedPlayer = isGrounded();
        // print(groundedPlayer);

        if (groundedPlayer && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = 0f;
        }

        // Horizontal and Vertical Movement

        playerControls.Move(playerMovement * Time.deltaTime * speed);

        if (playerMovement != Vector3.zero && !Input.GetMouseButton(1))
        {
            Quaternion newPlayerRotation = Quaternion.LookRotation(playerMovement, Vector3.up);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                newPlayerRotation,
                Time.deltaTime * rotationSpeed
            );
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            PlayerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (affectedByGravity)
            PlayerVelocity.y += gravityValue * Time.deltaTime;
        playerControls.Move(PlayerVelocity * Time.deltaTime);

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
            playerData.PickupItem(closestItem.GetComponent<ItemObject>().itemScript);
            Destroy(closestItem);
        }

        // use item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // get the first item in inventroy and use it
            // if the item is consumable
            var firstItem = playerData.inventory[0];
            if (firstItem is ConsumableItem)
            {
                ((ConsumableItem)firstItem).ConsumeItem();
                playerData.inventory.RemoveAt(0);
            }
        }
    }
}
