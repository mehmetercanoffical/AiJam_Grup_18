using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class TimePoints : MonoBehaviour
{
    public float timePoints = 180f;
    public TMP_Text timePointsText;

    private bool isCountingDown = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecreaseTimePoints());
    }

    // Update is called once per frame
    void Update()
    {
        if (timePointsText != null)
        {
            timePointsText.text = "Time Points " + Mathf.Ceil(timePoints).ToString();
        }
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
    }

    void OnTimePointsDepleted()
    {
        // Handle what happens when time points are depleted
        Debug.Log("Time points depleted!");
        isCountingDown = false;

        // You can add additional actions here, like ending the game or triggering an event
    }
}
