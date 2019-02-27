using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TableandChairsActions : ItemActionInterface
{
    public string[] myActionNames;
    public GameObject chair1;
    public GameObject chair2;
    public GameObject chair3;
    public GameObject chair4;
    public GameObject chair5;
    public GameObject chair6;
    public GameObject chair7;
    public GameObject chair8;
    private Haunt myHaunt;
    private List<GameObject> distortedNPCS = new List<GameObject>();
    float timeHolder;
    bool levitating;

    private void Start()
    {
        levitating = false;
        myActionNames = new string[] { "Levitate", "...", "Back...", "..." };
        myHaunt = GameObject.Find("Player").GetComponentInChildren<Haunt>();
        chair5.SetActive(false);
        chair6.SetActive(false);
        chair7.SetActive(false);
        chair8.SetActive(false);
    }

    public override void callAction1()
    {
        levitating = true;
        chair1.SetActive(false);
        chair2.SetActive(false);
        chair3.SetActive(false);
        chair4.SetActive(false);
        chair5.SetActive(true);
        chair6.SetActive(true);
        chair7.SetActive(true);
        chair8.SetActive(true);
    }

    private void Update()
    {
        if (levitating)
        {
            timeHolder += Time.deltaTime;
            if (timeHolder >= 0.5f)
            {

                timeHolder = 0;
                for (int i = 0; i < distortedNPCS.Count; i++)
                {
                    distortedNPCS[i].GetComponent<ReactiveNPC>().addFear(50);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!distortedNPCS.Contains(other.gameObject))
            {
                distortedNPCS.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (distortedNPCS.Contains(other.gameObject))
            {
                distortedNPCS.Remove(other.gameObject);
            }
        }
    }

    public override void callAction2()
    {
        
    }

    public override void callAction3()
    {
        ItemActionInterface tmp = gameObject.GetComponent<ItemActionInterface>();
        myHaunt.prepForHaunt(gameObject, tmp);
    }

    public override void callAction4()
    {
        
    }

    public override string[] getActionNames()
    {
        return myActionNames;
    }

    public override void setActionNames(string[] names)
    {
        for (int i = 0; i < 4; i++)
        {
            myActionNames[i] = names[i];
        }
    }
}
