using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineActions : ItemActionInterface
{
    public string[] myActionNames;
    private Haunt myHaunt;
    public GameObject mySodaCan;
    private bool shooting;
    private float counter;
    private int cans;
    private Global global;

    private void Start()
    {
        cans = 0;
        counter = 0;
        shooting = false;
        mySodaCan.SetActive(false);
        myActionNames = new string[] { "Shoot Soda*", "...", "Unhaunt", "..." };
        myHaunt = GameObject.Find("Player").GetComponentInChildren<Haunt>();
        global = GameObject.Find("Global").GetComponent<Global>();
        states = new bool[4] { true, false, true, true };
    }

    private void Update()
    {
        if (shooting)
        {
            counter += Time.deltaTime;
            if(counter >= .2)
            {
                cans++;
                counter = 0;
                GameObject newSodaCan = Instantiate(mySodaCan, gameObject.transform);
                newSodaCan.SetActive(true);
                newSodaCan.GetComponent<Rigidbody>().AddForce(1000 * newSodaCan.transform.up);
            }
            if(cans > 8)
            {
                shooting = false;
                cans = 0;
                counter = 0;
            }
        }

    }

    public override void callAction1()
    {
        shooting = true;
    }

    public override void callAction2()
    {

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
