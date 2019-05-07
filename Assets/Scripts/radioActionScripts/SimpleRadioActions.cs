using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRadioActions : ItemActionInterface
{
    public string[] myActionNames;
    private AudioSource radioSound;
    private AudioClip jazz;
    private AudioClip jazzDistorted;
    private Haunt myHaunt;
    private Global global;

    private SphereCollider myDistortTrigger;
    private List<GameObject> distortedNPCS = new List<GameObject>();
    private bool distorted;
    private float timeHolder;

    private bool playing;

    public AudioClip radioOverload;


    private void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Jazz", "Spooky", "Explode", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        radioSound = gameObject.GetComponent<AudioSource>();
        jazz = Resources.Load<AudioClip>("sounds/JazzSong_1");
        jazzDistorted = Resources.Load<AudioClip>("sounds/JazzSongDistorted_3");
        states = new bool[4] { true, true, true, false };
    }

    public override void callAction1()
    {
        if (playing && radioSound.clip == jazz)
        {
            radioSound.Pause();
            playing = false;
        }
        else
        {
            radioSound.clip = jazz;
            radioSound.Play();
            playing = true;
        }
        distorted = false;

    }

    public override void callAction2()
    {
        if (playing && radioSound.clip == jazzDistorted)
        {
            radioSound.Pause();
            playing = false;
            distorted = false;
        }
        else
        {
            radioSound.clip = jazzDistorted;
            radioSound.Play();
            distorted = true;
            playing = true;
        }
        
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
            if (timeHolder >= 0.5f)
            {

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

    public override void callAction3()
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
        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(gameObject, 1);
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