using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimePoints : MonoBehaviour
{
    public float health = 180f;
    public float timePoints;
    public TMP_Text timePointsText;

    public float jumpCost = 5f;

    private bool isCountingDown = true;
    
    // Reference to the Slider componenor the health bar
    public Slider healthSlider;

    // Key for saving time points in PlayerPrefs
    private const string TimePointsKey = "TimePoints";

    [Header("Key Stuff")]
    int keyIndex = 0;
    string keyName = "Key";
    public GameObject[] keyImages;
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health; 
        healthSlider.value = health;
    }
    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
    
    void Start()
    {
        timePoints = PlayerPrefs.GetFloat(TimePointsKey, health);
        keyIndex = PlayerPrefs.GetInt("Key", -1);
        //timePoints = health;
        SetMaxHealth(health);
        SetHealth(timePoints);
        StartCoroutine(DecreaseTimePoints());
    }

    // Update is called once per frame
    void Update()
    {
        if (timePointsText != null)
        {
            timePointsText.text = Mathf.Ceil(timePoints).ToString();
        }
        SetHealth(timePoints);
    }
    

    IEnumerator DecreaseTimePoints()
    {
        while (isCountingDown && timePoints > 0)
        {
            // Decrease time points every second
            yield return new WaitForSeconds(1f);
            timePoints -= 1f;

            // Save the updated time points
            PlayerPrefs.SetFloat(TimePointsKey, timePoints);
            PlayerPrefs.Save();

            // Check if time points have reached zero
            if (timePoints <= 0)
            {
                timePoints = 0;
                OnTimePointsDepleted();
            }
        }
        SetHealth(timePoints);
    }
    public void DecreasePointsByJump(int cost)
    {
        // Decrease points by 5
        timePoints -= cost;

        // Ensure timePoints doesn't go below 0
        if (timePoints < 0)
        {
            timePoints = 0;
        }

        // Update UI text
        if (timePointsText != null)
        {
            timePointsText.text = Mathf.Ceil(timePoints).ToString();
        }
        SetHealth(timePoints);

        // Save the updated time points
        SaveTimePoints();
    }

    public void AddTime(float timeToAdd)
    {
        Debug.Log("puan eklendi.");
        timePoints += timeToAdd;
        SaveTimePoints();
    }

    void SaveTimePoints()
    {
        // Save time points to PlayerPrefs
        PlayerPrefs.SetFloat(TimePointsKey, timePoints);
        PlayerPrefs.Save();
    }

    void OnTimePointsDepleted()
    {
        // Handle what happens when time points are depleted
        Debug.Log("Time points depleted!");
        isCountingDown = false;

        // You can add additional actions here, like ending the game or triggering an event
        // Load the Game Over scene
        SceneManager.LoadScene("gameOver");
    }

    private void OnApplicationQuit()
    {
        // Save time points when the application quits
        SaveTimePoints();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
                HourglassPickup pickup = other.GetComponent<HourglassPickup>();
                Debug.Log("Adding time to TimePoints.");
                float timeToAdd = pickup.timeToAdd;
                AddTime(timeToAdd);

                // Destroy the hourglass pickup object
                Destroy(other.gameObject);
            
        }

        if (other.CompareTag("Key"))
        {
            keyIndex++;
            if (keyIndex < keyImages.Length)
                keyImages[keyIndex].SetActive(true);

            PlayerPrefs.SetInt(keyName, keyIndex);
            PlayerPrefs.Save();
            Destroy(other.gameObject);
        }
    }
}
