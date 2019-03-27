using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRadioActions : ItemActionInterface
{
    public string[] myActionNames;
    private AudioSource radioSound;
    private AudioClip jazz;
    private AudioClip jazzDistorted;
    private Haunt myHaunt;
    private SphereCollider myDistortTrigger;
    private List<GameObject> distortedNPCS = new List<GameObject>();
    private bool distorted;
    private float timeHolder;
    private Global global;

    public AudioClip radioOverload;

    private void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        timeHolder = 0;
        distorted = false;
        myActionNames = new string[] { "Distort", "Overload*", "Back...", "..." };
        states = new bool[4] { true, true, true, false };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        radioSound = gameObject.GetComponent<AudioSource>();
        jazz = Resources.Load<AudioClip>("sounds/JazzSong_1");
        jazzDistorted = Resources.Load<AudioClip>("sounds/JazzSongDistorted_3");
        //jazzDistorted = Resources.Load<AudioClip>("sounds/");
    }

    public override void callAction1()
    {
        radioSound.clip = jazzDistorted;
        radioSound.Play();
        distorted = true;
        // summon NPC to fix back to normal
    }
    private void Update()
    {
        if (!radioSound.isPlaying)
        {
            distorted = false;
        }
        if (distorted)
        {
            timeHolder += Time.deltaTime;
            if (timeHolder >= 0.5f) {

                timeHolder = 0;
                for (int i = 0; i < distortedNPCS.Count; i++)
                {
                    distortedNPCS[i].GetComponent<ReactiveAIMK2>().addFear(50);
                }
            }
        }
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

    public override void callAction2()
    {
        
        //radioSound.clip = radioOverload;
        
        for (int i = 0; i < distortedNPCS.Count; i++)
        {
            distortedNPCS[i].GetComponent<ReactiveAIMK2>().addFear(2500);
            distortedNPCS[i].GetComponent<ReactiveAIMK2>().setDead();

        }
        //myHaunt.unPossess();
        radioSound.Stop();
        radioSound.clip = null;
        
        radioSound.PlayOneShot(radioOverload);
        global.softSelected = null;
        global.hardSelected = null;
        foreach (MeshRenderer mesh in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
        Destroy(gameObject, 1);
    }

    public override void callAction3()
    {
        myHaunt.goBackAHaunt();
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
