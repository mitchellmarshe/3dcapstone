using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncineratorActions : ItemActionInterface
{
    public string[] myActionNames;
    private DynamicButtonUpdater buttonUpdater;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;

    private AudioSource audio;
    private bool smoking = false;
    private bool open = true;
    public Avatar openAvatar;
    public Avatar closedAvatar;
    private List<GameObject> distortedNPCS = new List<GameObject>();


    float counter = 0;


    private void Start()
    {
        buttonUpdater = GameObject.Find("Player").GetComponent<DynamicButtonUpdater>();
        audio = gameObject.GetComponent<AudioSource>();
        myAnim = gameObject.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Open/Close", "Smoke", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, true, false, false };
    }

    private void Update()
    {

    }

    public override void callAction1()
    {
        open = !open;
        
        if (!open)
        {
           //myAnim.avatar = closedAvatar;    
            states = new bool[4] { true, false, false, false };
        } else
        {
            //myAnim.avatar = openAvatar;
            states = new bool[4] { true, true, false, false };
        }
        myAnim.SetBool("open", open);
        //buttonUpdater.setStates(states);
    }

    


    public override void callAction2()
    {

    }

    public override void callAction3()
    {

    }

    public override void callAction4()
    {

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