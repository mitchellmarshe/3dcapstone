using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioActions : ItemActionInterface
{
    public string[] myActionNames;
    private AudioSource radioSound;
    private AudioClip jazz;
    private AudioClip jazzDistorted;
    private Haunt myHaunt;

    private void Start()
    {
        myActionNames = new string[] { "Turn On", "Turn Off", "Back...", "Distort" };
        myHaunt = GameObject.Find("Player").GetComponentInChildren<Haunt>();
        radioSound = gameObject.GetComponent<AudioSource>();
        jazz = Resources.Load<AudioClip>("sounds/JazzSong_1");
        jazzDistorted = Resources.Load<AudioClip>("sounds/JazzSongDistorted_3");
}

    public override void  callAction1()
    {
        radioSound.clip = jazz;
        radioSound.Play();
    }

    public override void callAction2()
    {
        radioSound.Pause();
    }

    public override void callAction3()
    {
        ItemActionInterface tmp = gameObject.GetComponent<ItemActionInterface>();
        myHaunt.prepForHaunt(gameObject, tmp);
    }

    public override void callAction4()
    {
        radioSound.clip = jazzDistorted;
        radioSound.Play();
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
