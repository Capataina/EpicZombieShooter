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

    // Start is called before the first frame update
    void Start()
    {
        PlayerControls = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = isGrounded();

        print(groundedPlayer);

        if (groundedPlayer && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = 0f;
        }

        Vector3 PlayerMovement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

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
