using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRigidBodyActions : ItemActionInterface
{
    public string[] myActionNames;
    private Haunt myHaunt;
    Rigidbody myRigid;
    private Global global;


    private void Start()
    {
        myActionNames = new string[] { "...", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        myRigid = gameObject.GetComponent<Rigidbody>();
        global = GameObject.Find("Global").GetComponent<Global>();
        states = new bool[4] { false, false, false, false };

    }

    private void Update()
    {
       
    }

    public override void callAction1()
    {
        

    }

    public override void callAction2()
    {
       
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
