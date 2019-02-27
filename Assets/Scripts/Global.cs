using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global settings/variable for the whole game!

public class Global : MonoBehaviour
{
    public bool haunted;
    public bool mouseLocked;
    public bool placingDecal;
    public bool decided;
    public bool possessing;
    //public bool possesMove;
    public enum Action {None, One, Two, Three, Four};
    public Action action;
    public GameObject selectedItem;
    //public bool inMenus;

    // Defaults to PC controls.
    public bool platform;

    // Start is called before the first frame update.
    void Awake()
    {
        possessing = false;
        //possesMove = false;
        haunted = false;
        mouseLocked = true;
        placingDecal = false;
        decided = false;
        action = Action.None;
        selectedItem = null;
        //inMenus = false;

        //platform = false;
    }

    // Update is called once per frame.
    void Update()
    {
        
    }
}
