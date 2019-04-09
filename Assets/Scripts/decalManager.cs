using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decalManager : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer myRenderer;
    private List<GameObject> nearbyNPCs;
    private bool effecting = false;

    void Start()
    {
        myRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        nearbyNPCs = new List<GameObject>();
        enableDecalEffects();
        disableDecalEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if (effecting)
        {
            
            if (myRenderer.sprite.name == "redrum")
            {
                foreach(GameObject npc in nearbyNPCs)
                {
                    npc.GetComponent<ReactiveAIMK2>().setSurprised();
                }
                nearbyNPCs.Clear();
            } else if (myRenderer.sprite.name == "unicorn")
            {
                foreach (GameObject npc in nearbyNPCs)
                {
                    npc.GetComponent<ReactiveAIMK2>().setLaughLight();
                }
                nearbyNPCs.Clear();
            }
            else if (myRenderer.sprite.name == "skull decal")
            {
                foreach (GameObject npc in nearbyNPCs)
                {
                    npc.GetComponent<ReactiveAIMK2>().setCoughToDeath();
                }
                nearbyNPCs.Clear();
            }
            else if (myRenderer.sprite.name == "hypnolizard")
            {
                foreach (GameObject npc in nearbyNPCs)
                {
                    npc.GetComponent<ReactiveAIMK2>().setHeartAttack();
                }
                nearbyNPCs.Clear();
            }
        }
    }

    public void enableDecalEffects()
    {
        SphereCollider triggerArea = gameObject.GetComponent<SphereCollider>();
        effecting = true;
        nearbyNPCs.Clear();
        foreach( Collider other in Physics.OverlapSphere(transform.position, triggerArea.radius))
        {
            if (other.gameObject.tag == "NPC")
            {
                nearbyNPCs.Add(other.gameObject);
                //Debug.Log("Added " + other.gameObject.name);
            }
        }

    }

    public void disableDecalEffects()
    {
        effecting = false;

    }

    /*
    
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("other is " + other.gameObject.name);
        if (other.gameObject.tag == "NPC")
        {
            //Debug.Log("NPC Detected");
            if (!nearbyNPCs.Contains(other.gameObject))
            {
                //Debug.Log("Added NPC");
                nearbyNPCs.Add(other.gameObject);
            }
        }
    }

    // updates distoredNPCs list
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (nearbyNPCs.Contains(other.gameObject))
            {
                //Debug.Log("removed NPC");
                nearbyNPCs.Remove(other.gameObject);
            }
        }
    }

    */
    
}
