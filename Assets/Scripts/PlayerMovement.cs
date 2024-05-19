using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float RotationCamera = 5f;
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    private bool jumpReady;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isThisGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;
    public Animator playerAnim;


    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpReady = true;
    }
    private void Update()
    {
        //Debug.Log(grounded);
        //zemin var mı
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isThisGround);

        MyInput();
        SpeedControl();

        //hızlanma
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //ne zaman zıplanır
        if (Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            Debug.Log("isJumping");
            Jump();
            jumpReady = false;
            StartCoroutine(ResetJump());
            //Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //Hareket yönü
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        RotationCharackterByCamera();
        if (verticalInput != 0 || horizontalInput != 0)
            playerAnim.SetBool("isRunning", true);
        else
            playerAnim.SetBool("isRunning", false);

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
        }
    }

    public void RotationCharackterByCamera()
    {

        float horizontal = Input.GetAxis("Mouse X");
        Quaternion quaternion = Quaternion.AngleAxis(horizontal * RotationCamera, Vector3.up);
        transform.rotation = orientation.transform.rotation * quaternion;
        //transform.Rotate(Vector3.up * horizontal * RotationCamera);

    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //hızlanmayı limitle
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }

    }

    private void Jump()
    {
        //y ekseni 0
        playerAnim.SetTrigger("isJump");
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        jumpReady = true;
        //playerAnim.SetBool("isJumping", false);

        ResetJump();
    }
}
