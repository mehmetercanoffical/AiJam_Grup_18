using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator playerAnim;
    private PlayerMovement playerScript;
    private Rigidbody playerRb;
    public float jumpForce = 2.0f;
    private bool isGrounded;

    void Start()
    {   
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void GetGroundInfo()
    {
        isGrounded = playerScript.grounded;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("isThisGround"))
        { 
            isGrounded = true;
        }
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        playerAnim.SetBool("isJumping", true);
    }
}
