using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HourglassPickup : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the rotation speed
    public float timeToAdd = 30f;
    private void Update()
    {
        // Rotate the pickup around its y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
