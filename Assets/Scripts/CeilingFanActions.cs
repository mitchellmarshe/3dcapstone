using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFanActions : ItemActionInterface
{
    public string[] myActionNames;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;
    private bool thrashing = false;
    private float counter = 0;
    private AudioSource audio;
    public AudioClip fanSounds;


    private void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Whirlwind", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (thrashing)
        {
            counter += Time.deltaTime;
            if (counter >= 8)
            {
                thrashing = false;
                counter = 0;
                myAnim.SetBool("thrashing", false);
                


            }
        }
    }

    public override void callAction1()
    {
        if (!thrashing)
        {
            thrashing = true;
            myAnim.SetBool("thrashing", true);
            audio.PlayOneShot(fanSounds);
            counter = 0;
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
