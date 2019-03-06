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
    private Global global;

    private void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Power", "Sound", "Unhaunt", "Special" };
        myHaunt = GameObject.Find("Player").GetComponentInChildren<Haunt>();
        radioSound = gameObject.GetComponent<AudioSource>();
        jazz = Resources.Load<AudioClip>("sounds/JazzSong_1");
        jazzDistorted = Resources.Load<AudioClip>("sounds/JazzSongDistorted_3");
        states = new bool[4] { true, true, true, true };
    }

    public override void  callAction1()
    {
        ItemActionInterface tmp = gameObject.GetComponent<PowerActions>();
        myHaunt.goFowardAHaunt(tmp);
    }

    public override void callAction2()
    {
        ItemActionInterface tmp = gameObject.GetComponent<SoundActions>();
        myHaunt.goFowardAHaunt(tmp);
    }

    public override void callAction3()
    {
        //ItemActionInterface tmp = gameObject.GetComponent<ItemActionInterface>();
        //myHaunt.prepForHaunt(gameObject, tmp);

        global.possessing = false;
        myHaunt.unPossess();
    }

    public override void callAction4()
    {
        ItemActionInterface tmp = gameObject.GetComponent<SpecialRadioActions>();
        myHaunt.goFowardAHaunt(tmp);
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
