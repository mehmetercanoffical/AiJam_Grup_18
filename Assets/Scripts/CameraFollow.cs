using System.Collections;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform Player;
    public float smoothSpeed = 0.125f;
    public float speed = 0.125f;

    public Vector3 MainCameraOffset;
    public Vector3 CombatCameraOffset;

    public Transform MainCamera;
    public Transform CombatCamera;

    public CameraStyle currentStyle;

    public float mouseSensitivity;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float distance = 0f;
    public float verticalOffset = 0f;
    private Transform _target;
    private bool isCompletedMovement;

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
        xRotation = Mathf.Clamp(xRotation, -35f, 60f); // Dikey dönüþ açýsýný sýnýrla

        yRotation += mouseX;

        // Kamerayý oyuncunun etrafýnda döndür
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        //// Kamerayý oyuncunun arkasýna ve yukarýsýna yerleþtir
        //Vector3 targetPosition = Player.position - transform.forward * distance + Vector3.up * verticalOffset;
        //transform.position = targetPosition;

        // Oyuncunun yönünü kameranýn yönüne hizala (isteðe baðlý)        Player.rotation = Quaternion.Euler(0f, yRotation, 0f);


        if (currentStyle == CameraStyle.Basic)
        {

            Vector3 desiredPosition = Player.position + MainCameraOffset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

        }
        if (currentStyle == CameraStyle.Combat)
        {
            Vector3 desiredPosition = Player.position + CombatCameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

        if (Input.GetMouseButton(1)) SwitchCameraStyle(CameraStyle.Combat);
        else SwitchCameraStyle(CameraStyle.Basic);

    }

    void SwitchCameraStyle(CameraStyle newStyle)
    {
        currentStyle = newStyle;
        if (currentStyle == CameraStyle.Basic) StartCoroutine(ChangeController(MainCamera, (speed / 10000)));
        else StartCoroutine(ChangeController(CombatCamera, speed));
    }

    IEnumerator ChangeController(Transform position, float spd)
    {
        _target = position;
        while (!isCompletedMovement)
        {
            transform.position = Vector3.Slerp(transform.position, _target.position, spd * Time.deltaTime);

            if (Vector3.Distance(_target.position, transform.position) <= 1f) isCompletedMovement = true;
            yield return null;
        }

    }
}
