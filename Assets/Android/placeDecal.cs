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
        if (global.placingDecal && !isSnapped)
        {
            origin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            Vector3 newPos = origin + (camComponent.transform.forward * 2);
            decalTransform.position = newPos;
            decalTransform.rotation = camComponent.gameObject.transform.rotation;
        } else if(global.placingDecal && isSnapped)
        {
            origin = camComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            Transform myTransfrom = gameObject.transform;
            Vector3 newPos = new Vector3(origin.x, origin.y, myTransfrom.position.z);
            /*
            float y0 = origin.y;
            float y90 = Mathf.Abs(origin.y -90);
            float y180 = Mathf.Abs(origin.y - 180);
            float yneg180 = Mathf.Abs(origin.y + 180);
            float yneg90 = Mathf.Abs(origin.y + 90);
            float compareY = 400;
            float trueY = 400;
            if(y0 < compareY)
            {
                trueY = 0;
                compareY = y0;
            }
            if (y90 < compareY)
            {
                trueY = 90;
                compareY = y90;
            }
            if (y180 < compareY)
            {
                trueY = 180;
                compareY = y180;
            }
            if (yneg180 < compareY)
            {
                trueY = -180;
                compareY = yneg180;
            }
            if (yneg90 < compareY)
            {
                trueY = -90;
                compareY = yneg90;
            }
            Quaternion originRot = camComponent.gameObject.transform.rotation;
            Quaternion newRot = new Quaternion(originRot.x, trueY, originRot.z, originRot.w); */
            //Vector3 newPos = origin + camComponent.transform.forward * 3;//Mathf.Min(3,Mathf.Abs( 
            // snappedObject.transform.position.z -camComponent.gameObject.transform.position.z));
            decalTransform.position = newPos;
            //decalTransform.rotation = newRot;
        }
        
    }

    public void findSpot(Sprite decal, GameObject camera)
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = decal;
        camComponent = camera.GetComponent<Camera>();
        global.placingDecal = true;
    }


    public void placeAtSpot()
    {
        global.placingDecal = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSnapped && other.gameObject.name != "Player")
        {
            isSnapped = true;
            snappedObject = other.gameObject;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (snappedObject != null && other.gameObject == snappedObject)
        {

            Quaternion rotVect = snappedObject.transform.localRotation;
            //decalTransform.position = new Vector3(origin.x, origin.y, hit.point.z);
            decalTransform.localRotation = rotVect;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(snappedObject != null && other.gameObject == snappedObject)
        {
            isSnapped = false;
            snappedObject = null;
            //Debug.Log("Exited");
        }
    }
}
