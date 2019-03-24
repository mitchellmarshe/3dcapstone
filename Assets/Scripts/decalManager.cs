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
                    npc.GetComponent<ReactiveNPC>().setSurprised();
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
                Debug.Log("Added " + other.gameObject.name);
            }
        }

    }

    public void disableDecalEffects()
    {
        effecting = false;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!nearbyNPCs.Contains(other.gameObject))
            {
                nearbyNPCs.Add(other.gameObject);
            }
        }
    }

    // updates distoredNPCs list
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (nearbyNPCs.Contains(other.gameObject))
            {
                nearbyNPCs.Remove(other.gameObject);
            }
        }
    }
}
