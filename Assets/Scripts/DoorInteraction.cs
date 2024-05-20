using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public TMP_Text interactionText; // Reference to the UI text element
    private bool playerInRange = false;

    private void Start()
    {
        interactionText.gameObject.SetActive(false); // Initially hide the interaction text
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player has collected both keys
            int keysCollected = PlayerPrefs.GetInt("Key", 0);
            if (keysCollected >= 2)
            {
                // Transition to the YouWin scene
                SceneManager.LoadScene("YouWin");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionText.gameObject.SetActive(true); // Show the interaction text
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionText.gameObject.SetActive(false); // Hide the interaction text
        }
    }
}