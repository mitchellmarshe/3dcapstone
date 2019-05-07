using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public GameObject gddp;
    public GameObject logo;

    public void Start()
    {
        Gddp();
        Invoke("Logo", 3);
        Invoke("Game", 6);
    }

    public void Gddp()
    {
        gddp.SetActive(true);
        logo.SetActive(false);
    }

    public void Logo()
    {
        gddp.SetActive(false);
        logo.SetActive(true);
    }

    public void Game()
    {
        SceneManager.LoadScene("Start");
    }
}
