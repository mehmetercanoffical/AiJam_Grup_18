using System.Collections;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour 
{
    public Transform Player;
    public float smoothSpeed = 0.125f;
    public float switchSpeed = 2f;

    public Vector3 MainCameraOffset;
    public Vector3 CombatCameraOffset;

    public Transform MainCamera;
    public Transform CombatCamera;

    public CameraStyle currentStyle;

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private Transform targetCamera;

    public enum CameraStyle
    {
        Basic,
        Combat
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void LateUpdate()
    {
        // Rotate Camera with Mouse X and Y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 60f); // Clamp the vertical rotation angle

        yRotation += mouseX;

        // Rotate the camera around the player
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Position the camera behind the player
        Vector3 desiredPosition = Player.position + transform.rotation * (currentStyle == CameraStyle.Basic ? MainCameraOffset : CombatCameraOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Align the player's direction with the camera's direction
        Player.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Switch between camera styles on right mouse button
        if (Input.GetMouseButton(1))
        {
            SwitchCameraStyle(CameraStyle.Combat);
        }
        else
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }
    }

    void SwitchCameraStyle(CameraStyle newStyle)
    {
        if (currentStyle != newStyle)
        {
            currentStyle = newStyle;
            StartCoroutine(ChangeController(currentStyle == CameraStyle.Basic ? MainCamera : CombatCamera, switchSpeed));
        }
    }

    IEnumerator ChangeController(Transform target, float speed)
    {
        targetCamera = target;
        Vector3 startPos = transform.position;
        Vector3 endPos = target.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        transform.position = endPos;
    }
}