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
    public GameObject portrait;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audioClips;

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
    private float waitTime;
    private bool waited;
    private bool clicked;
    private bool sound;

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
        waitTime = 0.0f;
        waited = false;
        clicked = false;
        sound = false;
    }

    void Start()
    {
        time = Time.time + startTime;
        Invoke("Appear", startTime);
        ShowCanvas();
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

        if (gui.clickTutorialOn == false)
        {
            gui.ShowClickTutorial();
        }

        if (gui.moveTutorialOn == false)
        {
            gui.ShowMoveTutorial();
        }
    }

    public void Objects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }

    public void Portrait(bool trigger)
    {
        portrait.SetActive(trigger);
    }

    public void ShowCanvas()
    {
        canvas.SetActive(canvasOn);
        canvasOn = !canvasOn;
    }

    public void Dialogue()
    {
        // Introduction
        if (index == 0 && Time.time >= time)
        {
            ShowCanvas();
            text.text = "Whoa, you just fell through a wall! I'm pretty sure you hit your head on that toilet.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            softSelection = false;
        }

        if (index == 1 && Time.time >= time)
        {
            text.text = "You didn't feel that cause you're now a spirit.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            softSelection = false;
        }

        if (index == 2 && Time.time >= time)
        {
            text.text = "Did they pull the plug on you too?";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            softSelection = false;
        }

        if (index == 3 && Time.time >= time)
        {
            text.text = "Well, let's get revenge by haunting the staff workers of this morgue.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            softSelection = false;
        }

        if (index == 4 && Time.time >= time)
        {
            text.text = "I'll show you the basics to haunting.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            softSelection = false;
        }

        // Soft Selection
        if (index == 5 && Time.time >= time)
        {
            text.text = "Here is a portrait, we can manipulate.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            Portrait(true);
            index++;
            time += audioClips[index].length + 1.0f;
            softSelection = false;
        }

        if (index == 6 && Time.time >= time)
        {
            float length = 0.0f;

            if (softSelection == false)
            {
                if (waitTime >= 15.0f)
                {
                    Waiting();
                }
                else
                {
                    if (global.platform == false)
                    {
                        text.text = "Hover your hand (mouse pointer) over the portrait. The portrait will glow.";
                    }
                    else
                    {
                        text.text = "Hover your hand (finger select) over the portrait. The portrait will glow.";
                    }

                    if (sound == false)
                    {
                        audioSource.clip = audioClips[index];
                        length = audioClips[index].length + 1.0f;
                        audioSource.Play();
                        sound = true;
                    }
                    
                }

                waitTime = Time.time - time;
            }
            else
            {
                index++;

                if (waitTime <= length)
                {
                    time += length - waitTime;
                }
                else
                {
                    time += waitTime;
                }

                waitTime = 0.0f;
                waited = false;
                action = false;
                sound = false;
            }
        }
        
        // Actions
        if (index == 7 && Time.time >= time)
        {
            float length = 0.0f;

            if (action == false)
            {
                if (waitTime >= 15.0f)
                {
                    Waiting();
                }
                else
                {
                    if (global.platform == false)
                    {
                        text.text = "Now haunt the portrait (press 1).";
                    }
                    else
                    {
                        text.text = "Now haunt the portrait (press ghost icon).";
                    }

                    if (sound == false)
                    {
                        audioSource.clip = audioClips[index];
                        length = audioClips[index].length + 1.0f;
                        audioSource.Play();
                        sound = true;
                    }
                }

                waitTime = Time.time - time;
            }
            else
            {
                index++;

                if (waitTime <= length)
                {
                    time += length - waitTime;
                }
                else
                {
                    time += waitTime;
                }

                waitTime = 0.0f;
                waited = false;
                hardSelection = false;
                sound = false;
            }
        }

        // Hard Selection
        if (index == 8 && Time.time >= time)
        {
            text.text = "You're getting the hang of this!";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            hardSelection = false;
        }

        if (global.platform == false)
        {
            if (index == 9 && Time.time >= time)
            {
                float length = 0.0f;

                if (hardSelection == false)
                {
                    if (waitTime >= 15.0f)
                    {
                        Waiting();
                    }
                    else
                    {
                        text.text = "Instead of just hovering over the portrait, concentrate on it (left-mouse click).";

                        if (sound == false)
                        {
                            audioSource.clip = audioClips[index];
                            length = audioClips[index].length + 1.0f;
                            audioSource.Play();
                            sound = true;
                        }

                        if (clicked == false)
                        {
                            gui.ShowClickTutorial();
                            clicked = true;
                        }
                    }

                    waitTime = Time.time - time;
                }
                else
                {
                    index++;

                    if (waitTime <= length)
                    {
                        time += length - waitTime;
                    }
                    else
                    {
                        time += waitTime;
                    }

                    waitTime = 0.0f;
                    waited = false;
                    pickupObject = false;
                    gui.ShowClickTutorial();
                    clicked = false;
                    sound = false;
                }
            }

            if (index == 10 && Time.time >= time)
            {
                text.text = "When you concentrate on an object, it allows you to manipulate it selectively.";
                audioSource.clip = audioClips[index];
                audioSource.Play();
                index++;
                time += audioClips[index].length + 1.0f;
                pickupObject = false;
            }
        }
        else
        {
            if (index == 9)
            {
                index = 11;
            }
        }

        // Pickup Object
        if (index == 11 && Time.time >= time)
        {
            text.text = "Other objects like these soda cans, can be picked up and also manipulated.";
            Objects();
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            pickupObject = false;
        }

        if (index == 12 && Time.time >= time)
        {
            float length = 0.0f;

            if (pickupObject == false)
            {
                if (waitTime >= 15.0f)
                {
                    Waiting();
                }
                else
                {
                    if (global.platform == false)
                    {
                        text.text = "Try picking up a soda can (left-mouse click)!";
                    }
                    else
                    {
                        text.text = "Try picking up a soda can (finger select)!";
                    }

                    if (sound == false)
                    {
                        audioSource.clip = audioClips[index];
                        length = audioClips[index].length + 1.0f;
                        audioSource.Play();
                        sound = true;
                    }

                    if (clicked == false)
                    {
                        gui.ShowClickTutorial();
                        clicked = true;
                    }
                }

                waitTime = Time.time - time;
            }
            else
            {
                index++;

                if (waitTime <= length)
                {
                    time += length - waitTime;
                }
                else
                {
                    time += waitTime;
                }

                waitTime = 0.0f;
                waited = false;
                throwObject = false;
                gui.ShowClickTutorial();
                clicked = false;
                sound = false;
            }
        }

        // Throwing Object
        if (index == 13 && Time.time >= time)
        {
            float length = 0.0f;

            if (throwObject == false)
            {
                if (waitTime >= 15.0f)
                {
                    Waiting();
                }
                else
                {
                    if (global.platform == false)
                    {
                        text.text = "Throw the soda can (left-mouse click).";
                    }
                    else
                    {
                        text.text = "Throw the soda can (tap finger elsewhere).";
                    }

                    if (sound == false)
                    {
                        audioSource.clip = audioClips[index];
                        length = audioClips[index].length + 1.0f;
                        audioSource.Play();
                        sound = true;
                    }

                    if (clicked == false)
                    {
                        gui.ShowClickTutorial();
                        clicked = true;
                    }
                }

                waitTime = Time.time - time;
            }
            else
            {
                index++;

                if (waitTime <= length)
                {
                    time += length - waitTime;
                }
                else
                {
                    time += waitTime;
                }

                waitTime = 0.0f;
                waited = false;
                decal = false;
                gui.ShowClickTutorial();
                clicked = false;
                sound = false;
            }
        }

        if (index == 14 && Time.time >= time)
        {
            text.text = "Remember objects can be manipulated in various ways!";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            decal = false;
        }

        // Decals
        if (index == 15 && Time.time >= time)
        {
            text.text = "Here's one final thing you can do as a ghost.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            gui.ShowDecals(true);
            index++;
            time += audioClips[index].length + 1.0f;
            decal = false;
        }

        if (index == 16 && Time.time >= time)
        {
            text.text = "You can make art on the walls.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
            decal = false;
        }

        if (index == 17 && Time.time >= time)
        {
            float length = 0.0f;

            if (decal == false)
            {
                if (waitTime >= 15.0f)
                {
                    Waiting();
                }
                else
                {
                    if (global.platform == false)
                    {
                        text.text = "Just drag (left-mouse click) a decal onto the wall.";
                    }
                    else
                    {
                        text.text = "Just drag (finger select + drag) a decal onto the wall.";
                    }

                    if (sound == false)
                    {
                        audioSource.clip = audioClips[index];
                        length = audioClips[index].length + 1.0f;
                        audioSource.Play();
                        sound = true;
                    }

                    if (clicked == false)
                    {
                        gui.ShowClickTutorial();
                        clicked = true;
                    }
                }

                waitTime = Time.time - time;
            }
            else
            {
                index++;

                if (waitTime <= length)
                {
                    time += length - waitTime;
                }
                else
                {
                    time += waitTime;
                }

                waitTime = 0.0f;
                waited = false;
                gui.ShowClickTutorial();
                clicked = false;
                sound = false;
            }
        }

        if (index == 18 && Time.time >= time)
        {
            text.text = "These decals really get the attention of people.";
            audioSource.clip = audioClips[index];
            audioSource.Play();
            index++;
            time += audioClips[index].length + 1.0f;
        }

        // Moving
        if (index == 19 && Time.time >= time)
        {
            text.text = "You're free to haunt the morgue staff!";

            audioSource.clip = audioClips[index];
            audioSource.Play();

            if (clicked == false)
            {
                gui.ShowMoveTutorial();
                clicked = true;
            }

            Invoke("Disappear", audioClips[index].length + 1.0f);
        }
    }

    public void Waiting()
    {
        if (waitTime >= 25.0f)
        {
            text.text = "You're on your own here.";
            audioSource.clip = audioClips[20];
            audioSource.Play();
            Invoke("Disappear", audioClips[20].length + 1.0f);
            return;
        }

        if (waitTime >= 20.0f)
        {
            text.text = "I'm getting bored here.";
            audioSource.clip = audioClips[21];
            audioSource.Play();
            return;
        }

        if (waited == false)
        {
            float number = Random.Range(0.0f, 3.0f);

            if (number >= 0.0f && number < 1.0f)
            {
                text.text = "Just do it!";
                audioSource.clip = audioClips[22];
                audioSource.Play();
            }
            else if (number >= 1.0f && number < 2.0f)
            {
                text.text = "Why aren't you learning?";
                audioSource.clip = audioClips[23];
                audioSource.Play();
            }
            else
            {
                text.text = "You okay?";
                audioSource.clip = audioClips[24];
                audioSource.Play();
            }

            waited = true;
        }

        return;
    }
}
