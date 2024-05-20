using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : Singleton<TimeController>
{
    // Start is called before the first frame update

    public float timePoints;
    public TMP_Text timePointsText;

    public float jumpCost = 5f;

    private bool isCountingDown = true;

    public Slider healthSlider;

    private const string TimePointsKey = "TimePoints";

    [Header("Key Stuff")]
    int keyIndex = 0;
    string keyName = "Key";
    public GameObject[] keyImages;


    void Start()
    {
        if (timePointsText != null && healthSlider != null)
            StartCoroutine(DecreaseTimePoints());
    }

    void Update()
    {
        if (timePointsText != null && healthSlider != null)
        {
            if (timePointsText != null)
                timePointsText.text = Mathf.Ceil(timePoints).ToString();

            SetHealth(timePoints);
        }

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
    }

    void OnTimePointsDepleted()
    {
        isCountingDown = false;
        SceneManager.LoadScene("gameOver");
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Pickup"))
        //{
        //    HourglassPickup pickup = other.GetComponent<HourglassPickup>();
        //    float timeToAdd = pickup.timeToAdd;
        //    AddTime(timeToAdd);

        //    // Destroy the hourglass pickup object
        //    Destroy(other.gameObject);

        //}

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

    public void AddTime(float timeToAdd)
    {
        timePoints += timeToAdd;
    }
}
