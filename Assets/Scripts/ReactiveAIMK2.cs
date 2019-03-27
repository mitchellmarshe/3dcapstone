using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReactiveAIMK2 : MonoBehaviour
{
    private NavMeshAgent myAgent;
    private Animator myAnimator;
    private int myOldFear;
    private int myCurrentFear;
    private bool decided;
    private Transform myTarget;
    private int numZones = 10;
    private bool stopped;
    private bool arrived;
    private float timer;

    private Slider fearSlider;
    private Global global;

    public AudioSource myAudioSource;

    public AnimatorOverrideController noFearController;
    public AnimatorOverrideController lowFearController; 
    public AnimatorOverrideController medFearController;
    public AnimatorOverrideController highFearController;

    public AudioClip[] generalQuotes;

    public AudioClip[] deathCrys;

    public AudioClip[] fetalCrys;


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
        myAudioSource = gameObject.GetComponent<AudioSource>();
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
        if (myCurrentFear <= 2500 && myCurrentFear >= 0)
        {
            fearSlider.value = myCurrentFear;
        } else if(myCurrentFear > 2500)
        {
            fearSlider.value = 2500;
        }
        else
        {
            fearSlider.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myOldFear = myCurrentFear;
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
                arrived = false;
                setAllAnimBoolsToBool(false);
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    rand = Random.Range(1, numZones + 1);
                    Debug.Log(rand);
                    string targetname = "idlezone" + rand;
                    setNewTarget(targetname);
                    startWalk(myTarget);
                    Debug.Log("Walking");
                }
                else
                {
                    Debug.Log("Idling");
                    idleForTime(Random.Range(3, 8));
                }


            }
            else
            {
                

                if (arrived)
                {
                    decided = false;

                }
                // This code below helps update the NPC walk animation
                // if there fear level has changed
                else
                {
                    /*
                    timer += Time.deltaTime;
                    if (timer > 0.1)
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
                    */
                }
                
            }
            if (myAnimator.GetBool("walk") &&  Vector3.Distance(transform.position, myAgent.destination) <= 3)
            {
                arrived = true;
            }


        }
        else
        {

        }


    }

    // This handles fear specific triggers like heart attacks and fetal position
    // This also stops the NPC from moving

    /* Fear walk levels
     * 
     * 0-249 casual
     * 250-749 normal
     * 750-1249 walk scared
     * 1250-1999 run scared
     * 
     * */
     public void setAllAnimBoolsToBool(bool newBool)
    {
        myAnimator.SetBool("idle", newBool);
        myAnimator.SetBool("walk", newBool);
        myAnimator.SetBool("surprised", newBool);
        myAnimator.SetBool("surprisedDuck", newBool);
        myAnimator.SetBool("deadPose", newBool);
        myAnimator.SetBool("heartAttack", newBool);
        myAnimator.SetBool("fetalPosition", newBool);

    }
    public void checkStates()
    {

        if (myCurrentFear >= 2500)
        {
            setDead();
        }
        else if (myCurrentFear < 2500 && myCurrentFear >= 2000) // fetal position
        {
            setFetalPosition();

        } else if (myAnimator.GetBool("walk")) {
            if (myCurrentFear >= 1250) // run scared
            {
                myAnimator.runtimeAnimatorController = highFearController;
            }
            else if (myCurrentFear >= 750) // walk scared
            {
                myAnimator.runtimeAnimatorController = medFearController;
            }
            else if (myCurrentFear >= 250) // walk normal
            {
                myAnimator.runtimeAnimatorController = lowFearController;
            }
            else // walk casual
            {
                myAnimator.runtimeAnimatorController = noFearController;
            }
            
        }


    }

    public void saySomethingGeneral(AudioClip[] general)
    {
        int rando = Random.Range(0, general.Length);
        myAudioSource.PlayOneShot(general[rando]);
    }
    //Sets the NPC to dead by stopping movement, playing animation, and setting fear to max
    public void setDead()
    {
        if (!myAnimator.GetBool("deadPose"))
        {
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("deadPose", true);
            myAnimator.SetTrigger("fireTransition");
            saySomethingGeneral(deathCrys);
            myAnimator.fireEvents = false;
            StopAllCoroutines();
            myAnimator.SetInteger("fearFactor", 2500);
            checkFear();
            updateFearSlider();
        }

    }
    public void setFetalPosition()
    {
        if (!myAnimator.GetBool("fetalPosition"))
        {
            stopped = true;
            myAgent.isStopped = true;

            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("fetalPosition", true);
            myAnimator.SetTrigger("fireTransition");
            saySomethingGeneral(fetalCrys);
            StopAllCoroutines();
            myAnimator.fireEvents = false;
        }
    }
    // This plays this surprised animation on the NPC when called
    // You need to create a new method for every new NPC reaction and make sure they can be called at any point and
    // pathfinding/walking is paused and then resumed
    public void setSurprised()
    {
        if (!myAgent.isStopped)
        {
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("surprised", true);
            myAnimator.SetTrigger("fireTransition");
            myAnimator.fireEvents = false;
            IEnumerator coro = surprisedIenum(3.75f);
            //StopAllCoroutines();
            StartCoroutine(coro);
        }

    }

    public void setSurprisedDuck()
    {
        if (!myAgent.isStopped)
        {
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("surprisedDuck", true);
            myAnimator.SetTrigger("fireTransition");
            addFear(250);
            IEnumerator coro = idling(2);
            StartCoroutine(coro);
            saySomethingGeneral(generalQuotes);
        }
    }

    // This picks a location in the list of possible targets
    public void setNewTarget(string name)
    {
        try
        {
            decided = true;
            arrived = false;
            myTarget = GameObject.Find(name).GetComponent<Transform>();
        }
        catch
        {
        }

    }

    // Starts the walk animation and sets destination
    public void startWalk(Transform loc)
    {
        myAgent.SetDestination(myTarget.position);
        setAllAnimBoolsToBool(false);
        myAnimator.SetBool("walk", true);
        myAnimator.SetTrigger("fireTransition");
        myAgent.speed = 3.5f;
    }

    // Keeps the NPC idling for the parameter time in seconds
    public void idleForTime(float time)
    {

        setAllAnimBoolsToBool(false);
        myAnimator.SetBool("idle", true);
        myAnimator.SetTrigger("fireTransition");
        IEnumerator coro = idling(time);
        StartCoroutine(coro);

    }

    // coroutine for idling/ or generic waiting
    private IEnumerator idling(float time)
    {
        yield return new WaitForSeconds(time);
        setAllAnimBoolsToBool(false);
        myAgent.destination = gameObject.transform.position;
        stopped = false;
        decided = false;
        arrived = true;
        myAgent.isStopped = false;
        myAnimator.fireEvents = true;
        

    }

    // coroutine for surprised, BUGGY
    private IEnumerator surprisedIenum(float time)
    {

        yield return new WaitForSeconds(time);
        stopped = false;
        myAgent.isStopped = false;
        myAnimator.fireEvents = true;
        addFear(500);
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

    }
}
