using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global settings/variable for the whole game!

public class Global : MonoBehaviour
{
    public bool haunted;
    public bool decided;
    public int choice;

    // Start is called before the first frame update
    void Start()
    {
        haunted = false;
        decided = false;
        choice = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
