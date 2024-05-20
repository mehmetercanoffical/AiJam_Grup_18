using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [Header("Time Point Reference")]
    private TimePoints timePointsScript; 
    
    [Header("Time Cost Reference")]
    public GameObject jumpCost_PopUp;




    private void Start()
    {
        timePointsScript = GetComponent<TimePoints>();
        jumpCost_PopUp.SetActive(false);

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpReady = true;
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isThisGround);

        MyInput();
        SpeedControl();

        //hızlanma
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (Input.GetKeyDown(jumpKey) && jumpReady && grounded)
        {   
            timePointsScript.DecreasePointsByJump(); 
            ShowJumpCostPopUp(); // Show the jump cost pop-up above the player's head
        }
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
        if (Input.GetKeyDown(jumpKey) && jumpReady && grounded)
        {
            playerAnim.SetTrigger("isJump");
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

    public void Jump()
    {
        jumpReady = false;
        GetJumpReady();
        StartCoroutine(ResetJump());
        //y ekseni 0
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        jumpReady = true;
    }

    public bool GetJumpReady()
    {
        return jumpReady;
    }

    void ShowJumpCostPopUp()
    {
        // Enable the jumpCostPopUp GameObject
        jumpCost_PopUp.gameObject.SetActive(true);
        StartCoroutine(HideJumpCostPopUp());
        
    }


    IEnumerator HideJumpCostPopUp()
    {
        // Wait for a short duration
        yield return new WaitForSeconds(1f);

        // Disable the jumpCostPopUp GameObject after the delay
        jumpCost_PopUp.gameObject.SetActive(false);
    }

}
