using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ItemInterface
{
    void setActionNames(string[] names);
    string[] getActionNames();
    void callAction1();
    void callAction2();
    void callAction3();
    void callAction4();
}

//This abstract class will be used for most object-player interactions and
//provides a baseline for updating UI and performing actions
public abstract class ItemActionInterface : MonoBehaviour, ItemInterface
{


    public abstract void callAction1();

    public abstract void callAction2();

    public abstract void callAction3();


    public abstract void callAction4();

    public abstract string[] getActionNames();

    public abstract void setActionNames(string[] names);
}
