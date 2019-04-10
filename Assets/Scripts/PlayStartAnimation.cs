using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStartAnimation : MonoBehaviour
{
    public GameObject player;
    public Menu menu;
    public GameObject decals;
    public GameObject tutorial;
    // Start is called before the first frame update
    private void Start()
    {
        player.GetComponentInChildren<Camera>().enabled = false;
        player.GetComponentInChildren<Controller2>().enabled = false;
        decals.SetActive(false);
        tutorial.SetActive(false);
        menu.fade.Play("reverseDeath");
        Invoke("activePlayer", 14);
    }

    private void activePlayer()
    {
        player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponentInChildren<Controller2>().enabled = true;
        decals.SetActive(true);
        tutorial.SetActive(true);
        gameObject.SetActive(false);
    }
}
