using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerActions : ItemActionInterface
{
    public string[] myActionNames;

    private AudioSource radioSound;
    private AudioClip jazz;
    private AudioClip jazzDistorted;
    private Haunt myHaunt;
    private bool playing;

    private void Start()
    {
        playing = false;
        myActionNames = new string[] { "On/Off", "...", "Back...", "..." };

        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        radioSound = gameObject.GetComponent<AudioSource>();
        jazz = Resources.Load<AudioClip>("sounds/JazzSong_1");
        jazzDistorted = Resources.Load<AudioClip>("sounds/JazzSongDistorted_3");
        states = new bool[4] { true, false, true, false };
    }

    public override void callAction1()
    {
        if (playing)
        {
            radioSound.Pause();
        }
        else
        {
            radioSound.clip = jazz;
            radioSound.Play();
        }
        playing = !playing;
    }

    public override void callAction2()
    {
        
    }

    public override void callAction3()
    {
        myHaunt.goBackAHaunt();
    }

    public override void callAction4()
    {
        // need to play explosion sound, do vfx, add fear at anynearby NPC, call unpossess, and then remove gameobject.
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