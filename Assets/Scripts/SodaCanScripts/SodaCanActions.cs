using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaCanActions : ItemActionInterface
{
    public string[] myActionNames;
    private Haunt myHaunt;
    Rigidbody myRigid;
    private bool shaking;
    private float myTime;
    private int shakeStopper;
    private List<GameObject> scaredNPCS = new List<GameObject>();
    private bool popped;
    private Global global;

    private void Start()
    {
        popped = false;
        shakeStopper = 0;
        myActionNames = new string[] { "Shake", "POP*", "Back...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        myRigid = gameObject.GetComponent<Rigidbody>();
        shaking = false;
        global = GameObject.Find("Global").GetComponent<Global>();
        states = new bool[4] { true, true, true, false };

    }

    private void Update()
    {
        if (shaking)
        {
            myTime += Time.deltaTime;
            if(myTime >= .3)
            {
                shakeStopper += 1;
                int random = Random.Range(1, 6);
                if(random == 1)
                {
                    myRigid.AddForce(gameObject.transform.right * 1000);
                } else if (random == 2)
                {
                    myRigid.AddForce(gameObject.transform.forward * 1000);
                } else if (random == 3)
                {
                    myRigid.AddForce(gameObject.transform.up * 1000);
                } else if (random == 4)
                {
                    myRigid.AddForce(gameObject.transform.right * -1000);
                }
                else if (random == 5)
                {
                    myRigid.AddForce(gameObject.transform.forward * -1000);
                }
                else if (random == 6)
                {
                    myRigid.AddForce(gameObject.transform.up * -1000);
                }
                if(shakeStopper >= 5)
                {
                    shaking = false;
                    shakeStopper = 0;
                }
                myTime = 0;
                

            }
        }
        if (popped)
        {
            
            for (int i = 0; i < scaredNPCS.Count; i++)
            {
            scaredNPCS[i].GetComponent<ReactiveAIMK2>().addFear(150);
            }
            //myHaunt.unPossess();
            global.softSelected = null;
            global.hardSelected = null;
            gameObject.SetActive(false);

        }
    }

    public override void callAction1()
    {
        shaking = true;
        
    }

    public override void callAction2()
    {
        popped = true;
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

    public override void callAction3()
    {
        //ItemActionInterface tmp = gameObject.GetComponent<ItemActionInterface>();
        //myHaunt.prepForHaunt(gameObject, tmp);

        //global.possessing = false;
        //myHaunt.unPossess();
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
