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


    private void Start()
    {
        myAnim = transform.parent.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Open/Close", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
        audio = gameObject.GetComponent<AudioSource>();
    }

    public override void callAction1()
    {
        if (!audio.isPlaying) {
            if (myAnim.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("open"))
            {
                open = true;
                myAnim.SetBool("open", false);
                audio.PlayOneShot(doorSound);
            } else
            {
                open = false;
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
