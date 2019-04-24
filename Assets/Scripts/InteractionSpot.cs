using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSpot : MonoBehaviour
{
    // Start is called before the first frame update
    private ReactiveAIMK2 currentNPC;
    private bool occupied;

    public bool sendNpc(ReactiveAIMK2 newNPC)
    {
        if (!occupied)
        {
            currentNPC = newNPC;
            occupied = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public ReactiveAIMK2 getCurrentNPC()
    {
        if (occupied)
        {
            return currentNPC;
        } else
        {
            return null;
        }
    }

    public bool isOccupied()
    {
        return occupied;
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
