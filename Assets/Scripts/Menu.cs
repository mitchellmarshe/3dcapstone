using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script to handle menu system.

public class Menu : MonoBehaviour
{
    // Ensure these are hooked up from GUI/Menu!
    [Header("Scripts")]
    public Global global;

    [Header("Knob")]
    public GameObject knob;

    [Header("Drawer")]
    public GameObject drawer;
    public Button menu1Button;
    public Text menu1Text;
    public Button menu2Button;
    public Text menu2Text;
    public Button menu3Button;
    public Text menu3Text;
    public Button menu4Button;
    public Text menu4Text;

    [Header("Confirm")]
    public GameObject confirm;

    [Header("Controls")]
    public GameObject controls;

    [Header("Options")]
    public GameObject options;

    [Header("Credits")]
    public GameObject credits;

    [Header("Animations")]
    public Animator animator;

    private bool inMenu;
    private bool inConfirm;
    private bool inControls;
    private bool inOptions;
    private bool inCredits;

    private bool inGame;
    private bool isRestarting;
    private bool isQuitting;

    private void Awake()
    {
        inMenu = false;
        inConfirm = false;
        inControls = false;
        inOptions = false;
        inCredits = false;

        isRestarting = false;
        isQuitting = false;

        knob.SetActive(true); // Script it?

        ShowConfirm();
        ShowControls();
        ShowOptions();
        ShowCredits();
    }

    private void Start()
    {
        if (global.currentScene == 0)
        {
            inGame = false;
            SetOutGameMenu();
            ShowMenu(true);
        }
        else
        {
            inGame = true;
            SetInGameMenu();
            ShowMenu(false);
        }
    }

    private void Update()
    {
        ShowMenu(false); // place in Controller?
    }

    // Triggerable script for Menu 1 Button.
    public void Menu1()
    {
        if (inMenu == true)
        {
            if (inGame == false)
            {
                StartGame();
            }
            else
            {
                RestartingGame();
            }
        }
    }

    // Triggerable script for Menu 2 Button.
    public void Menu2()
    {
        if (inMenu == true)
        {
            ShowControls();
        }
    }

    // Triggerable script for Menu 3 Button.
    public void Menu3()
    {
        if (inMenu == true)
        {
            if (inGame == false)
            {
                ShowCredits();
            }
            else
            {
                ShowOptions();
            }
        }
    }

    // Triggerable script for Menu 4 Button.
    public void Menu4()
    {
        if (inMenu == true)
        {
            QuittingGame();
        }
    }

    // Triggerable script for Yes Button.
    public void ConfirmYes()
    {
        if (inMenu == true)
        {
            if (isRestarting == true && isQuitting == false)
            {
                RestartGame();
            }

            if (isQuitting == true && isRestarting == false)
            {
                QuitGame();
            }
        }
    }

    // Triggerable script for No Button.
    public void ConfirmNo()
    {
        if (inMenu == true)
        {
            if (isRestarting == true && isQuitting == false)
            {
                RestartingGame();
            }

            if (isQuitting == true && isRestarting == false)
            {
                QuittingGame();
            }
        }
    }

    // Triggerable script to show menu window.
    public void ShowMenu(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Escape) || trigger == true)
        {
            inMenu = !inMenu;
            drawer.SetActive(inMenu);
        }
    }

    // Triggerable script to show confirmation window.
    public void ShowConfirm()
    {
        confirm.SetActive(inConfirm);
        inConfirm = !inConfirm;
    }

    // Triggerable script to show controls window.
    public void ShowControls()
    {
        controls.SetActive(inControls);
        inControls = !inControls;
    }

    // Triggerable script to show options window.
    public void ShowOptions()
    {
        options.SetActive(inOptions);
        inOptions = !inOptions;
    }

    // Triggerable script to show credits windows.
    public void ShowCredits()
    {
        credits.SetActive(inCredits);
        inCredits = !inCredits;
    }

    // Start game.
    private void StartGame()
    {
        animator.Play("Death");
        Invoke("LoadGame", 3);
    }

    // Load game.
    private void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    // Context switch for the player to confirm whether or not they want to restart the game.
    private void RestartingGame()
    {
        isRestarting = !isRestarting;
        ShowConfirm();
    }

    // Restart game.
    private void RestartGame()
    {
        SceneManager.LoadScene("Start");
    }

    // Context switch for the player to confirm whether or not they want to quit the game.
    private void QuittingGame()
    {
        isQuitting = !isQuitting;
        ShowConfirm();
    }

    // Quit game entirely.
    private void QuitGame()
    {
        Application.Quit();
    }

    // Set up the out-game menu.
    private void SetOutGameMenu()
    {
        menu1Text.text = "Start";
        menu2Text.text = "Controls";
        menu3Text.text = "Credits";
        menu4Text.text = "Quit";
    }

    // Set up the in-game menu.
    private void SetInGameMenu()
    {
        menu1Text.text = "Restart";
        menu2Text.text = "Controls";
        menu3Text.text = "Options";
        menu4Text.text = "Quit";
    }
}
