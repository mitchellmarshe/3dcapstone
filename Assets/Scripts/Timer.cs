using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float maxTime; // seconds.
    public AudioClip bellSound;
    public AudioSource source;
    public float volume;
    public GameObject ending;
    public GameObject cremated;

    private int rings;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "00:00";
        rings = 0;
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
        /*
        if (minutes < 10 && rings == 0)
        {
            source.PlayOneShot(bellSound, volume);
            rings++;
        }

        if (minutes < 5 && rings == 1)
        {
            source.PlayOneShot(bellSound, volume);
            rings++;
        }

        if (minutes <= 0 && seconds <= 0 && rings == 2)
        {
            source.PlayOneShot(bellSound, volume);
            rings++;
            ending.SetActive(true);
            cremated.SetActive(true);
        }
        */
    }
}
