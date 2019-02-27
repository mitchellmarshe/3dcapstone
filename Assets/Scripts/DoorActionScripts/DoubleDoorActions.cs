using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorActions : ItemActionInterface
{
    public string[] myActionNames;
    private Haunt myHaunt;
    private Transform myTransform;
    private bool open;
    public GameObject opened;
    public GameObject closed;
    public BoxCollider openedCollider1;
    public BoxCollider openedCollider2;
    public BoxCollider closedCollider;

    private void Start()
    {
        opened.SetActive(false);
        openedCollider1.enabled = false;
        openedCollider2.enabled = false;
        myTransform = gameObject.transform;
        open = false;
        myActionNames = new string[] { "Open", "Close", "Back...", "" };
        myHaunt = GameObject.Find("Player").GetComponentInChildren<Haunt>();

    }

    public override void callAction1()
    {
        open = true;
        opened.SetActive(true);
        openedCollider1.enabled = true;
        openedCollider2.enabled = true;
        closedCollider.enabled = false;
        closed.SetActive(false);
    }

    public override void callAction2()
    {
        open = false;
        closed.SetActive(true);
        openedCollider1.enabled = false;
        openedCollider2.enabled = false;
        closedCollider.enabled = true;
        opened.SetActive(false);
    }

    public override void callAction3()
    {
        ItemActionInterface tmp = gameObject.GetComponent<ItemActionInterface>();
        myHaunt.prepForHaunt(gameObject, tmp);
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
