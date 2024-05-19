using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    private Animator playerAnim;


    private void Start()
    {
        playerAnim = GetComponent<Animator>();
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
        if(grounded)
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
        if(Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            Debug.Log("isJumping");
            jumpReady = false;
            playerAnim.SetBool("isJumping", true);
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //Hareket yönü
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //hızlanmayı limitle
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        //y ekseni 0
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
    }

    private void ResetJump()
    {
        playerAnim.SetBool("isJumping", false);
        jumpReady = true;
    }
}
