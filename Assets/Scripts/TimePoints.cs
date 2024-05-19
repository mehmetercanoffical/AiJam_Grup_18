using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimePoints : MonoBehaviour
{
    public float health = 180f;
    public float timePoints;
    public TMP_Text timePointsText;

    public float jumpCost = 5f;

    private bool isCountingDown = true;
    
    // Reference to the Slider component for the health bar
    public Slider healthSlider;

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
        timePoints = health;
        SetMaxHealth(health);
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

            // Check if time points have reached zero
            if (timePoints <= 0)
            {
                timePoints = 0;
                OnTimePointsDepleted();
            }
        }
        SetHealth(timePoints);

    }
    public void DecreasePointsByJump()
    {
        // Decrease points by 5
        timePoints -= jumpCost;

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
    }


    void OnTimePointsDepleted()
    {
        // Handle what happens when time points are depleted
        Debug.Log("Time points depleted!");
        isCountingDown = false;

        // You can add additional actions here, like ending the game or triggering an event
    }
}
