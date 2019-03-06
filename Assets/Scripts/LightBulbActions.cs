using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbActions : ItemActionInterface
{
    public string[] myActionNames;
    private Haunt myHaunt;
    public Light myLight;
    private bool flickering;
    private float counter;
    private float rand;
    private bool toggle;
    private Global global;
    private Haunt myHauntScript;


    private void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        toggle = true;
        rand = Random.RandomRange(0.2f, 1.5f);
        flickering = false;
        myActionNames = new string[] { "On/Off", "...", "Unhaunt", "Flicker" };
        myHaunt = GameObject.Find("Player").GetComponentInChildren<Haunt>();
    }

    public override void callAction1()
    {
        if (!toggle)
        {
            myLight.intensity = 5;
        } else
        {
            myLight.intensity = 0;
        }
        toggle = !toggle;
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
        flickering = !flickering;
    }

    private void Update()
    {
        if (flickering)
        {
            counter += Time.deltaTime;
            
            if(counter >= rand)
            {
                rand = Random.RandomRange(0.2f, 1.5f);
                counter = 0;
                if(myLight.intensity > 0)
                {
                    myLight.intensity = 0;
                } else
                {
                    myLight.intensity = 5;
                }
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
