using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalTravel : MonoBehaviour
{
    public string sceneNameToLoad; // The name of the scene to load on collision
    public string targetTag; // The tag of the object that should trigger the scene load

    // Method to handle trigger collision and load the specified scene
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the target tag
        if (other.CompareTag(targetTag))
        {
            // Check if the scene name is valid
            if (!string.IsNullOrEmpty(sceneNameToLoad) && Application.CanStreamedLevelBeLoaded(sceneNameToLoad))
            {
                SceneManager.LoadScene(sceneNameToLoad);
            }
            else
            {
                Debug.LogError("Scene " + sceneNameToLoad + " cannot be found in the build settings.");
            }
        }
    }
}
