using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntActions : ItemActionInterface
{
    public string[] myActionNames;
    private Global global;
    private Haunt myHauntScript;
    // Start is called before the first frame update
    void Awake()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        myActionNames = new string[] { "Haunt", "...", "Unpossess", "..." };
        myHauntScript = gameObject.GetComponent<Haunt>();
    }

    public override void callAction1()
    {
        global.possessing = true;
        myHauntScript.possess();

    }

    public override void callAction2()
    {
        //global.possesMove = !global.possesMove;
    }

    public override void callAction3()
    {
        global.possessing = false;
        myHauntScript.unPossess();
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
