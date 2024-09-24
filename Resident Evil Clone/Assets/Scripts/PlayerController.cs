using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float verticalLookLimit;
    private bool isGrounded = true;
    private float xRotation;

    [SerializeField] Transform fpsCamera;
    private Rigidbody rb;

    [SerializeField] private Weapon equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        lookAround();
        movePlayer();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            equippedWeapon.TryFire();
        }
    }

    void lookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void movePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.Normalize();
        Vector3 moveVelocity = move * moveSpeed;

        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
