using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void mainMenu ()
    {
        SceneManager.LoadScene("Start");
    }

    public void restart()
    {
        SceneManager.LoadScene("Main");
    }
}
