using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class largeTriggerCollider : MonoBehaviour
{
    private GameObject parent;
    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !parent.GetComponent<ReactiveAIMK2>().stopped)
        {
            other.gameObject.GetComponent<NPCThoughtGetter>().registerNewNPC(parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            other.gameObject.GetComponent<NPCThoughtGetter>().unregisterNPC(parent);
        }
    }
}
