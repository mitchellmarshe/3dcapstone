using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeActions : ItemActionInterface
{
    public string[] myActionNames;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;
    private bool thrashing = false;
    private float counter = 0;
    private AudioSource audio;
    public AudioClip fridgeSounds;
    public Light myLight;
    private List<GameObject> distortedNPCS = new List<GameObject>();


    private void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Slam", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (thrashing)
        {
            counter += Time.deltaTime;
            if (counter >= audio.clip.length)
            {
                thrashing = false;
                counter = 0;
                myAnim.SetBool("thrashing", false);
                myLight.enabled = false;


            }
        }
    }

    public override void callAction1()
    {
        if (!thrashing)
        {
            thrashing = true;
            myAnim.SetBool("thrashing", true);
            myLight.enabled = true;
            audio.PlayOneShot(fridgeSounds);
            counter = 0;
            for (int i = 0; i < distortedNPCS.Count; i++)
            {
                distortedNPCS[i].GetComponent<ReactiveAIMK2>().setSurprised();
            }
        }
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