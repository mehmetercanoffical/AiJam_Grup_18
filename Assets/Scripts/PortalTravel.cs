using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.UI;
using TMPro;


public class PortalTravel : MonoBehaviour
{
    public string sceneNameToLoad; // The name of the scene to load on collision
    public string targetTag; // The tag of the object that should trigger the scene load
    public TMP_Text uiText; // Reference to the UI Text component
    public GameObject portalPopUp;

    private bool isPlayerInTrigger = false; // Flag to check if player is in the trigger zone

    // Method to handle entering the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the target tag
        if (other.CompareTag(targetTag))
        {
            isPlayerInTrigger = true;
            portalPopUp.SetActive(true);
        }
    }

    // Method to handle exiting the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the collided object has the target tag
        if (other.CompareTag(targetTag))
        {
            isPlayerInTrigger = false;
            portalPopUp.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player is in the trigger zone and the "E" key is pressed
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Check if the scene name is valid
            if (!string.IsNullOrEmpty(sceneNameToLoad) && Application.CanStreamedLevelBeLoaded(sceneNameToLoad))
            {
                // Save data to PlayerPrefs before changing the scene
                SaveData();

                SceneManager.LoadScene(sceneNameToLoad);
            }
            else
            {
                Debug.LogError("Scene " + sceneNameToLoad + " cannot be found in the build settings.");
            }
        }
    }

    // Method to save necessary data to PlayerPrefs
    private void SaveData()
    {
        //reference to the TimePoints script attached to the Player object
        GameObject playerObject = GameObject.FindWithTag(targetTag);
        if (playerObject != null)
        {
            TimePoints timePointsScript = playerObject.GetComponent<TimePoints>();
            if (timePointsScript != null)
            {
                // Save the timePoints value to PlayerPrefs
                PlayerPrefs.SetFloat("TimePoints", timePointsScript.timePoints);
                PlayerPrefs.Save();
            }
        }
    }
}
