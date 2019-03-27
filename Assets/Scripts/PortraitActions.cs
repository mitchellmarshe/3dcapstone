using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitActions : ItemActionInterface
{
    public string[] myActionNames;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;

    private bool soundFired = false;

    private AudioSource audio;

    private List<GameObject> distortedNPCS = new List<GameObject>();

    public Material matNorm;
    public Material matSpooky1;
    public Material matSpooky2;

    public AudioClip spookyNoise;

    private int index = 1;

    public Shader transitionShader;
    private MeshRenderer myMeshRenderer;

    float counter = 0;
    bool ticking = false;


    private void Start()
    {
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        audio = gameObject.GetComponent<AudioSource>();
        myAnim = gameObject.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Spookify", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
        //myMeshRenderer.material.SetTexture("currentTexture", getIndex(index));

    }

    private void Update()
    {
        /*
        if (ticking)
        {
            counter += Time.deltaTime;
            myMeshRenderer.material.SetVector("sliderOutput", new Vector4(counter, 0));
            if(counter >= 1)
            {
                ticking = false;
                counter = 0;
                myMeshRenderer.material.SetTexture("currentTexture", getIndex(index));
            }
        }

        */
        /*
        if (popped && !enabledPapers)
        {
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Burst"))
            {
                counter = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                counter = counter - (int)counter;
                if (counter >= .18f && !soundFired)
                {
                    audio.PlayOneShot(audio.clip);
                    soundFired = true;
                    for (int i = 0; i < distortedNPCS.Count; i++)
                    {
                        distortedNPCS[i].GetComponent<ReactiveAIMK2>().setSurprisedDuck();
                    }
                }
                if (counter >= .25f)
                {
                    enabledPapers = true;
                }


            }
        }

    */
    }

    public override void callAction1()
    {
        index++;
        myMeshRenderer.material = getIndex(index);
        gameObject.GetComponent<AudioSource>().PlayOneShot(spookyNoise);
        for (int i = 0; i < distortedNPCS.Count; i++)
        {
            distortedNPCS[i].GetComponent<ReactiveAIMK2>().setSurprised();
        }
        //ticking = true;

    }

    private Material getIndex(int i)
    {
        if(i > 3)
        {
            i = 1;

        }
        index = i;
        if(i == 1)
        {
            return matNorm;
        } else if(i == 2)
        {
            return matSpooky1;
        } else
        {
            return matSpooky2;
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