using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundActions : ItemActionInterface
{
    public string[] myActionNames;
    private AudioSource radioSound;
    private AudioClip jazz;
    private AudioClip jazzDistorted;
    private Haunt myHaunt;

    private void Start()
    {
        myActionNames = new string[] { "Random Pitch", "Volume Up", "Back...", "Volume Down" };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        radioSound = gameObject.GetComponent<AudioSource>();
        jazz = Resources.Load<AudioClip>("sounds/JazzSong_1");
        jazzDistorted = Resources.Load<AudioClip>("sounds/JazzSongDistorted_3");
        states = new bool[4] { true, true, true, true };
    }

    public override void callAction1()
    {
        
        float newPitch = 0;
        do
        {
            newPitch = Random.Range(-2, 3);
        } while (newPitch == 0 || radioSound.pitch == newPitch);
        radioSound.pitch = newPitch;
        Debug.Log(radioSound.pitch + " is my Pitch!");
    }

    public override void callAction2()
    {
        radioSound.volume += 0.1f;
    }

    public override void callAction3()
    {
        myHaunt.goBackAHaunt();
    }

    public override void callAction4()
    {
        radioSound.volume -= 0.1f;
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
