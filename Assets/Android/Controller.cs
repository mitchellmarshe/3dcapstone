using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Note: This script handles EVERY single player action in this game!

public class Controller : MonoBehaviour
{
    public Text dialogue;
    public Vector2 position;

    private Global global; // Global variables.

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>(); // Shared pointer.

        position = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Haunt(false);

        if (global.haunted == true)
        {
            Decision1(false);
            Decision2(false);
            Decision3(false);
            Decision4(false);
        }
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            position = touch.position;
        }*/
        //dialogue.text = "Position: (" + position.x + ", " + position.y + ")";
        //dialogue.text = "" + global.haunted;
    }

    // Note: with a tag, enable this function and set up dialogue accordingly.
    public void Haunt(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.E) || trigger == true)
        {
            global.haunted = !global.haunted;
        }
    }

    // Note: triggerable script for the button named Decision 1.
    // When calling from Unity Editor, set trigger to true.
    // Attaching this script to button, allows the button to be touchle for Mobile platforms.
    // When using this script from another script, set trigger to false.
    public void Decision1(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || trigger == true)
        {
            global.decided = true;
            global.choice = 1;
        }
    }

    public void Decision2(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) || trigger == true)
        {
            global.decided = true;
            global.choice = 2;
        }
    }

    public void Decision3(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) || trigger == true)
        {
            global.decided = true;
            global.choice = 3;
        }
    }

    public void Decision4(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) || trigger == true)
        {
            global.decided = true;
            global.choice = 4;
        }
    }
}
