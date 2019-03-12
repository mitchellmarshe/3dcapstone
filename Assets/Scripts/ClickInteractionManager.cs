using System.Collections;
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

    private bool setButtons = false;
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
        rigidBodyHandler();
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
                if (other.tag == "Item")
                {
                    if (global.softSelected == null && !Input.GetMouseButton(1))
                    {
                        MeshRenderer[] myRenders = other.GetComponentsInChildren<MeshRenderer>();
                        foreach(MeshRenderer rend in myRenders){
                            rend.material.shader = softSelectShader;

                        }
                        //Debug.Log("setting soft select to other!!");
                        global.softSelected = other;
                        //dynamicButtonUpdaterScript.enableAllButtons();
                        global.itemInfo = other.GetComponent<ItemActionInterface>();

                        // dynamicButtonUpdaterScript.setStates(global.itemInfo.states);
                        //dynamicButtonUpdaterScript.enableAllButtons();
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        MeshRenderer[] myRenders = other.GetComponentsInChildren<MeshRenderer>();
                        foreach (MeshRenderer rend in myRenders)
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
            } else
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
            setButtons = false;
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit rayHit;
                Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out rayHit, 200)) // 200 may be too long or short and should be adjusted
                {
                    GameObject other = rayHit.collider.gameObject;
                    if (other.tag == "Item")
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
                        dropObject();
                        global.hardSelected = other;
                        global.itemInfo = other.GetComponent<ItemActionInterface>();


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
                        global.hardSelected = null;
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
                    global.hardSelected = null;
                }
                setButtons = false;
            }
        }
    }

    // updates the actions according to what is hardSelected or softSelected
    private void actionUpdater()
    {
        
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

            }
            setButtons = true;
        }
    }

    private void rigidBodyHandler()
    {
        try
        {
           Rigidbody tmp =  global.hardSelected.GetComponent<Rigidbody>();
            if(tmp != null)
            {
                if (global.hardSelected != null && !holdingObject)
                {
                    holdingObject = true;

                }
                else if (global.hardSelected == null)
                {
                    holdingObject = false;
                }

                if (holdingObject)
                {
                    pickupObject();
                }
                Debug.Log("Before mouse button down");
                if (Input.GetMouseButtonDown(2) && holdingObject)
                {
                    Debug.Log("Middle mouse pressed");
                    throwObject(tmp);
                }
            }
        }
        catch
        {

        }
        
        
    }

    private void pickupObject()
    {
        global.hardSelected.transform.position = gameObject.transform.position + cameraComponent.transform.forward*3;
    }

    private void dropObject()
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

    private void throwObject(Rigidbody rig)
    {
        dropObject();
        rig.AddForce(cameraComponent.transform.forward * 5000);
    }
}
