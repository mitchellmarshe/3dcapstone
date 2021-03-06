﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReactiveAIMK2 : MonoBehaviour
{
    public NavMeshAgent myAgent;
    public Animator myAnimator;
    private int myOldFear;
    private int myCurrentFear;
    private bool decided;
    private Transform myTarget;
    private int numZones = 10;
    public bool stopped;
    private bool arrived;
    private float timer;


    private Slider fearSlider;
    private Global global;

    public AudioSource myAudioSource;
    public AudioSource footstepsSource;

    public AnimatorOverrideController noFearController;
    public AnimatorOverrideController lowFearController; 
    public AnimatorOverrideController medFearController;
    public AnimatorOverrideController highFearController;

    [Header("Audio Clips")]
    public AudioClip walkSound;

    public AudioClip runSound;

    public AudioClip[] generalQuotes;

    public AudioClip[] deathCrys;

    public AudioClip[] fetalCrys;

    public AudioClip[] iSeeDeadPeople;

    public AudioClip[] lowFearThoughts;
    public AudioClip[] midFearThoughts;
    public AudioClip[] highFearThoughts;

    public AudioClip[] fearImmuneResponses;

    public AudioClip cough;
    public AudioClip coughingToDeath;

    public AudioClip lightLaugh;

    public SkinnedMeshRenderer mySkinMeshRend;
    public Texture human_crying;
    public Texture human_dead;
    public Texture human_frowning;
    public Texture human_idle;
    public Texture human_scared;
    public Texture human_smiling;
    public Texture human_screaming;
    public Texture human_surprised;

    //private Queue<GameObject> recentDecals = new Queue<GameObject>();
    //private float decalCounter = 0;

    private float soundCounter = 0;
    private int randomTick = 0;

    private Texture lastFace;
    private bool reactionFace = false;

    int coughCount = 0;

    private List<GameObject> closeDecals = new List<GameObject>();
    private bool seesObj = false;
    private bool closeMiss = false;
    private float thrownCounter = 2f;

    private List<GameObject> nearbyNPCs = new List<GameObject>();
    private List<GameObject> deadNPCs = new List<GameObject>();

    private List<GameObject> interactionSpots = new List<GameObject>();

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
        randomTick = Random.Range(4, 11);
        lastFace = human_smiling;
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

        foreach(GameObject spot in GameObject.FindGameObjectsWithTag("interactionSpot"))
        {
            interactionSpots.Add(spot);
        }
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
        soundCounter += Time.deltaTime;
        /*
        decalCounter += Time.deltaTime;
        if(decalCounter >= 15)
        {
            if (recentDecals.Count > 0)
            {
                recentDecals.Clear();
            }
            decalCounter = 0;
        } 
        */
        
        thrownCounter -= Time.deltaTime;
        if(thrownCounter <= 0)
        {
            thrownCounter = 2f;
            closeMiss = false;
            seesObj = false;
        }

        myOldFear = myCurrentFear;
        myCurrentFear = checkFear();
        updateFearSlider();

        checkStates();
        if (!myAgent.isStopped)
        {
            checkCloseDecals();
        }
        //The stopped state is true when the NPC suffers a disabilitating
        // animation such as dead, heart attack, fetal position, ect
        if (!stopped)
        {
            
            checkForDeadNPCs();
            if (lastFace != mySkinMeshRend && reactionFace)
            {
                reactionFace = false;
                mySkinMeshRend.material.mainTexture = lastFace;
            } else
            {
                lastFace = mySkinMeshRend.material.mainTexture;
            }

            //Decided is true when the NPC is either idling or walking somewhere
            if (!decided)
            {
                //Debug.Log("I am going to" + myAgent.destination);
                //This code below decides wether to idle or walk to a new target
                decided = true;
                arrived = false;
                setAllAnimBoolsToBool(false);
                StopAllCoroutines();
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    //rand = Random.Range(1, numZones + 1);
                    rand = Random.Range(0, interactionSpots.Count);
                    //Debug.Log(rand);
                    //string targetname = "idlezone" + rand;
                    while (interactionSpots[rand].GetComponent<InteractionSpot>().isOccupied())
                    {
                        rand = Random.Range(0, interactionSpots.Count);
                    }

                    setNewTarget(interactionSpots[rand].transform);
                    StartWalk(myTarget);
                    //Debug.Log("Walking");
                }
                else
                {
                    //Debug.Log("Idling");
                    idleForTime(Random.Range(3, 8));
                }

                //soundCounter = 0;
                //randomTick = Random.Range(4, 11);
            }
            else
            {
                

                if (arrived)
                {
                    decided = false;
                    //gameObject.transform.LookAt(myTarget.GetChild(0));
                    idleForTime(2);
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
            //Debug.Log("my Loc: " + transform.position + " | MyDestination: " + myAgent.destination);
            //Debug.Log(Vector3.Distance(transform.position, myAgent.destination));
            if (myAnimator.GetBool("walk") &&  Vector3.Distance(transform.position, myAgent.destination) <= 3)
            {
                //myAgent.Move(Vector3.MoveTowards(transform.position, myAgent.destination, 1));
                //myAgent.areaMask = NavMesh.AllAreas;
                //myAgent.ResetPath();
                //myAgent.destination = myTarget.position;

                //NavMeshPath newPath;
                //myAgent.CalculatePath(myTarget.position, out newPath);
                //myAgent.path = NavMeshPath;
                
                arrived = true;
                //myAgent.Warp(myTarget.position);
                //gameObject.GetComponent<Rigidbody>().MovePosition(myTarget.position);
                //gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, myTarget.GetChild(0).transform.rotation, 360f));
                //gameObject.transform.Translate(myTarget.position - transform.position);
                //Vector3.MoveTowards(transform.position, myTarget.position, 4f);
                
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
        myAnimator.SetBool("coughingDeath", newBool);
        myAnimator.SetBool("cough", newBool);
        myAnimator.SetBool("laughLight", newBool);
        myAnimator.SetBool("trapped", newBool);
        myAnimator.SetBool("startHypno", newBool);
        myAnimator.SetBool("endHypno", newBool);
        myAnimator.SetBool("pentagram", newBool);

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

        } else if (myAnimator.GetBool("walk") || myAnimator.GetBool("idle") && myCurrentFear < 2000) {
            if (myCurrentFear >= 1250) // run scared
            {
                if(myAnimator.runtimeAnimatorController != highFearController) {
                 myAnimator.runtimeAnimatorController = highFearController;
                    
                }
                if(mySkinMeshRend.material.mainTexture != human_screaming)
                {
                    mySkinMeshRend.material.mainTexture = human_screaming;
                    lastFace = human_screaming;
                }
            }
            else if (myCurrentFear >= 750){ // walk scared
            {
                if (myAnimator.runtimeAnimatorController != medFearController)
                    myAnimator.runtimeAnimatorController = medFearController;
                    
                }
                if (mySkinMeshRend.material.mainTexture != human_scared)
                {
                    mySkinMeshRend.material.mainTexture = human_scared;
                    lastFace = human_scared;
                }
            }
            else if (myCurrentFear >= 250){ // walk normal
            {
                if (myAnimator.runtimeAnimatorController != lowFearController)
                    myAnimator.runtimeAnimatorController = lowFearController;
                    
                }
                if (mySkinMeshRend.material.mainTexture != human_idle)
                {
                    mySkinMeshRend.material.mainTexture = human_idle;
                    lastFace = human_idle;
                }
            }
            else // walk casual
            {
                if (myAnimator.runtimeAnimatorController != noFearController)
                {
                    myAnimator.runtimeAnimatorController = noFearController;
                    
                }
                if (mySkinMeshRend.material.mainTexture != human_smiling)
                {
                    mySkinMeshRend.material.mainTexture = human_smiling;
                    lastFace = human_smiling;
                }
            }
            
        }


    }

    public void saySomethingGeneral(AudioClip[] general)
    {
        if (global.canISpeak())
        {
            int rando = Random.Range(0, general.Length);
            myAudioSource.PlayOneShot(general[rando]);
        }
        

    }
    //Sets the NPC to dead by stopping movement, playing animation, and setting fear to max
    public void setDead()
    {
        if (!myAnimator.GetBool("deadPose"))
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("deadPose", true);
            myAnimator.SetTrigger("fireTransition");
            saySomethingGeneral(deathCrys);
            myAnimator.fireEvents = false;
            gameManager.instance.killNPC();
            myAnimator.SetInteger("fearFactor", 2500);
            checkFear();
            updateFearSlider();
            mySkinMeshRend.material.mainTexture = human_dead;
            reactionFace = true;
        }

    }

    public void setHeartAttack()
    {
        if (!myAnimator.GetBool("heartAttack") && !myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("heartAttack", true);
            myAnimator.SetTrigger("fireTransition");
            saySomethingGeneral(deathCrys);
            IEnumerator coro = coughingDeathCoro(4);
            StartCoroutine(coro);
            mySkinMeshRend.material.mainTexture = human_screaming;
            reactionFace = true;
            gameManager.instance.killNPC();
        }
    }

    public void setFetalPosition()
    {
        if (!myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;

            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("fetalPosition", true);
            myAnimator.SetTrigger("fireTransition");
            saySomethingGeneral(fetalCrys);
            mySkinMeshRend.material.mainTexture = human_crying;
            reactionFace = true;
            //gameManager.instance.killNPC();
            //myAnimator.fireEvents = true;
            //Debug.Log("end of fetal call");
        }

        //myAnimator.fireEvents = false;
    }
    // This plays this surprised animation on the NPC when called
    // You need to create a new method for every new NPC reaction and make sure they can be called at any point and
    // pathfinding/walking is paused and then resumed
    public void setSurprised()
    {
        if (!myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            addFear(250);
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("surprised", true);
            myAnimator.SetTrigger("fireTransition");
            
            //myAnimator.fireEvents = false;
            //IEnumerator coro = surprisedIenum(3.75f);
            //StopAllCoroutines();
            IEnumerator coro = idling(3.5f);
            StartCoroutine(coro);
            saySomethingGeneral(generalQuotes);
            mySkinMeshRend.material.mainTexture = human_surprised;
            reactionFace = true;
        } else
        {
            addFear(250);
        }

    }

    public void setSurprised(AudioClip[] audio)
    {
        if (!myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            addFear(250);
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("surprised", true);
            myAnimator.SetTrigger("fireTransition");

            //myAnimator.fireEvents = false;
            //IEnumerator coro = surprisedIenum(3.75f);
            //StopAllCoroutines();
            IEnumerator coro = idling(3.5f);
            StartCoroutine(coro);
            saySomethingGeneral(audio);
            mySkinMeshRend.material.mainTexture = human_surprised;
            reactionFace = true;
        }
        else
        {
            addFear(250);
        }

    }

    public void setSurprisedDuck()
    {
        if (!myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("surprisedDuck", true);
            myAnimator.SetTrigger("fireTransition");
            addFear(250);
            IEnumerator coro = idling(2);
            StartCoroutine(coro);
            saySomethingGeneral(generalQuotes);
            mySkinMeshRend.material.mainTexture = human_surprised;
            reactionFace = true;
        } else
        {
            addFear(250);
        }
    }

    public void setCough()
    {
        if (!myAgent.isStopped)
        {
            if (coughCount < 4)
            {
                footstepsSource.Stop();
                StopAllCoroutines();
                coughCount++;
                stopped = true;
                myAgent.isStopped = true;
                setAllAnimBoolsToBool(false);
                myAnimator.SetBool("cough", true);
                myAnimator.SetTrigger("fireTransition");
                addFear(250);
                IEnumerator coro = idling(2);
                StartCoroutine(coro);
                myAudioSource.PlayOneShot(cough);
                mySkinMeshRend.material.mainTexture = human_scared;
                reactionFace = true;
            } else
            {
                setCoughToDeath();
            }
        } else
        {
            addFear(250);
        }
    }

    public void setCoughToDeath()
    {
        if (!myAnimator.GetBool("coughingDeath") && !myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("coughingDeath", true);
            myAnimator.SetTrigger("fireTransition");
            IEnumerator coro = coughingDeathCoro(4);
            StartCoroutine(coro);
            myAudioSource.PlayOneShot(coughingToDeath);
            mySkinMeshRend.material.mainTexture = human_screaming;
            reactionFace = true;
            gameManager.instance.killNPC();
        }
    }

    public void setLaughLight()
    {
        if (!myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("laughLight", true);
            myAnimator.SetTrigger("fireTransition");
            IEnumerator coro = idling(2);
            StartCoroutine(coro);
            myAudioSource.PlayOneShot(lightLaugh);
            mySkinMeshRend.material.mainTexture = human_smiling;
            reactionFace = true;
        }
    }

    public void setHypnotized()
    {
        if (!myAgent.isStopped)
        {

            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("startHypno", true);
            myAnimator.SetTrigger("fireTransition");
            saySomethingGeneral(generalQuotes);
            mySkinMeshRend.material.mainTexture = human_surprised;
            reactionFace = true;
        }
    }

    public void endHypnotized()
    {
        //if (!myAgent.isStopped)
        //{
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("endHypno", true);
            myAnimator.SetTrigger("fireTransition");
            IEnumerator coro = idling(2);
            StartCoroutine(coro);
            saySomethingGeneral(generalQuotes);
            mySkinMeshRend.material.mainTexture = human_frowning;
            reactionFace = true;
        //}
        
    }

    public void setPentagram(Transform decal)
    {
        if (!myAgent.isStopped)
        {
            footstepsSource.Stop();
            StopAllCoroutines();
            stopped = true;
            myAgent.isStopped = true;
            setAllAnimBoolsToBool(false);
            myAnimator.SetBool("pentagram", true);
            myAnimator.SetTrigger("fireTransition");
            IEnumerator coro = pentagraming(100f, decal);
            StartCoroutine(coro);
            saySomethingGeneral(generalQuotes);
            mySkinMeshRend.material.mainTexture = human_screaming;
            reactionFace = true;
        }
    }

    private IEnumerator pentagraming(float times, Transform target)
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {


            Material npcMat = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
            Color newColor = new Color(npcMat.color.r + .01f, npcMat.color.g - .01f, npcMat.color.b - .01f, npcMat.color.a - .01f);
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
            //Vector3 newv3 = new Vector3(0.02f, 0.02f, 0.02f);
            // Vector3 myForward = transform.forward;
            //myForward.Scale(newv3);
            //transform.LookAt(target);
            myAgent.Move(transform.forward * (Vector3.Distance(transform.position, target.position) / 100));
            Debug.Log("In pentagraming");
            if (times > 0)
            {
                yield return new WaitForSeconds(.01f);
                IEnumerator coro = pentagraming(times - 1, target);
                StartCoroutine(coro);
            }
            else
            {
                Debug.Log("Destroy");
                gameManager.instance.killNPC();
                Destroy(gameObject);
            }
        }


    }



    public bool getPath(Transform loc, ref NavMeshPath newPath)
    {
        return myAgent.CalculatePath(loc.position, newPath);

    }

    public float GetPathLength(NavMeshPath path)
    {
        float resultLength = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                resultLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return resultLength;
    }

    // This picks a location in the list of possible targets
    public void setNewTarget(string name)
    {
        try
        {
            Debug.Log("setNewTarget");
            StopAllCoroutines();
            setAllAnimBoolsToBool(false);
            decided = true;
            arrived = false;
            myTarget = GameObject.Find(name).GetComponent<Transform>();
            stopped = false;
            myAgent.isStopped = false;
        }
        catch
        {
        }

    }

    public void setNewTarget(Transform loc)
    {
        try
        {
            Debug.Log("setNewTarget");
            StopAllCoroutines();
            setAllAnimBoolsToBool(false);
            decided = true;
            arrived = false;
            myTarget = loc;
            stopped = false;
            myAgent.isStopped = false;
        }
        catch
        {
        }

    }

    // Starts the walk animation and sets destination
    public void StartWalk(Transform loc)
    {
        if (myCurrentFear >= 1250) //running
        {
            footstepsSource.clip = runSound;
        }else //walking
        {
            footstepsSource.clip = walkSound;
        }
        footstepsSource.Play();
        myAgent.SetDestination(myTarget.position);
        stopped = false;
        myAgent.isStopped = false;
        setAllAnimBoolsToBool(false);
        myAnimator.SetBool("walk", true);
        myAnimator.SetTrigger("fireTransition");
        myAgent.speed = 3.5f;
        
    }

    // Keeps the NPC idling for the parameter time in seconds
    public void idleForTime(float time)
    {
        footstepsSource.Stop();
        stopped = true;
        //myAgent.isStopped = true;
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

    private IEnumerator coughingDeathCoro(float time)
    {
        yield return new WaitForSeconds(time);
        setAllAnimBoolsToBool(false);
        myAgent.destination = gameObject.transform.position;
        stopped = false;
        decided = false;
        arrived = true;
        myAgent.isStopped = false;
        myAnimator.fireEvents = true;
        setDead();


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

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("other is " + other.gameObject.name);
        
        if (other.gameObject.tag == "Decal")
        {
            Sprite tmp = other.gameObject.GetComponent<SpriteRenderer>().sprite;
            closeDecals.Add(other.gameObject);
            //Debug.Log("NPC Detected");
            
        }
        if(other.gameObject.tag == "NPC" && !deadNPCs.Contains(other.gameObject))
        {
            nearbyNPCs.Add(other.gameObject);
        }
        if (!seesObj && !closeMiss && other.isTrigger == false)
        {
            LayerMask newMask = LayerMask.GetMask("walls");
            if (other.gameObject.tag == "Ignore" && !Physics.Linecast(transform.position, other.gameObject.transform.position, newMask, QueryTriggerInteraction.UseGlobal))
            {
                ItemActionInterface actions = other.GetComponent<ItemActionInterface>();
                Rigidbody body = other.attachedRigidbody;
                if (actions != null && body != null && body.velocity.magnitude > 2)
                {
                    seesObj = true;
                    setSurprised();
                    Debug.Log("Just seen it");
                }
            }
        }

    }

    public void OnCloseTriggerEnter(Collider other)
    {
        if (!closeMiss && other.isTrigger == false && other.gameObject.tag != "Decal")
        {
            //Debug.Log("Close Miss!!");
            LayerMask newMask = LayerMask.GetMask("walls");
            if (other.gameObject.tag == "Ignore" && !Physics.Linecast(transform.position, other.gameObject.transform.position, newMask, QueryTriggerInteraction.UseGlobal))
            {
                
                ItemActionInterface actions = other.GetComponent<ItemActionInterface>();
                Rigidbody body = other.attachedRigidbody;
                if (actions != null && body != null && body.velocity.magnitude > 2)
                {
                    if (seesObj)
                    {
                        //Debug.Log("I saw the obj first");
                        StopAllCoroutines();
                        stopped = true;
                        myAgent.isStopped = true;
                        setAllAnimBoolsToBool(false);
                        myAnimator.SetBool("surprisedDuck", true);
                        myAnimator.SetTrigger("fireTransition");
                        addFear(250);
                        IEnumerator coro = idling(2);
                        StartCoroutine(coro);
                        //saySomethingGeneral(generalQuotes);
                        mySkinMeshRend.material.mainTexture = human_surprised;
                        reactionFace = true;
                    } else
                    {
                        setSurprisedDuck();
                    }
                    closeMiss = true;
                    
                    
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("other is " + other.gameObject.name);
        if (other.gameObject.tag == "Decal" && closeDecals.Contains(other.gameObject))
        {
            closeDecals.Remove(other.gameObject);
            //Debug.Log("NPC Detected");

        }
        if (other.gameObject.tag == "NPC" && nearbyNPCs.Contains(other.gameObject))
        {
            nearbyNPCs.Remove(other.gameObject);
        }
    }

    public void checkCloseDecals()
    {
        int i = 0;
        while (i < closeDecals.Count)
        {

            if (closeDecals[i] != null )//&& !recentDecals.Contains(closeDecals[i]))
            {

                LayerMask newMask = LayerMask.GetMask("walls");
                if (Physics.Linecast(transform.position, closeDecals[i].transform.position, newMask, QueryTriggerInteraction.UseGlobal))
                {
                    i++;
                }
                else
                {
                    //transform.LookAt(closeDecals[i].transform);
                    reactToDecal(closeDecals[i]);
                    

                    //recentDecals.Enqueue(closeDecals[i]);
                    closeDecals.RemoveAt(i);
                }
            }//else if(closeDecals[i] != null && recentDecals.Contains(closeDecals[i]))
            //{
                //saySomethingGeneral(fearImmuneResponses);
            //}
            else
            {
                closeDecals.RemoveAt(i);
            }
            
        }
    }

    public void checkForDeadNPCs()
    {
        for(int i = 0; i < nearbyNPCs.Count; i++)
        {
            //Debug.Log("Other NPC fear is " + nearbyNPCs[i].GetComponent<ReactiveAIMK2>().myCurrentFear);
            if (nearbyNPCs[i] != null)
            {
                if (nearbyNPCs[i].GetComponent<ReactiveAIMK2>().myCurrentFear >= 2500)
                {
                    LayerMask newMask = LayerMask.GetMask("walls");
                    if (!Physics.Linecast(transform.position, nearbyNPCs[i].transform.position, newMask, QueryTriggerInteraction.UseGlobal))
                    {
                        //transform.LookAt(nearbyNPCs[i].transform);
                        setSurprised(iSeeDeadPeople);

                        deadNPCs.Add(nearbyNPCs[i]);
                        nearbyNPCs.RemoveAt(i);
                        i = nearbyNPCs.Count;

                    }

                }
            } else
            {
                nearbyNPCs.RemoveAt(i);
                i = nearbyNPCs.Count;
            }
        }
    }

    public void reactToDecal(GameObject obj)
    {
        Sprite tmp = obj.GetComponent<SpriteRenderer>().sprite;
        //Debug.Log("NPC Detected");
        if (tmp.name == "redrum")
        {
            if (myAnimator.GetInteger("fearFactor") >= 2250)
            {
                if(global.missionHandler.task1Type == 1 && global.missionHandler.task1Name == tmp.name)
                {
                    global.missionHandler.killed4Task(1);
                } if (global.missionHandler.task2Type == 1 && global.missionHandler.task2Name == tmp.name)
                {
                    global.missionHandler.killed4Task(2);
                } if(global.missionHandler.task3Type == 1 && global.missionHandler.task3Name == tmp.name)
                {
                    global.missionHandler.killed4Task(3);
                }
            }
            setSurprised();
        }
        else if (tmp.name == "unicorn")
        {
            setLaughLight();
        }
        else if (tmp.name == "skull")
        {
            if (coughCount >= 4)
            {
                if (global.missionHandler.task1Type == 1 && global.missionHandler.task1Name == tmp.name)
                {
                    global.missionHandler.killed4Task(1);
                }
                else if (global.missionHandler.task2Type == 1 && global.missionHandler.task2Name == tmp.name)
                {
                    global.missionHandler.killed4Task(2);
                }
                else if (global.missionHandler.task3Type == 1 && global.missionHandler.task3Name == tmp.name)
                {
                    global.missionHandler.killed4Task(3);
                }
            }
            setCough();
        }
        else if (tmp.name == "hypnolizard")
        {
            setHypnotized();
            obj.GetComponent<EndHypnoOnDestroy>().hypnoNPCS.Add(gameObject.GetComponent<ReactiveAIMK2>());

        }
        else if (tmp.name == "pentagram")
        {

            if (global.missionHandler.task1Type == 1 && global.missionHandler.task1Name == tmp.name)
            {
                global.missionHandler.killed4Task(1);
            }
            else if (global.missionHandler.task2Type == 1 && global.missionHandler.task2Name == tmp.name)
            {
                global.missionHandler.killed4Task(2);
            }
            else if (global.missionHandler.task3Type == 1 && global.missionHandler.task3Name == tmp.name)
            {
                global.missionHandler.killed4Task(3);
            }
            
            setPentagram(obj.transform);
        }
        else
        {
            Debug.Log("decal has non-decal image");
        }
    }



}
