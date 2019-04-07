﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteractionManager : MonoBehaviour
{
    public GameObject guiActionObj;
    public Shader softSelectShader;
    public Shader hardSelectShader;

    Controller2 myController;
    Global global;
    Actions actions;
    Camera cameraComponent;
    DynamicButtonUpdater dynamicButtonUpdaterScript;
    public Shader normalShader;

    private bool holdingObject = false;
    private float holdLength = 3f;

    private bool setButtons = false;
    public RectTransform button1Rect;
    public RectTransform button2Rect;
    public RectTransform button3Rect;
    public RectTransform button4Rect;
    // Start is called before the first frame update
    void Start()
    {
        myController = gameObject.GetComponent<Controller2>();
        actions = gameObject.GetComponent<Actions>();
        global = myController.global;
        cameraComponent = myController.camera.GetComponent<Camera>();
        dynamicButtonUpdaterScript = gameObject.GetComponent<DynamicButtonUpdater>();
    }

    // Update is called once per frame
    void Update()
    {
        raySelectCheck();
        actionUpdater();
        if (global.hardSelected != null)
        {
            //rigidBodyHandler(); currently breaks objects when spam clicking them
        }
    }

    public bool checkUIClick()
    {
        Vector2 localMousePosition = button1Rect.InverseTransformPoint(Input.mousePosition);
        if (button1Rect.rect.Contains(localMousePosition))
        {
            return true;
        }
        localMousePosition = button2Rect.InverseTransformPoint(Input.mousePosition);
        if (button2Rect.rect.Contains(localMousePosition))
        {
            return true;
        }
        localMousePosition = button3Rect.InverseTransformPoint(Input.mousePosition);
        if (button3Rect.rect.Contains(localMousePosition))
        {
            return true;
        }
        localMousePosition = button4Rect.InverseTransformPoint(Input.mousePosition);
        if (button4Rect.rect.Contains(localMousePosition))
        {
            return true;
        }
        return false;
    }

    // raySelectCheck handles updating global states depending what objects are being hovered/clicked on
    private void raySelectCheck()
    {
        if (global.hardSelected == null)
        {
            RaycastHit rayHit;
            Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out rayHit, 200)) // 200 may be too long or short and should be adjusted
            {
                GameObject other = rayHit.collider.gameObject;
                if (!checkUIClick())
                {
                    if (other.GetComponent<ItemActionInterface>() != null)
                    {
                        if (global.softSelected == null)// && !Input.GetMouseButton(1))
                        {
                            MeshRenderer[] myRenders = other.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer rend in myRenders)
                            {
                                rend.material.shader = softSelectShader;

                            }
                            //Debug.Log("setting soft select to other!!");
                            global.softSelected = other;
                            //dynamicButtonUpdaterScript.enableAllButtons();
                            global.itemInfo = other.GetComponent<ItemActionInterface>();

                            // dynamicButtonUpdaterScript.setStates(global.itemInfo.states);
                            //dynamicButtonUpdaterScript.enableAllButtons();
                        }
                        else if (global.softSelected != other)
                        {
                            MeshRenderer[] myRenders = global.softSelected.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer rend in myRenders)
                            {
                                rend.material.shader = normalShader;
                            }
                            MeshRenderer[] myRenders2 = other.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer rend in myRenders2)
                            {
                                rend.material.shader = softSelectShader;
                            }

                            global.softSelected = other;
                            global.itemInfo = other.GetComponent<ItemActionInterface>();
                        }

                        if (Input.GetMouseButtonDown(0))
                        {
                            MeshRenderer[] myRenders3 = other.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer rend in myRenders3)
                            {
                                rend.material.shader = hardSelectShader;

                            }

                            global.hardSelected = other;
                            setButtons = false;
                        }

                    }
                    else
                    {
                        if (global.softSelected != null)
                        {
                            MeshRenderer[] myRenders = global.softSelected.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer rend in myRenders)
                            {
                                rend.material.shader = normalShader;
                            }
                        }
                        //dynamicButtonUpdaterScript.enableAllButtons();
                        global.itemInfo = null;
                        global.softSelected = null;
                    }
                }
                else
                {
                    if (global.softSelected != null)
                    {
                        MeshRenderer[] myRenders = global.softSelected.GetComponentsInChildren<MeshRenderer>();
                        foreach (MeshRenderer rend in myRenders)
                        {
                            rend.material.shader = normalShader;
                        }
                    }
                    //dynamicButtonUpdaterScript.enableAllButtons();
                    global.itemInfo = null;
                    global.softSelected = null;
                }
            }
            setButtons = false;
        } else // if the player has something hardselected
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit rayHit;
                Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out rayHit, 200)) // 200 may be too long or short and should be adjusted
                {
                    GameObject other = rayHit.collider.gameObject;
                    if (!checkUIClick())
                    {
                        if (other.GetComponent<ItemActionInterface>() != null)
                        {
                            if (global.hardSelected != null)
                            {
                                MeshRenderer[] myRenders = global.hardSelected.GetComponentsInChildren<MeshRenderer>();
                                foreach (MeshRenderer rend in myRenders)
                                {
                                    rend.material.shader = normalShader;
                                }

                            }

                            MeshRenderer[] myRenders2 = other.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer rend in myRenders2)
                            {
                                rend.material.shader = hardSelectShader;

                            }

                            global.hardSelected = other;
                            global.itemInfo = other.GetComponent<ItemActionInterface>();
                            setButtons = false;


                        }
                        else
                        {
                            if (global.hardSelected != null)
                            {
                                MeshRenderer[] myRenders = global.hardSelected.GetComponentsInChildren<MeshRenderer>();
                                foreach (MeshRenderer rend in myRenders)
                                {
                                    rend.material.shader = normalShader;
                                }
                                if (holdingObject)
                                {
                                    dropObject();
                                }

                            }
                            global.hardSelected = null;

                        }
                   }

                } else
                {

                    if (global.hardSelected != null)
                    {
                        MeshRenderer[] myRenders = global.hardSelected.GetComponentsInChildren<MeshRenderer>();
                        foreach (MeshRenderer rend in myRenders)
                        {
                            rend.material.shader = normalShader;
                        }
                    }
                    if (holdingObject)
                    {
                        dropObject();
                    }
                    global.hardSelected = null;
                }
                setButtons = false;
            }
        }
    }

    // updates the actions according to what is hardSelected or softSelected
    private void actionUpdater()
    {
        //Debug.Log((global.hardSelected != null) + " " + (global.softSelected != null));
        if (!setButtons)
        {
            guiActionObj.SetActive(true);
            if (global.hardSelected != null)
            {

                dynamicButtonUpdaterScript.receiveItemObject(global.hardSelected, global.itemInfo);
                global.softSelected = null;
            }
            else if (global.softSelected != null)
            {
                dynamicButtonUpdaterScript.receiveItemObject(global.softSelected, global.itemInfo);
                global.hardSelected = null;
            }
            else
            {
                
                dynamicButtonUpdaterScript.enableAllButtons();
                dynamicButtonUpdaterScript.setStates(new bool[] { true, true, false, true });
                guiActionObj.SetActive(false);
                global.itemInfo = null;
                holdingObject = false;

            }
            setButtons = true;
        }
    }

    private void rigidBodyHandler()
    {
        Rigidbody tmp =  global.hardSelected.GetComponent<Rigidbody>();
        if(tmp != null)
        {
            if (global.hardSelected != null && !holdingObject && Input.GetMouseButtonDown(0))
            {
                holdingObject = true;
                pickupObject();

            } else if(holdingObject && Input.GetMouseButtonDown(0))
            {
                throwObject(tmp);
            }

            if (holdingObject) { 
                

                float mouseDelta = Input.mouseScrollDelta.y;
                holdLength += mouseDelta;
                if(holdLength > 10)
                {
                    holdLength = 10;
                } else if(holdLength < 3)
                {
                    holdLength = 3;
                }

                pickupObject();
            }

            /*
            Debug.Log("Before mouse button down");
            if (Input.GetMouseButtonDown(2) && holdingObject)
            {
                Debug.Log("Middle mouse pressed");
                throwObject(tmp);
            }*/
        }
        
        
    }

    private void pickupObject()
    {

        global.hardSelected.GetComponent<Rigidbody>().useGravity = false;
        Vector3 rayOrigin = cameraComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit rayHit;
        if (Physics.Raycast(rayOrigin, cameraComponent.transform.forward, out rayHit, holdLength + .2f)) // 3 is the length of the ray drawn
        {
            global.hardSelected.transform.position = Vector3.MoveTowards(global.hardSelected.transform.position, gameObject.transform.position + cameraComponent.transform.forward * (rayHit.distance -.2f), 15 * Time.deltaTime);
            Debug.Log("held object is hitting a collider");
        }
        else
        {
            global.hardSelected.transform.position = Vector3.MoveTowards(global.hardSelected.transform.position, gameObject.transform.position + cameraComponent.transform.forward * holdLength, 15 * Time.deltaTime);
        }
    }

    private void dropObject()
    {
        try
        {
            global.hardSelected.GetComponent<Rigidbody>().useGravity = true;
        }
        catch
        {

        }
        finally
        {
            MeshRenderer[] myRenders = global.hardSelected.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in myRenders)
            {
                rend.material.shader = normalShader;
            }

            if (global.softSelected != null)
            {
                MeshRenderer[] myRenders2 = global.softSelected.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer rend in myRenders2)
                {
                    rend.material.shader = normalShader;
                }
            }

            holdingObject = false;
            global.hardSelected = null;
            global.softSelected = null;
            global.itemInfo = null;
        }
    }

    private void throwObject(Rigidbody rig)
    {
        dropObject();
        rig.AddForce(cameraComponent.transform.forward.normalized * 5000);
    }
}
