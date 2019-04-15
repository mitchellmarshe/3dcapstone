using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script to control Guardi

public class Guardi : MonoBehaviour
{
    [Header("Scripts")]

    [Header("UI")]
    public GameObject canvas;
    public TextMeshProUGUI text;

    private bool canvasOn;
    private int index;

    private void Awake()
    {
        canvasOn = false;
        index = 0;
        Dialogue();
    }

    void Start()
    {
        Dialogue();
        Invoke("Dialogue", 10);
        Invoke("Dialogue", 20);
        Invoke("Dialogue", 30);
        Invoke("Dialogue", 40);
    }

    void Update()
    {

    }

    public void ShowCanvas()
    {
        canvasOn = !canvasOn;
        canvas.SetActive(canvasOn);
    }

    public void Dialogue()
    {
        if (index == 0)
        {
            text.text = "";
        }

        if (index == 1)
        {
            text.text = "Whoa, you just fell through a wall! I'm pretty sure you hit your head on that toilet.";
        }

        if (index == 2)
        {
            text.text = "You didn't feel that cause you're now a spirit.";
        }

        if (index == 3)
        {
            text.text = "Did they pull the plug on you too?";
        }

        if (index == 4)
        {
            text.text = "Well, let's get revenge by haunting the staff workers of this morgue.";
        }

        if (index == 5)
        {
            text.text = "I'll show you the basics to haunting.";
        }

        if (index == 6)
        {
            text.text = "I'll show you the basics to haunting.";
        }

        index++;
    }
}
