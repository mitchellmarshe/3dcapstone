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
    public GameObject startImage;
    public GameObject restartImage;
    public Button menu2Button;
    public GameObject controlsImage;
    public Button menu3Button;
    public GameObject creditsImage;
    public GameObject optionsImage;
    public Button menu4Button;
    public GameObject quitImage;

    [Header("Confirm")]
    public GameObject confirm;

    [Header("Controls")]
    public GameObject controls;

    [Header("Options")]
    public GameObject options;

    [Header("Credits")]
    public GameObject credits;

    [Header("Animations")]
    public Animator fade;
    public Animator title;

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
    }

    private void Start()
    {
        ShowControls();
        ShowOptions();
        ShowCredits();

        if (global.currentScene == global.startScene)
        {
            inGame = false;
            SetOutGameMenu();
            ShowMenu(true);
            ShowConfirm();
        }
        else
        {
            inGame = true;
            SetInGameMenu();
            ShowMenu(true);
            ShowConfirm();
            ShowMenu(true);
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

            if (inMenu == false)
            {
                inConfirm = false;
                ShowConfirm();
                inControls = false;
                ShowControls();
                inOptions = false;
                ShowOptions();
                inCredits = false;
                ShowCredits();

                isRestarting = false;
                isQuitting = false;
            }
        }
    }

    // Triggerable script to show confirmation window.
    public void ShowConfirm()
    {
        confirm.SetActive(inConfirm);
        inConfirm = !inConfirm;

        menu1Button.interactable = inConfirm;
        menu2Button.interactable = inConfirm;
        menu3Button.interactable = inConfirm;
        menu4Button.interactable = inConfirm;

        if (inMenu == true)
        {
            if (inConfirm == true)
            {
                controlsImage.GetComponent<Animator>().Play("MenuImageAlphaUp");
                quitImage.GetComponent<Animator>().Play("MenuImageAlphaUp");

                if (global.currentScene == global.startScene)
                {
                    startImage.GetComponent<Animator>().Play("MenuImageAlphaUp");
                    creditsImage.GetComponent<Animator>().Play("MenuImageAlphaUp");
                }
                else
                {
                    restartImage.GetComponent<Animator>().Play("MenuImageAlphaUp");
                    optionsImage.GetComponent<Animator>().Play("MenuImageAlphaUp");
                }
            }
            else
            {
                controlsImage.GetComponent<Animator>().Play("MenuImageAlphaDown");
                quitImage.GetComponent<Animator>().Play("MenuImageAlphaDown");

                if (global.currentScene == global.startScene)
                {
                    startImage.GetComponent<Animator>().Play("MenuImageAlphaDown");
                    creditsImage.GetComponent<Animator>().Play("MenuImageAlphaDown");
                }
                else
                {
                    restartImage.GetComponent<Animator>().Play("MenuImageAlphaDown");
                    optionsImage.GetComponent<Animator>().Play("MenuImageAlphaDown");
                }
            }
        }
    }

    // Triggerable script to show controls window.
    public void ShowControls()
    {
        if (inControls == true)
        {
            inCredits = false;
            ShowCredits();
            inOptions = false;
            ShowOptions();
        }

        controls.SetActive(inControls);
        inControls = !inControls;
    }

    // Triggerable script to show options window.
    public void ShowOptions()
    {
        if (inOptions == true)
        {
            inControls = false;
            ShowControls();
        }

        options.SetActive(inOptions);
        inOptions = !inOptions;
    }

    // Triggerable script to show credits windows.
    public void ShowCredits()
    {
        if (inCredits == true)
        {
            inControls = false;
            ShowControls();
        }

        credits.SetActive(inCredits);
        inCredits = !inCredits;
    }

    // Start game.
    private void StartGame()
    {
        ShowMenu(true);
        fade.Play("Death");
        title.Play("Title");
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
        startImage.SetActive(true);
        restartImage.SetActive(false);
        controlsImage.SetActive(true);
        creditsImage.SetActive(true);
        optionsImage.SetActive(false);
        quitImage.SetActive(true);
    }

    // Set up the in-game menu.
    private void SetInGameMenu()
    {
        startImage.SetActive(false);
        restartImage.SetActive(true);
        controlsImage.SetActive(true);
        creditsImage.SetActive(false);
        optionsImage.SetActive(true);
        quitImage.SetActive(true);
    }
}
