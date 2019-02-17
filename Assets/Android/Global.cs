using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global settings/variable for the whole game!

public class Global : MonoBehaviour
{
    public bool haunted;

    public bool decided;
    public enum Action {None, One, Two, Three, Four};
    public Action action;

    // Defaults to PC controls.
    public bool platform;

    // Start is called before the first frame update.
    void Start()
    {
        haunted = false;

        decided = false;
        action = Action.None;

        platform = false;
    }

    // Update is called once per frame.
    void Update()
    {
        
    }
}
