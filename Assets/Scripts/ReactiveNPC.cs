using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReactiveNPC : MonoBehaviour
{
    private NavMeshAgent myAgent;
    private Animator myAnimator;
    private int myOldFear;
    private int myCurrentFear;
    private bool decided;
    private Transform myTarget;
    private int numZones = 3;
    private bool stopped;
    private bool arrived;
    private float timer;

    private Slider fearSlider;
    private Global global;
    

    /*Trigger names
     * 
     * walk
     * idle
     * blind
     * fetal
     * heartAttack
     * surprised
     * 
     * */

    /*Other variable names
     * 
     * fearFactor
     * 
     * */


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        decided = false;
        myAgent = gameObject.GetComponent<NavMeshAgent>();
        myAnimator = gameObject.GetComponent<Animator>();
        fearSlider = gameObject.GetComponentInChildren<Slider>();
        myOldFear = checkFear();
        myCurrentFear = myOldFear;
        updateFearSlider();
        global = GameObject.Find("Global").GetComponent<Global>();
    }

    //This is called to update the NPCs gui fear bar
    void updateFearSlider()
    {
        if(myCurrentFear <= 2500)
        {
            fearSlider.value = myCurrentFear;
        } else
        {
            fearSlider.value = 2500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myCurrentFear = checkFear();
        updateFearSlider();


        //The stopped state is true when the NPC suffers a disabilitating
        // animation such as dead, heart attack, fetal position, ect
        if (!stopped)
        {
            checkStates();

            //Decided is true when the NPC is either idling or walking somewhere
            if (!decided)
            {
                //This code below decides wether to idle or walk to a new target
                decided = true;
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    rand = Random.Range(1, numZones + 1);
                    Debug.Log(rand);
                    string targetname = "idlezone" + rand;
                    setNewTarget(targetname);
                    startWalk(myTarget);
                }
                else
                {
                    idleForTime(Random.Range(3, 8));
                }


            }
            else
            {
                myAnimator.SetTrigger("walk");

                if (arrived)
                {
                    decided = false;

                }
                // This code below helps update the NPC walk animation
                // if there fear level has changed
                else
                {
                    timer += Time.deltaTime;
                    if (timer > 0.5)
                    {
                        int tmp = myOldFear;
                        myOldFear = checkFear();
                        if (tmp != myOldFear)
                        {
                            myAnimator.ResetTrigger("walk");
                            myAnimator.SetTrigger("walk");
                        }

                        timer = 0f;
                    }
                }
            }
            if (Vector3.Distance(transform.position, myAgent.destination) <= 3)
            {
                arrived = true;
            }


        } else
        {

        }


    }

    // This handles fear specific triggers like heart attacks and fetal position
    // This also stops the NPC from moving
    public void checkStates()
    {

        if(myCurrentFear < 2500 && myCurrentFear >= 2000)
        {
            stopped = true;
            myAgent.isStopped = true;
            
            myAnimator.ResetTrigger("walk");
            myAnimator.ResetTrigger("idle");
            myAnimator.SetTrigger("fetal");
            myAnimator.fireEvents = false;
            StopAllCoroutines();
        } else if (myCurrentFear > 2500)
        {
            stopped = true;
            myAgent.isStopped = true;
            myAnimator.ResetTrigger("walk");
            myAnimator.ResetTrigger("idle");
            myAnimator.SetTrigger("heartAttack");
            myAnimator.fireEvents = false;
            StopAllCoroutines();
        }
    }
    
    //Sets the NPC to dead by stopping movement, playing animation, and setting fear to max
    public void setDead()
    {

            stopped = true;
            myAgent.isStopped = true;
            myAnimator.ResetTrigger("walk");
            myAnimator.ResetTrigger("idle");
            myAnimator.SetTrigger("dead");
            myAnimator.fireEvents = false;
            StopAllCoroutines();
            myAnimator.SetInteger("fearFactor", 2500);
            checkFear();
            updateFearSlider();
        
    }

    // This plays this surprised animation on the NPC when called
    public void setSurprised()
    {
        if (!myAgent.isStopped)
        {
            stopped = true;
            myAgent.isStopped = true;
            myAnimator.SetTrigger("surprised");
            myAnimator.fireEvents = false;
            IEnumerator coro = surprisedIenum(3.75f);
            //StopAllCoroutines();
            StartCoroutine(coro);
        }

    }

    // This picks a location in the list of possible targets
    public void setNewTarget(string name)
    {
        try { 
            decided = true;
            arrived = false;
            myTarget = GameObject.Find(name).GetComponent<Transform>();
        }
        catch
        {
            Debug.Log("setNewTarget in ReactiveNPC script has failed to find a GameObject with name " + name);
        }

        Debug.Log("set new target");
    }

    // Starts the walk animation and sets destination
    public void startWalk(Transform loc)
    {
        myAgent.SetDestination(myTarget.position);
        myAnimator.ResetTrigger("idle");
        myAnimator.SetTrigger("walk");
        myAgent.speed = 3.5f;
        Debug.Log("started walk");
    }

    // Keeps the NPC idling for the parameter time in seconds
    public void idleForTime(float time)
    {
        Debug.Log("idle for time " + time);
        myAnimator.ResetTrigger("walk");
        stopped = true;
        myAnimator.SetTrigger("idle");
        IEnumerator coro = idling(time);
        StartCoroutine(coro);

    }

    // coroutine for idling
    private IEnumerator idling(float time)
    {
        yield return new WaitForSeconds(time);
        stopped = false;
        myAgent.isStopped = false;
        myAnimator.fireEvents = true;
        
        Debug.Log("Done idling");
    }

    // coroutine for surprised, BUGGY
    private IEnumerator surprisedIenum(float time)
    {

        yield return new WaitForSeconds(time);
        stopped = false;
        myAnimator.ResetTrigger("surprised");
        myAgent.isStopped = false;
        myAnimator.fireEvents = true;
        addFear(500);
        Debug.Log("Done surprising");
    }

    //updates local fear var
    public int checkFear()
    {
        return myAnimator.GetInteger("fearFactor");
    }

    //adds to the NPC fear attribute
    // also sets dead if added fear puts NPC over max
    public void addFear(int num)
    {
        //if (!stopped)
        //{
            int tmp = myAnimator.GetInteger("fearFactor");
            if (tmp < 2500)
            {
                int tmp2 = 2500 - tmp;
                if (tmp2 < num)
                {
                    global.points += tmp2;
                    myAnimator.SetInteger("fearFactor", 2500);
                }
                else
                {
                    global.points += num;
                    myAnimator.SetInteger("fearFactor", tmp + num);
                }
            }
            else
            {
                setDead();
            }
        //}

    }
}
