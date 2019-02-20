using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntActions : ItemActionInterface
{
    public string[] myActionNames;

    // Start is called before the first frame update
    void Start()
    {
        myActionNames = new string[] { "Haunt", "...", "Unpossess", "..." };
    }

    // Update is called once per frame
    void Update()
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
