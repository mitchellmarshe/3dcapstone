using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float maxTime; // seconds.

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = maxTime - Time.time;

        int minutes = (int) timeLeft / 60;
        minutes = Mathf.Max(0, minutes);

        int seconds = (int) timeLeft % 60;
        if (minutes <= 0)
        {
            seconds = Mathf.Max(0, seconds);
        }

        string timeFormat = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = timeFormat;

        if (minutes <= 0 && seconds <= 0)
        {
            // Trigger Ending!
        }
    }
}
