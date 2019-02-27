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
    }

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



        if (!stopped)
        {
            checkStates();


            if (!decided)
            {
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
    
    public void setDead()
    {
        stopped = true;
        myAgent.isStopped = true;
        myAnimator.ResetTrigger("walk");
        myAnimator.ResetTrigger("idle");
        myAnimator.SetTrigger("dead");
        myAnimator.fireEvents = false;
        StopAllCoroutines();
    }

    public void setSurprised()
    {
        stopped = true;
        myAgent.isStopped = true;
        myAnimator.SetTrigger("surprised");
        myAnimator.fireEvents = false;
        IEnumerator coro = surprisedIenum(3.75f);
        StopAllCoroutines();
        StartCoroutine(coro);
        

    }

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

    public void startWalk(Transform loc)
    {
        myAgent.SetDestination(myTarget.position);
        myAnimator.ResetTrigger("idle");
        myAnimator.SetTrigger("walk");
        myAgent.speed = 3.5f;
        Debug.Log("started walk");
    }

    public void idleForTime(float time)
    {
        Debug.Log("idle for time " + time);
        myAnimator.ResetTrigger("walk");
        stopped = true;
        myAnimator.SetTrigger("idle");
        IEnumerator coro = idling(time);
        StartCoroutine(coro);

    }

    private IEnumerator idling(float time)
    {
        yield return new WaitForSeconds(time);
        stopped = false;
        myAgent.isStopped = false;
        myAnimator.fireEvents = true;
        
        Debug.Log("Done idling");
    }

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

    public int checkFear()
    {
        return myAnimator.GetInteger("fearFactor");
    }

    public void addFear(int num)
    {
        int tmp =  myAnimator.GetInteger("fearFactor");
        myAnimator.SetInteger("fearFactor", tmp + num);
    }
}
