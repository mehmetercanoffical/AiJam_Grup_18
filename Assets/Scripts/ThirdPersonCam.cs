using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public CameraStyle currentStyle;
    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;

    public enum CameraStyle
    {
        Basic,
        Combat
    }
    private void Start()
    {
        //cursor visibility
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible  = false;
    }

    private void Update()
    {
        //switch style
        if (!Input.GetMouseButton(1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetMouseButton(1)) SwitchCameraStyle(CameraStyle.Combat);
        //rotate orientation
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        //rotate player obj.
        if(currentStyle == CameraStyle.Basic)
        {
            float horizantalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizantalInput;

            if(inputDirection != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }

        void SwitchCameraStyle(CameraStyle newStyle)
        {
            combatCam.SetActive(false);
            thirdPersonCam.SetActive(false);

            if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
            if (newStyle == CameraStyle.Combat) thirdPersonCam.SetActive(true);

            currentStyle = newStyle;
        }
        
    }
}
