using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperActions : ItemActionInterface
{
    public string[] myActionNames;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;
    private bool crumpled = false;
    private AudioSource audio;
    


    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        myAnim = gameObject.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Crumble", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
    }

    public override void callAction1()
    {
        if (!crumpled)
        {

            myAnim.SetBool("crumple", true);
            crumpled = true;
            audio.Play();
            
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
