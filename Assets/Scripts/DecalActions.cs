using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalActions : ItemActionInterface
{
    public string[] myActionNames;
    Controller myController;

    private void Start()
    {
        myActionNames = new string[4]{ "Decal Menu", "Place Decal", "...", "Remove Decal" };
        myController = GameObject.Find("Player/Controller").GetComponent<Controller>();
        states = new bool[4] { true, false, false, true };
    }

    public override void callAction1()
    {
        myController.showDecalMenu();
        //myController.global.inMenus = true;
        //myController.unlockMouse();
    }

    public override void callAction2()
    {
        myController.placeDecalScript.placeAtSpot();
    }

    public override void callAction3()
    {
        
    }

    public override void callAction4()
    {
        myController.placeDecalScript.removeDecal();
    }

    public override string[] getActionNames()
    {
        return myActionNames;
    }

    public override void setActionNames(string[] names)
    {
        throw new System.NotImplementedException();
    }

}
