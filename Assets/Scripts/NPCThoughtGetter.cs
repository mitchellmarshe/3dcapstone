using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCThoughtGetter : MonoBehaviour
{
    private List<GameObject> npcs = new List<GameObject>();
    private float time;
    private int minSpeakTime = 4;
    private int maxSpeakTime = 20;
    private int rand;
    // Start is called before the first frame update
    private void Start()
    {
        rand = Random.Range(minSpeakTime, maxSpeakTime + 1);
    }
    public bool registerNewNPC(GameObject npc)
    {
        if (!npcs.Contains(npc))
        {
            npcs.Add(npc);
            return true;
        }

            return false;
        
        
    }

    public bool unregisterNPC(GameObject npc)
    {

       return npcs.Remove(npc);

    }

    public GameObject getNearestNPC()
    {
        float min = -1;
        GameObject closestNPC = null;
        for(int i = 0; i < npcs.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, npcs[i].transform.position);
            if (min > dist || min == -1)
            {
                min = dist;
                closestNPC = npcs[i];
            }
        }

        return closestNPC;
    }

    public void nearestSaySomething()
    {
        GameObject obj = getNearestNPC();
        if (obj != null)
        {


            ReactiveAIMK2 ai = obj.GetComponent<ReactiveAIMK2>();

            if (ai.myAnimator.runtimeAnimatorController == ai.noFearController)
            {
                ai.saySomethingGeneral(ai.lowFearThoughts);
            }
            else if (ai.myAnimator.runtimeAnimatorController == ai.lowFearController || ai.myAnimator.runtimeAnimatorController == ai.medFearController)
            {
                ai.saySomethingGeneral(ai.midFearThoughts);
            }
            else if (ai.myAnimator.runtimeAnimatorController == ai.highFearController)
            {
                ai.saySomethingGeneral(ai.highFearThoughts);
            }
        } else
        {
            npcs.Remove(obj);
        }

    }

    private void Update()
    {
        if (npcs.Count > 0)
        {
            time += Time.deltaTime;
            if (time >= rand)
            {
                rand = Random.Range(minSpeakTime, maxSpeakTime + 1);
                time = 0;
                nearestSaySomething();
            }
        }
    }
}

/*
 * 
 * ReactiveAIMK2 ai = npcs[i].GetComponent<ReactiveAIMK2>();
            if (ai.myAnimator.runtimeAnimatorController == ai.noFearController)
            {

            } else if(ai.myAnimator.runtimeAnimatorController == ai.lowFearController || ai.myAnimator.runtimeAnimatorController == ai.medFearController)
            {

            } else if (ai.myAnimator.runtimeAnimatorController == ai.highFearController)
            {

            }
 * */

