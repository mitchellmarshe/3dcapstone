using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject GUI;
    private Image[] UISprites;
    public Transform heldObject;

    public Guardi guardi;

    // Start is called before the first frame update
    void Start()
    {
        GUI.GetComponent<GUI>().ShowOverlays(false);
        UISprites = GUI.GetComponentsInChildren<Image>();
        GUI.GetComponent<GUI>().ShowOverlays(true);
        GUI.GetComponent<GUI>().SetOverlays();

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
        
    }

    public bool checkUIClick()
    {
        if (global.hardSelected != null)
        {
            foreach (Image component in UISprites)
            {
                //Debug.Log(component.gameObject.name + " is " + component.gameObject.activeInHierarchy);
                if (component.gameObject.activeInHierarchy)
                {

                    RectTransform tmpRect = component.gameObject.GetComponent<RectTransform>();
                    Vector2 localMousePosition = tmpRect.InverseTransformPoint(Input.mousePosition);
                    if (tmpRect.rect.Contains(localMousePosition))
                    {
                        //Debug.Log("Clicked on " + component.gameObject.name + " | Returning true");
                        return true;
                    }
                }
            }
        }
        //Debug.Log("returning false from chuckUICLick()");
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
                            

                            // Guardi
                            if (global.currentScene == global.mainScene && global.tutorial == true && guardi.softSelection == false)
                            {
                                guardi.softSelection = true;
                            }
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

                            // Guardi
                            if (global.currentScene == global.mainScene && global.tutorial == true && guardi.hardSelection == false)
                            {
                                guardi.hardSelection = true;
                            }
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
                            if (global.hardSelected != null && global.hardSelected != other)
                            {
                                MeshRenderer[] myRenders = global.hardSelected.GetComponentsInChildren<MeshRenderer>();
                                foreach (MeshRenderer rend in myRenders)
                                {
                                    rend.material.shader = normalShader;
                                }

                                MeshRenderer[] myRenders2 = other.GetComponentsInChildren<MeshRenderer>();
                                foreach (MeshRenderer rend in myRenders2)
                                {
                                    rend.material.shader = hardSelectShader;

                                }
                                if (holdingObject)
                                {
                                    dropObject();
                                }
                                

                            }
                            else
                            {
                                if (!holdingObject)
                                {
                                    pickupObject(other.GetComponent<Rigidbody>());
                                }
                            }

                            global.hardSelected = other;
                            global.itemInfo = other.GetComponent<ItemActionInterface>();
                            setButtons = false;




                        }
                        else
                        {
                            if (global.hardSelected != null)
                            {
                                if (global.hardSelected != other)
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
                                } else
                                {
                                    if (!holdingObject)
                                    {
                                        pickupObject(other.GetComponent<Rigidbody>());
                                    }
                                }
                                //if (holdingObject)
                                //{
                                //dropObject();
                                //}

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
                        if (holdingObject)
                        {
                            dropObject();
                        }
                    }
                    //if (holdingObject)
                    //{
                        //dropObject();
                    //}
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

    /*
    private void rigidBodyHandler()
    {
        Rigidbody tmp = global.hardSelected.GetComponent<Rigidbody>();
        
        if(tmp != null && tmp.mass >= 2)
        {
            if (!holdingObject && Input.GetMouseButtonDown(0) && !checkUIClick())
            {
                holdingObject = true;
                pickupObject(tmp);

            }else if (Input.GetMouseButtonDown(0) && holdingObject && !checkUIClick())
            {
                dropObject();
            }
        }
        
        
    }
    */


    private void pickupObject(Rigidbody body)
    {
        if (body.mass >= 2)
        {
            holdingObject = true;
            body.useGravity = false;
            body.detectCollisions = false;
            body.gameObject.transform.position = heldObject.transform.parent.position;
            heldObject.GetComponentInChildren<HeldItemTranslation>().resetAnim(true);
            body.transform.SetParent(heldObject.transform);

            // Guardi
            if (global.currentScene == global.mainScene && global.tutorial == true && guardi.pickupObject == false)
            {
                guardi.pickupObject = true;
            }
        }
    }

    private void dropObject()
    {
        holdingObject = false;
        Rigidbody tmp = global.hardSelected.GetComponent<Rigidbody>();
        tmp.useGravity = true;
        tmp.detectCollisions = true;
        heldObject.DetachChildren();
        heldObject.GetComponent<HeldItemTranslation>().resetAnim(false);
        //global.hardSelected.transform.LookAt(Input.mousePosition);
        Ray tmpRay = cameraComponent.ScreenPointToRay(Input.mousePosition);

        tmp.AddForce(tmpRay.direction.normalized * 5000);
        if (global.hardSelected != null)
        {
            MeshRenderer[] myRenders = global.hardSelected.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in myRenders)
            {
                rend.material.shader = normalShader;
            }
            global.hardSelected = null;
        }

        // Guardi
        if (global.currentScene == global.mainScene && global.tutorial == true && guardi.throwObject == false)
        {
            guardi.throwObject = true;
        }
    }
    /*
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

    */
}
