using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script to control Guardi

public class Guardi : MonoBehaviour
{
    [Header("Scripts")]
    public Global global;
    public GUI gui;

    [Header("UI")]
    public GameObject canvas;
    public TextMeshProUGUI text;

    [Header("Conditions")]
    public bool look;
    public bool move;
    public bool action;
    public bool decal;
    public bool softSelection;
    public bool hardSelection;
    public bool pickupObject;
    public bool throwObject;

    private bool canvasOn;
    private int index;

    private void Awake()
    {
        canvasOn = false;

        look = false;
        move = false;
        action = false;
        decal = false;
        softSelection = false;
        hardSelection = false;
        pickupObject = false;
        throwObject = false;

        index = 0;
        Dialogue();
    }

    void Start()
    {
        Appear(); // Start based on time?

        if (global.tutorial == true) {
            Dialogue();
            Invoke("Dialogue", 10);
            Invoke("Dialogue", 20);
            Invoke("Dialogue", 30);
            Invoke("Dialogue", 40);
        }
    }

    void Update()
    {
        if (softSelection == true)
        {
            Debug.Log("soft");
        }

        if (hardSelection == true)
        {
            Debug.Log("hard");
        }

        if (pickupObject == true)
        {
            Debug.Log("pickup");
        }

        if (throwObject == true)
        {
            Debug.Log("throw");
        }

        if (action == true)
        {
            Debug.Log("action");
        }

        if (look == true)
        {
            Debug.Log("look");
        }

        if (move == true)
        {
            Debug.Log("move");
        }

        if (decal == true)
        {
            Debug.Log("decal");
        }
    }

    public void TutorialYes()
    {
        gui.ShowTutorial(false);
        global.tutorial = false;
        Disappear();
    }

    public void TutorialNo()
    {
        gui.ShowTutorial(false);
    }

    public void Appear()
    {
        this.gameObject.SetActive(true);
        gui.ShowTutorial(true);
        gui.ShowLookTutorial();
        Invoke("TutorialNo", 10);
    }

    public void Disappear()
    {
        this.gameObject.SetActive(false);
        gui.ShowActions(true);
        gui.ShowDecals(true);
        
        if (gui.lookTutorialOn == false)
        {
            gui.ShowLookTutorial();
        }
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
