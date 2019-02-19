using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haunt : MonoBehaviour
{
    private bool inRange;
    private bool hButton;

    public Transform npcTransform;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown("h"))
            {
                hButton = !hButton;
            }

            if (hButton == true)
            {
                npcTransform.position = playerTransform.position;
            }
        }
    }
}
