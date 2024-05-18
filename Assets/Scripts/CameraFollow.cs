using UnityEngine;

public class CameraFollow :Singleton<CameraFollow>
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);

        //transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
    }
}
