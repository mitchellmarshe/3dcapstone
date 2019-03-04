using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject knob;
    private GameObject drawer;
    private GameObject options;
    private GameObject controls;

    private bool inMenu;
    private bool inOptions;
    private bool inControls;

    private void Awake()
    {
        knob = GameObject.Find("GUI/Menu/Knob") as GameObject;
        drawer = GameObject.Find("GUI/Menu/Drawer") as GameObject;
        options = GameObject.Find("GUI/Menu/Options") as GameObject;
        controls = GameObject.Find("GUI/Menu/Controls") as GameObject;

        inMenu = false;
        inOptions = false;
        inControls = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        ShowMenu(false); // place in Controller
    }

    // Triggerable script to show in-game menu.
    public void ShowMenu(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Escape) || trigger == true)
        {
            if (inMenu == false)
            {
                drawer.SetActive(true);
            }
            else
            {
                drawer.SetActive(false);
            }

            inMenu = !inMenu;
        }
    }

    // Triggerable script to show in-game options.
    public void ShowOptions()
    {
        if (inOptions == false)
        {
            options.SetActive(true);
        }
        else
        {
            options.SetActive(false);
        }

        inOptions = !inOptions;
    }

    // Triggerable script to show in-game controls.
    public void ShowControls()
    {
        if (inControls == false)
        {
            controls.SetActive(true);
        }
        else
        {
            controls.SetActive(false);
        }

        inControls = !inControls;
    }

    // Exit game to main menu.
    public void MainMenu()
    {

    }

    // Exit game to window.
    public void QuitGame()
    {
        Application.Quit();
    }
}
