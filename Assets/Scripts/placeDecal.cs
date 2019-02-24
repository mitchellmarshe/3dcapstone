﻿using System.Collections;
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
    // Start is called before the first frame update
    private string[] redrumReactions = new string[] {"Terror"};
    private string[] unicornReactions = new string[] {"Terror"};
    void Awake()
    {
        isSnapped = false;
        global = GameObject.Find("Global").GetComponent<Global>();
        decalTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

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
                    

                }
            } else
            {
                isSnapped = false ;
                origin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                Vector3 newPos = origin + (camComponent.transform.forward * 3);
                decalTransform.position = newPos;
                decalTransform.rotation = camComponent.gameObject.transform.rotation;
            }
        }
    }


    public void findSpot(Sprite decal, GameObject camera)
    {
        lastSprite = decal;
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = decal;
        camComponent = camera.GetComponent<Camera>();
        global.placingDecal = true;
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "humanAnim")
        {
            if(!global.placingDecal && placed)
            {
                if(lastSprite.name == "unicorn")
                {
                    other.GetComponent<npcWander>().reactToDecal(pickReaction(unicornReactions));
                } else if (lastSprite.name == "redrum")
                {
                    other.GetComponent<npcWander>().reactToDecal(pickReaction(redrumReactions));
                }
                
            }
        }
    }

    private string pickReaction(string[] list)
    {
        int i = Random.Range(0, list.Length);

        return list[i];
    }
}
