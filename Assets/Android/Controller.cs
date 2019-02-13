using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Note: This script handles EVERY single player action in this game!

public class Controller : MonoBehaviour
{
    public Text dialogue;
    public Vector2 position;

    public Vector2 startPosition;
    public Vector2 deltaPosition;
    public Vector2 endPosition;

    public GameObject camera;

    private Global global; // Global variables.

    private float x;
    private float z;
    private bool moved;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>(); // Shared pointer.
        camera = GameObject.Find("Camera");
        position = new Vector2(0.0f, 0.0f);

        moved = false;
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
        
        // TESTING touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                deltaPosition = touch.deltaPosition;
                //float x;
                if (deltaPosition.x < 0)
                {
                    x = -1.0f;
                }
                else if (deltaPosition.x == 0)
                {
                    x = 0.0f;
                }
                else
                {
                    x = 1.0f;
                }

                //float z;
                if (deltaPosition.y < 0)
                {
                    z = -1.0f;
                }
                else if (deltaPosition.y == 0)
                {
                    z = 0.0f;
                }
                else
                {
                    z = 1.0f;
                }

                camera.transform.position += new Vector3(x, 0.0f, z);
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                camera.transform.position += new Vector3(x, 0.0f, z);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Canceled)
            {

            }
            //position = touch.position;
        }
        //dialogue.text = "Position: (" + position.x + ", " + position.y + ")";
        //dialogue.text = "" + global.haunted;

        dialogue.text = 
            "S(" + startPosition.x + ", " + startPosition.y + 
            ") D(" + deltaPosition.x + ", " + deltaPosition.y + 
            ") E(" + endPosition.x + ", " + endPosition.y + ")";
        /*
        float x;
        if (deltaPosition.x < 0)
        {
            x = -1.0f;
        }
        else if (deltaPosition.x == 0)
        {
            x = 0.0f;
        }
        else
        {
            x = 1.0f;
        }

        float z;
        if (deltaPosition.y < 0)
        {
            z = -1.0f;
        }
        else if (deltaPosition.y == 0)
        {
            z = 0.0f;
        }
        else
        {
            z = 1.0f;
        }

        camera.transform.position += new Vector3(x, 0.0f, z);*/
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
