 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcWander : MonoBehaviour
{
    public GameObject[] locations;
    public float threshold;

    private Vector3 currentDest;
    private NavMeshAgent agent;
    private bool arrived;
    private Animator anim;

    private bool isReactingToDecal;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        chooseNewDest();
        currentDest = new Vector3(0, 0, 0);
        isReactingToDecal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReactingToDecal)
        {
            if (!arrived)
            {
                if (Vector3.Distance(transform.position, agent.destination) <= threshold)
                {
                    arrived = true;
                    anim.SetTrigger("Reset");
                    Invoke("chooseNewDest", 5f);
                }
            }
        }
    }

    void chooseNewDest ()
    {
        isReactingToDecal = false;
        arrived = false;
        do
        {
            int i = Random.Range(0, locations.Length);
            agent.SetDestination(locations[i].transform.position);
        } while (agent.destination == currentDest);
        currentDest = agent.destination;
        int speed = Random.Range(0, 2);
        if(speed == 0)
        {
            anim.SetTrigger("Walk");
            agent.speed = 3.5f;
        }else
        {
            anim.SetTrigger("Run");
            agent.speed = 5f;
        }
    }
    public void reactToDecal(string reaction)
    {
        isReactingToDecal = true;
        anim.SetTrigger(reaction);
        Invoke("chooseNewDest", 2f);

    }
}
