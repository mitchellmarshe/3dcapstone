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
    private List<GameObject> scaredNPCS = new List<GameObject>();
    private bool startSound = false;
    private bool startAnim = false;

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
            if (counter > .2f && startSound)
            {
                startSound = false;
                audio.PlayOneShot(fanSounds);
                
            }

            if(counter >= 1.6f && startAnim)
            {
                startAnim = false;
                for (int i = 0; i < scaredNPCS.Count; i++)
                {
                    LayerMask newMask = LayerMask.GetMask("walls");
                    if (!Physics.Linecast(transform.position, scaredNPCS[i].transform.position, newMask, QueryTriggerInteraction.UseGlobal))
                    {

                        scaredNPCS[i].GetComponent<ReactiveAIMK2>().setSurprisedDuck();
                    }
                }
            }
            if (counter >= 2.2f)
            {
                thrashing = false;
                counter = 0;
                myAnim.SetBool("thrashing", false);
                audio.Stop();
                


            }
        }
    }

    public override void callAction1()
    {
        if (!thrashing)
        {
            thrashing = true;
            myAnim.SetBool("thrashing", true);
            startSound = true;
            startAnim = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!scaredNPCS.Contains(other.gameObject))
            {
                scaredNPCS.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (scaredNPCS.Contains(other.gameObject))
            {
                scaredNPCS.Remove(other.gameObject);
            }
        }
    }

    public override void setActionNames(string[] names)
    {
        for (int i = 0; i < 4; i++)
        {
            myActionNames[i] = names[i];
        }
    }
}
