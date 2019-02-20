using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioActions : ItemActionInterface
{
    public string[] myActionNames;
    private AudioSource radioSound;

    private void Start()
    {
        myActionNames = new string[] { "Turn On", "Turn Off", "Distort", "Reset" };
        radioSound = gameObject.GetComponent<AudioSource>();
    }

    public override void  callAction1()
    {
        radioSound.Play();
    }

    public override void callAction2()
    {
        radioSound.Pause();
    }

    public override void callAction3()
    {
        radioSound.pitch = radioSound.pitch - (float) 0.6;
    }

    public override void callAction4()
    {
        radioSound.pitch = 1;
    }

    public override string[] getActionNames()
    {
        return myActionNames;
    }

    public override void setActionNames(string[] names)
    {
        for(int i = 0; i < 4; i++)
        {
            myActionNames[i] = names[i];
        }
    }
}
