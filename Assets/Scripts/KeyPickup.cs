using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the rotation speed
    
    private void Update()
    {
        // Rotate the pickup around its y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
