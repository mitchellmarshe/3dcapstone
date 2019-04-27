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

    [Header("Objects")]
    public GameObject[] objects;

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
    private float startTime;
    private float time;

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
        startTime = 14.0f;
    }

    void Start()
    {
        time = Time.time + startTime;
        Invoke("Appear", startTime);
    }

    void Update()
    {
        if (global.tutorial == false)
        {
            Disappear();
            return;
        }

        Dialogue();

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
        gui.ShowDecals(true);
        move = true;
        
        if (gui.lookTutorialOn == false)
        {
            gui.ShowLookTutorial();
        }
    }

    public void Objects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }

    public void ShowCanvas()
    {
        canvasOn = !canvasOn;
        canvas.SetActive(canvasOn);
    }

    public void Dialogue()
    {
        // Introduction
        if (index == 0 && Time.time >= time)
        {
            text.text = "Whoa, you just fell through a wall! I'm pretty sure you hit your head on that toilet.";
            index++;
            time += 10.0f;

            softSelection = false;
        }

        if (index == 1 && Time.time >= time)
        {
            text.text = "You didn't feel that cause you're now a spirit.";
            index++;
            time += 10.0f;

            softSelection = false;
        }

        if (index == 2 && Time.time >= time)
        {
            text.text = "Did they pull the plug on you too?";
            index++;
            time += 10.0f;

            softSelection = false;
        }

        if (index == 3 && Time.time >= time)
        {
            text.text = "Well, let's get revenge by haunting the staff workers of this morgue.";
            index++;
            time += 10.0f;

            softSelection = false;
        }

        if (index == 4 && Time.time >= time)
        {
            text.text = "I'll show you the basics to haunting.";
            index++;
            time += 10.0f;

            softSelection = false;
        }

        // Soft Selection
        if (index == 5 && Time.time >= time)
        {
            text.text = "Here are some soda cans, we can manipulate.";
            Objects();
            index++;
            time += 10.0f;

            softSelection = false;
        }

        if (index == 6 && Time.time >= time)
        {
            if (softSelection == false)
            {
                text.text = "Hover your hand (mouse pointer) over a can. The can will glow.";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;

                action = false;
            }
        }
        
        // Actions
        if (index == 7 && Time.time >= time)
        {
            if (action == false)
            {
                text.text = "Now move this can by haunting it (press 1).";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;

                action = false;
            }
        }

        if (index == 8 && Time.time >= time)
        {
            if (action == false)
            {
                text.text = "Hover over another can, then destroy it (press 2).";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;

                hardSelection = false;
            }
        }

        // Hard Selection
        if (index == 9 && Time.time >= time)
        {
            text.text = "You're getting the hang of this!";
            index++;
            time += 10.0f;

            hardSelection = false;
        }

        if (index == 10 && Time.time >= time)
        {
            if (hardSelection == false)
            {
                text.text = "Instead of just hovering over a can, concentrate on it (left-mouse click).";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;

                pickupObject = false;
            }
        }

        if (index == 11 && Time.time >= time)
        {
            text.text = "When you concentrate on an object, it allows you manipulate it selectively.";
            index++;
            time += 10.0f;

            pickupObject = false;
        }

        // Pickup Object
        if (index == 12 && Time.time >= time)
        {
            text.text = "Also when concentrating on an object, you can do more powerful haunting tactics like picking up the object.";
            index++;
            time += 10.0f;

            pickupObject = false;
        }

        if (index == 13 && Time.time >= time)
        {
            if (pickupObject == false)
            {
                text.text = "Try picking up a soda can (left-mouse click)!";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;

                throwObject = false;
            }
        }

        // Throwing Object
        if (index == 14 && Time.time >= time)
        {
            if (throwObject == false)
            {
                text.text = "Throw the soda can (left-mouse click).";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;

                decal = false;
            }
        }

        if (index == 15 && Time.time >= time)
        {
            text.text = "Remember objects can be manipulated in various ways!";
            index++;
            time += 10.0f;

            decal = false;
        }

        // Decals
        if (index == 16 && Time.time >= time)
        {
            text.text = "Here's one final thing you can do as a ghost.";
            gui.ShowDecals(true);
            index++;
            time += 10.0f;

            decal = false;
        }

        if (index == 17 && Time.time >= time)
        {
            text.text = "You can make art on the walls.";
            index++;
            time += 10.0f;

            decal = false;
        }

        if (index == 18 && Time.time >= time)
        {
            if (decal == false)
            {
                text.text = "Just drag (left-mouse click) a decal onto the wall.";
                time = (Time.time - time);
            }
            else
            {
                index++;
                time += 10.0f;
            }
        }

        if (index == 19 && Time.time >= time)
        {
            text.text = "These decals really get the attention of people.";
            index++;
            time += 10.0f;
        }

        // Moving
        if (index == 20 && Time.time >= time)
        {
            text.text = "You're free to haunt the morgue staff!";
            Invoke("Disappear", 10);
        }

        /*
        // Waiting
        if (index == 21)
        {
            text.text = "You okay?";
        }

        if (index == 22)
        {
            text.text = "Why aren't you learning?";
        }

        if (index == 23)
        {
            text.text = "Just do it!";
        }

        if (index == 24)
        {
            text.text = "I'm getting bored here.";
        }

        if (index == 25)
        {
            text.text = "You're on your own here.";
        }
        */
        //index++;
    }
}
