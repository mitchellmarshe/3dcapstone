using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<DoubleDoorActions2> doors;
    public List<RoomManager> neighbors;
    private bool lockdown = false;
    private List<ReactiveAIMK2> npcs;
    public bool isHallWay = false;
    //private RoomManager theRoomMan;


    // Start is called before the first frame update
    void Start()
    {
        //theRoomMan = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, LayerMask.GetMask(LayerMask.LayerToName(0)));
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "NPC")
            {
                npcs.Add(collider.gameObject.GetComponent<ReactiveAIMK2>());
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public bool atleastOneDoorOpen()
    {
        
        bool tmp = false;
        if (!isHallWay)
        {
            int i = 0;
            while (!tmp)
            {
                tmp = doors[i].isOpen();
            }
        }
        return tmp;
    }

    public bool hasAtleastOneNPC()
    {
        return npcs.Count > 0;
    }

    public ReactiveAIMK2 getOneNPC(int depth)
    {
        if (hasAtleastOneNPC())
        {
            return npcs[0];
        } else if (depth > 0)
        {
            ReactiveAIMK2 tmpNPC = null;
            if (neighbors.Count == 2)
            {
                
                if (doors[1].isOpen())
                {
                    tmpNPC = neighbors[1].getOneNPC(0);
                }
                if (tmpNPC == null && doors[0].isOpen())
                {
                    tmpNPC = neighbors[0].getOneNPC(0);
                }
            } else
            {
                if (doors[0].isOpen())
                {
                    tmpNPC = neighbors[0].getOneNPC(0);
                }
            }
            return tmpNPC;
        }

        return null;

    }

    public List<ReactiveAIMK2> getMyNPC()
    {
        return npcs;
    }

    public List<ReactiveAIMK2> getAllNPC()
    {
        List<ReactiveAIMK2> tmpNPC = npcs;
        if (neighbors.Count == 2)
        {
            foreach(ReactiveAIMK2 ai in neighbors[1].getMyNPC())
            {
                tmpNPC.Add(ai);
            }
            foreach (ReactiveAIMK2 ai in neighbors[0].getMyNPC())
            {
                tmpNPC.Add(ai);
            }
        }
        else
        {
            foreach (ReactiveAIMK2 ai in neighbors[0].getMyNPC())
            {
                tmpNPC.Add(ai);
            }
        }

        return tmpNPC;
    }


    /*
    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, LayerMask.GetMask(LayerMask.LayerToName(0)));
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!npcs.Contains(other.gameObject.GetComponent<ReactiveAIMK2>()))
            {
                npcs.Add(other.gameObject.GetComponent<ReactiveAIMK2>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (npcs.Contains(other.gameObject.GetComponent<ReactiveAIMK2>()))
            {
                npcs.Remove(other.gameObject.GetComponent<ReactiveAIMK2>());
            }
        }
    }
}
