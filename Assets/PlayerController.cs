using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController PlayerControls;
    private Vector3 PlayerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    [SerializeField]
    private float playerSpeed = 4.0f;

    [SerializeField]
    private float jumpHeight = 4.0f;

    [SerializeField]
    LayerMask defaultLayer;

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
        PlayerControls = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePosition = Input.mousePosition;
        // Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Vector3 relativePosition = targetPosition - transform.position;
        // Quaternion playerRotation = Quaternion.LookRotation(relativePosition);
        // transform.rotation = playerRotation;

        // Rotate Player to Mouse on RightClick

        Vector3 mousePosition = Input.mousePosition;
        Ray mousePositionRay = Camera.main.ScreenPointToRay(mousePosition);
        Plane mousePositionPlane = new Plane(Vector3.up, Vector3.zero);
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
            transform.rotation = Quaternion.Euler(0, playerRotation, 0);
        }

        // idk wtf this is
        groundedPlayer = isGrounded();
        // print(groundedPlayer);

        if (groundedPlayer && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = 0f;
        }

        // Get horizontal and vertical player inputs

        Vector3 PlayerMovement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // Horizontal and Vertical Movement

        PlayerControls.Move(PlayerMovement * Time.deltaTime * playerSpeed);

        if (PlayerMovement != Vector3.zero)
        {
            gameObject.transform.forward = PlayerMovement;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            PlayerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        PlayerVelocity.y += gravityValue * Time.deltaTime;
        PlayerControls.Move(PlayerVelocity * Time.deltaTime);
    }
}
