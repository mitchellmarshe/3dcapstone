using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PhoneActions : ItemActionInterface
{
    public string[] myActionNames;
    private Haunt myHaunt;
    private float myTime = 0f;
    private List<GameObject> scaredNPCS = new List<GameObject>();
    private Global global;
    private bool ringing;
    private bool answering;
    private AudioSource myAudio;
    public AudioClip ring;
    public AudioClip[] scary_msgs;
    public Transform npcPosition;
    private GameObject selectedNPC;
    public AnimationClip ringAnimClip;
    private Animator animator;

    private float dist = 1000000;
    private float maxDis;

    private bool arrived = false;


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        myActionNames = new string[] { "Haunt Call", "...", "...", "..." };
        myHaunt = GameObject.Find("Player").GetComponent<Haunt>();
        global = GameObject.Find("Global").GetComponent<Global>();
        states = new bool[4] { true, false, false, false };
        myAudio = gameObject.GetComponent<AudioSource>();
        maxDis = Vector3.Distance(transform.position, npcPosition.position) + 3f;
    }

    private void Update()
    {
        if (ringing && !answering)
        {
            myTime += Time.deltaTime;
            if (myTime >= ring.length-1)
            {
                ringing = false;
                animator.SetBool("ringing", ringing);
                myTime = 0;
            }
            if (selectedNPC == null)
            {
                for (int i = 0; i < scaredNPCS.Count; i++)
                {
                    ReactiveAIMK2 npc = scaredNPCS[i].GetComponent<ReactiveAIMK2>();
                    NavMeshPath newPath = new NavMeshPath();
                    //bool validPath = npc.myAgent.CalculatePath(npcPosition.position, newPath);

                    if (!npc.stopped)
                    {
                        float tmpDist = Vector3.Distance(transform.position, npcPosition.position);
                        if (tmpDist < dist)
                        {
                            dist = tmpDist;
                            selectedNPC = npc.gameObject;

                        }
                        else
                        {
                            Debug.Log("Longer than infinity");
                        }
                    }
                    else
                    {
                    }

                }
            } else
            {
                if(selectedNPC.GetComponent<NavMeshAgent>().destination != npcPosition.position)
                {
                    selectedNPC = null;
                }
            }
            if (selectedNPC != null)
            {
                //Debug.Log("SET NEW PATH");
                selectedNPC.GetComponent<ReactiveAIMK2>().setNewTarget(npcPosition);
                answering = true;
                dist = 1000000;
            }
        } else if (answering && arrived)
        {
            ringing = false;
            animator.SetBool("ringing", ringing);
            answering = false;
            arrived = false;
            myAudio.Stop();
            myAudio.PlayOneShot(randomMsg());
            selectedNPC.GetComponent<ReactiveAIMK2>().setHeartAttack();

        } else if (answering && selectedNPC != null)
        {
            if(selectedNPC.GetComponent<ReactiveAIMK2>().stopped)
            {
                answering = false;
                
            } else if (selectedNPC.GetComponent<NavMeshAgent>().destination != npcPosition.position)
            {
                selectedNPC = null;
            }
            else if(selectedNPC.GetComponent<ReactiveAIMK2>().myAgent.remainingDistance <= 3)
            {
                arrived = true;
            }
        }
    }

    public override void callAction1()
    {
        ringing = true;
        myTime = 0f;
        myAudio.PlayOneShot(ring);
        selectedNPC = null;
        animator.SetBool("ringing", ringing);




    }

    public void printDebug()
    {
        Debug.Log("ringing : " + ringing);
        Debug.Log("answering : " + answering);
        Debug.Log("arrived : " + arrived);
        if (selectedNPC != null)
        {
            Debug.Log("selectedNPC is : " + selectedNPC.name);
        } else
        {
            Debug.Log("selectedNPC : NULL");
        }
        Debug.Log("NPC count : " + scaredNPCS.Count);
    }

    public AudioClip randomMsg()
    {
        int rand = Random.Range(0,scary_msgs.Length-1);
        return scary_msgs[rand];
    }

    public override void callAction2()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!scaredNPCS.Contains(other.gameObject))
            {
                scaredNPCS.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (scaredNPCS.Contains(other.gameObject))
            {
                scaredNPCS.Remove(other.gameObject);
            }
        }
    }

    public override void callAction3()
    {
        //ItemActionInterface tmp = gameObject.GetComponent<ItemActionInterface>();
        //myHaunt.prepForHaunt(gameObject, tmp);

        //global.possessing = false;
        //myHaunt.unPossess();
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