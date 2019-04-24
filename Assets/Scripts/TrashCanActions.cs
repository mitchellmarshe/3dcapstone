using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanActions : ItemActionInterface
{
    public string[] myActionNames;
    private Animator myAnim;
    private Haunt myHaunt;
    private Global global;


    private bool popped = false;
    private bool enabledPapers = false;
    private bool soundFired = false;

    private AudioSource audio;

    private List<GameObject> distortedNPCS = new List<GameObject>();

    public GameObject paper1;
    public GameObject paper2;
    public GameObject paper3;

    float counter = 0;


    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        myAnim = gameObject.GetComponent<Animator>();
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Burst", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        states = new bool[4] { true, false, false, false };
    }

    public override void callAction1()
    {
        if (!popped)
        {
            
            popped = true;
            myAnim.SetBool("burst", popped);
        }
    }

    private void Update()
    {
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
                    //ejectPapers();
                }
               

            }
        }
    }

    private void ejectPapers()
    {
        Vector3 force1 = new Vector3(2500 * Random.Range(-2, 2), 8000, 0);
        Vector3 force2 = new Vector3(2500 * Random.Range(-2, 2), 8000, 0);
        Vector3 force3 = new Vector3(2500 * Random.Range(-2, 2), 8000, 0);
        paper1.SetActive(true);
        paper2.SetActive(true);
        paper3.SetActive(true);
        paper1.GetComponent<Rigidbody>().AddForce(force1);
        paper1.GetComponent<Rigidbody>().AddTorque(force1);
        paper2.GetComponent<Rigidbody>().AddForce(force2);
        paper2.GetComponent<Rigidbody>().AddTorque(force2);
        paper3.GetComponent<Rigidbody>().AddForce(force3);
        paper3.GetComponent<Rigidbody>().AddTorque(force3);
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
