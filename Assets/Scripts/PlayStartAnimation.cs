using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStartAnimation : MonoBehaviour
{
    public GameObject player;
    public Menu menu;

    // Start is called before the first frame update
    private void Start()
    {
        player.GetComponentInChildren<Camera>().enabled = false;
        player.GetComponentInChildren<Controller2>().enabled = false;
        menu.fade.Play("reverseDeath");
        Invoke("activePlayer", 14);
    }

    private void activePlayer()
    {
        player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponentInChildren<Controller2>().enabled = true;
        gameObject.SetActive(false);
    }
}
