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
    // Start is called before the first frame update
    void Start()
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
                if (global.placingDecal)
                {
                    isSnapped = false ;
                    origin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                    Vector3 newPos = origin + (camComponent.transform.forward * 3);
                    decalTransform.position = newPos;
                    decalTransform.rotation = camComponent.gameObject.transform.rotation;
                }
            }
        }
    }


    public void findSpot(Sprite decal, GameObject camera)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = decal;
        camComponent = camera.GetComponent<Camera>();
        global.placingDecal = true;
    }


    public void placeAtSpot()
    {
        global.placingDecal = false;

    }
}
