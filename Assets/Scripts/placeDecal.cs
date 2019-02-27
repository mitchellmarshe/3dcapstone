using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeDecal : MonoBehaviour
{
    private Global global;
    private Vector3 origin;
    private Transform decalTransform;
    private Camera camComponent;
    private bool isSnapped;
    private GameObject snappedObject = null;
    private bool placed = false;
    private Sprite lastSprite;
    public Sprite invalid;
    // Start is called before the first frame update
    private string[] redrumReactions = new string[] {"Terror"};
    private string[] unicornReactions = new string[] {"Terror"};
    private bool npcDetected;
    private bool distorted;
    private float timeHolder;
    private List<GameObject> distortedNPCS = new List<GameObject>();

    void Awake()
    {
        distorted = false;
        npcDetected = false;
        timeHolder = 0;
        isSnapped = false;
        global = GameObject.Find("Global").GetComponent<Global>();
        decalTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (distortedNPCS.Count > 0)
        {
            distorted = true;
        } else
        {
            distorted = false;
        }

        if (global.placingDecal)
        {
            
            Vector3 rayOrigin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit rayHit;
            //int layerMask = LayerMask.GetMask("Ignore Raycast"); // this is unnecessesary because raycast autoblocks objects belonging to the built-in layer Ignore Raycast
            //layerMask = ~layerMask;
            if (Physics.Raycast(rayOrigin, camComponent.transform.forward, out rayHit, 4)) // 3 is the length of the ray drawn
            {
                GameObject other = rayHit.collider.gameObject;
                Vector3 hitPoint = rayHit.point;
                Vector3 hitNormal = rayHit.normal;
                decalTransform.position = hitPoint;
                if (!isSnapped) {
                    isSnapped = true;
                    decalTransform.LookAt(hitPoint-hitNormal);
                    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = lastSprite;



                }
            } else
            {
                isSnapped = false ;
                origin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                Vector3 newPos = origin + (camComponent.transform.forward * 3);
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = invalid;
                decalTransform.position = newPos;
                decalTransform.rotation = camComponent.gameObject.transform.rotation;
            }
        } else if (distorted)
        {
            timeHolder += Time.deltaTime;
            if (timeHolder >= 0.5f)
            {

                timeHolder = 0;
                for (int i = 0; i < distortedNPCS.Count; i++)
                {
                    if (lastSprite.name == "unicorn")
                    {
                        //other.GetComponent<npcWander>().reactToDecal(pickReaction(unicornReactions));
                        distortedNPCS[i].gameObject.GetComponent<ReactiveNPC>().setDead();

                    }
                    else if (lastSprite.name == "redrum")
                    {
                        //other.GetComponent<npcWander>().reactToDecal(pickReaction(redrumReactions));
                        distortedNPCS[i].gameObject.GetComponent<ReactiveNPC>().setSurprised();
                        //other.gameObject.GetComponent<ReactiveNPC>().addFear(500);
                    }
                }
            }
        }
    }


    public void findSpot(Sprite decal, GameObject camera)
    {
        lastSprite = decal;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = decal;
        camComponent = camera.GetComponent<Camera>();
        global.placingDecal = true;
        npcDetected = false;
    }


    public void placeAtSpot()
    {
        if (isSnapped)
        {
            global.placingDecal = false;
            placed = true;
        }
    }

    public void removeDecal()
    {
        if(!global.placingDecal && placed)
        {
            placed = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (!distortedNPCS.Contains(other.gameObject))
            {
                distortedNPCS.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (distortedNPCS.Contains(other.gameObject))
            {
                distortedNPCS.Remove(other.gameObject);
            }
        }
    }

    /*
    public void OnTriggerStay(Collider other)
    {
        if(other.name == "humanAnim")
        {
            if(!global.placingDecal && placed)
            {
                Debug.Log("Hello " + npcDetected);
               // if (!npcDetected)
               // {
                    npcDetected = true;
                    if (lastSprite.name == "unicorn")
                    {
                        //other.GetComponent<npcWander>().reactToDecal(pickReaction(unicornReactions));
                        other.gameObject.GetComponent<ReactiveNPC>().setDead();

                    }
                    else if (lastSprite.name == "redrum")
                    {
                        //other.GetComponent<npcWander>().reactToDecal(pickReaction(redrumReactions));
                        other.gameObject.GetComponent<ReactiveNPC>().setSurprised();
                        //other.gameObject.GetComponent<ReactiveNPC>().addFear(500);
                    }

                    //removeDecal();
               // }
            }
        }
    } */

    private string pickReaction(string[] list)
    {
        int i = Random.Range(0, list.Length);

        return list[i];
    }
}
