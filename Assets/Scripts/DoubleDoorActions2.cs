using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorActions2 : ItemActionInterface
{
    public string[] myActionNames;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;
    private bool open = true;
    private AudioSource audio;
    public AudioClip doorSound;
    private float counter = 0;
    private bool freeze = true;


    private void Start()
    {
        myAnim = transform.parent.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Close2", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
        audio = gameObject.GetComponent<AudioSource>();
    }

    public override void callAction1()
    {
        if (freeze)
        {
            counter += Time.deltaTime;
            if(counter >= 2)
            {
                freeze = false;
                counter = 0;
                setFreezeDoors(true);
            }
        }
        if (!audio.isPlaying) {
            if (myAnim.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("open"))
            {
                freeze = true;
                counter = 0;
                setFreezeDoors(false);
                open = true;
                myActionNames = new string[] { "Open2", "...", "...", "..." };
                myAnim.SetBool("open", false);
                audio.PlayOneShot(doorSound);
            } else
            {
                freeze = true;
                counter = 0;
                setFreezeDoors(false);
                open = false;
                myActionNames = new string[] { "Close2", "...", "...", "..." };
                myAnim.SetBool("open", true);
                audio.PlayOneShot(doorSound);
            }
        }
        
    }

    public bool isOpen()
    {
        return open;
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

    public void setFreezeDoors(bool freeze)
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = freeze;
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
