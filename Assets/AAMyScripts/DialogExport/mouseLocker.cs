using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLocker : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.playerScriptSP controller;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Makes cursor invisible
    public void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller.enabled = true;

        
    }

    
    public void unlockMouse()
    {
        controller.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}