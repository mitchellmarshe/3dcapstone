using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Note: This script handles EVERY single player action in this game!

public class Controller : MonoBehaviour
{
    public Text debugger;

    // Global variables
    public Global global;

    // Player
    private GameObject player;
    private GameObject camera;
    private Vector3 cameraRotation;
    private Camera cameraComponent;

    // Actions
    private DynamicButtonUpdater dynamicButtonUpdaterScript;
    ItemActionInterface itemInfo;

    private GameObject pcActions;
    private GameObject mobileActions;

    private Image selectorIcon;
    private Text selectorText;

    private Button action1Button;
    private Button action2Button;
    private Button action3Button;
    private Button action4Button;

    private Text action1Text;
    private Text action2Text;
    private Text action3Text;
    private Text action4Text;

    // Dialogue
    private GameObject pcDialogue;
    private GameObject mobileDialogue;

    private Text dialogueText;

    // Joysticks
    private GameObject mobileMoveJoystick;
    private GameObject mobileLookJoystick;

    private Vector2 startPosition;
    private Vector2 deltaPosition;
    private Vector2 endPosition;

    // Decals
    private GameObject decalBank;
    private GameObject placeableDecal;
    public placeDecal placeDecalScript;
    private DecalActions myDecalActions;



    // Start is called before the first frame update
    void Start()
    {
        
        global = GameObject.Find("Global").GetComponent<Global>(); // Shared pointer.

        player = GameObject.Find("Player");
        camera = GameObject.Find("Camera");
        cameraComponent = camera.GetComponent<Camera>();
        dynamicButtonUpdaterScript = gameObject.GetComponent<DynamicButtonUpdater>();
        myDecalActions = gameObject.GetComponent<DecalActions>();
        Platform();
        placeDecalScript = placeableDecal.GetComponent<placeDecal>();
        lockMouse();
        //lockMouse();

        cameraRotation = new Vector3(0.0f, 0.0f, 0.0f);

        startPosition = new Vector2(0.0f, 0.0f);
        deltaPosition = new Vector2(0.0f, 0.0f);
        endPosition = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (global.platform == true) {
            Touch();
        } else {
            if (Input.GetKeyDown(KeyCode.M))
            {
                global.inMenus = !global.inMenus;
                oppositeMouse();
            }
            if (!global.inMenus)
            {
                Move();
                Look();
            }
        }
        rayCheck();
        Haunt(false);

        Action1(false);
        Action2(false);
        Action3(false);
        Action4(false);
    }

    // Note: with a tag, enable this function and set up dialogue accordingly.
    public void Haunt(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.E) || trigger == true)
        {
            global.haunted = !global.haunted;
        }
    }

    // UPDATE
    // Note: triggerable script for the button named Decision 1.
    // When calling from Unity Editor, set trigger to true.
    // Attaching this script to button, allows the button to be touchle for Mobile platforms.
    // When using this script from another script, set trigger to false.
    public void Action0()
    {
        global.decided = false;
        global.action = Global.Action.None;
    }

    public void Action1(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || trigger == true)
        {
            
            global.inMenus = false;
            lockMouse();
            global.decided = true;
            global.action = Global.Action.One;
            if(itemInfo != null)
            {
                itemInfo.callAction1();
            }
        }
    }

    public void Action2(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) || trigger == true)
        {
            global.inMenus = false;
            lockMouse();
            global.decided = true;
            global.action = Global.Action.Two;
            if (itemInfo != null)
            {
                itemInfo.callAction2();
            }
        }
    }

    public void Action3(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) || trigger == true)
        {
            global.inMenus = false;
            lockMouse();
            global.decided = true;
            global.action = Global.Action.Three;
            if (itemInfo != null)
            {
                itemInfo.callAction3();
            }
        }
    }

    public void Action4(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) || trigger == true)
        {
            global.inMenus = false;
            lockMouse();
            global.decided = true;
            global.action = Global.Action.Four;
            if (itemInfo != null)
            {
                itemInfo.callAction4();
            }
        }
    }

    public void showDecalMenu()
    {
        decalBank.SetActive(!decalBank.activeInHierarchy);
    }

    public void placeSelectedDecal(Sprite selectedSprite)
    {
        showDecalMenu(); // this will close the decal menu because someone just selected a decal
        placeableDecal.SetActive(true);
        global.inMenus = false;
        lockMouse();
        placeDecalScript.findSpot(selectedSprite, camera);
    }

    public void lockMouse()
    {
        global.mouseLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //controller.enabled = true; Insert component controlling movement here if you want to freeze player while they pick an option


    }


    public void unlockMouse()
    {
        global.mouseLocked = false;
        //controller.enabled = false; Insert component controlling movement here if you want to freeze player while they pick an option
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void oppositeMouse()
    {
        if (global.mouseLocked)
        {
            unlockMouse();
        }
        else
        {
            lockMouse();
        }
    }



    private void rayCheck()
    {

        Vector3 rayOrigin = cameraComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit rayHit;
        if (Physics.Raycast(rayOrigin, cameraComponent.transform.forward, out rayHit, 3)) // 3 is the length of the ray drawn
        {
            GameObject other = rayHit.collider.gameObject;
            if (other.tag == "Item")
            {
                //Call Dynamic button system
                itemInfo = other.GetComponent<ItemActionInterface>();

                dynamicButtonUpdaterScript.receiveItemObject(other, itemInfo);
            }
            // do stuff like check other for tags or w/e you like
        }
        else
        {
            selectorIcon.sprite = null;
            itemInfo = myDecalActions;
            dynamicButtonUpdaterScript.receiveItemObject(gameObject, myDecalActions);
            selectorText.text = "Decal";
        }
        //Debug.DrawRay(rayOrigin, gameCam.transform.forward * 3, Color.green); // this will draw a green line in editor for debugging

    }

    private void Platform()
    {
        // PC.
        if (global.platform == false)
        {
            pcActions = GameObject.Find("PC Actions") as GameObject;
            pcActions.SetActive(true);

            action1Button = GameObject.Find("PC Actions/Action 1 Button").GetComponent<Button>();
            action2Button = GameObject.Find("PC Actions/Action 2 Button").GetComponent<Button>();
            action3Button = GameObject.Find("PC Actions/Action 3 Button").GetComponent<Button>();
            action4Button = GameObject.Find("PC Actions/Action 4 Button").GetComponent<Button>();

            action1Text = GameObject.Find("PC Actions/Action 1 Button/Action 1 Text").GetComponent<Text>();
            action2Text = GameObject.Find("PC Actions/Action 2 Button/Action 2 Text").GetComponent<Text>();
            action3Text = GameObject.Find("PC Actions/Action 3 Button/Action 3 Text").GetComponent<Text>();
            action4Text = GameObject.Find("PC Actions/Action 4 Button/Action 4 Text").GetComponent<Text>();

            selectorIcon = GameObject.Find("PC Actions/Selection Icon").GetComponent<Image>();
            selectorText = GameObject.Find("PC Actions/Selection Icon/Selection Text").GetComponent<Text>();

            pcDialogue = GameObject.Find("PC Dialogue") as GameObject;
            pcDialogue.SetActive(true);

            dialogueText = GameObject.Find("PC Dialogue/Dialogue Text").GetComponent<Text>();

            mobileActions = GameObject.Find("Mobile Actions") as GameObject;
            mobileActions.SetActive(false);

            mobileDialogue = GameObject.Find("Mobile Dialogue") as GameObject;
            mobileDialogue.SetActive(false);

            mobileMoveJoystick = GameObject.Find("Mobile Move Joystick") as GameObject;
            mobileMoveJoystick.SetActive(false);

            mobileLookJoystick = GameObject.Find("Mobile Look Joystick") as GameObject;
            mobileLookJoystick.SetActive(false);

            decalBank = GameObject.Find("Decal Bank") as GameObject;
            decalBank.SetActive(false);

            placeableDecal = GameObject.Find("tmp_decal") as GameObject;
            placeableDecal.SetActive(false);
        }
        else // Mobile.
        {
            mobileActions = GameObject.Find("Mobile Actions") as GameObject;
            mobileActions.SetActive(true);

            action1Button = GameObject.Find("Mobile Actions/Action 1 Button").GetComponent<Button>();
            action2Button = GameObject.Find("Mobile Actions/Action 2 Button").GetComponent<Button>();
            action3Button = GameObject.Find("Mobile Actions/Action 3 Button").GetComponent<Button>();
            action4Button = GameObject.Find("Mobile Actions/Action 4 Button").GetComponent<Button>();

            action1Text = GameObject.Find("Mobile Actions/Action 1 Button/Action 1 Text").GetComponent<Text>();
            action2Text = GameObject.Find("Mobile Actions/Action 2 Button/Action 2 Text").GetComponent<Text>();
            action3Text = GameObject.Find("Mobile Actions/Action 3 Button/Action 3 Text").GetComponent<Text>();
            action4Text = GameObject.Find("Mobile Actions/Action 4 Button/Action 4 Text").GetComponent<Text>();

            selectorIcon = GameObject.Find("Mobile Actions/Selection Icon").GetComponent<Image>();
            selectorText = GameObject.Find("Mobile Actions/Selection Icon/Selection Text").GetComponent<Text>();

            mobileDialogue = GameObject.Find("Mobile Dialogue") as GameObject;
            mobileDialogue.SetActive(true);

            dialogueText = GameObject.Find("Mobile Dialogue/Dialogue Text").GetComponent<Text>();

            pcActions = GameObject.Find("PC Actions") as GameObject;
            pcActions.SetActive(false);

            pcDialogue = GameObject.Find("PC Dialogue") as GameObject;
            pcDialogue.SetActive(false);

            mobileMoveJoystick = GameObject.Find("Mobile Move Joystick") as GameObject;
            mobileMoveJoystick.SetActive(true);

            mobileLookJoystick = GameObject.Find("Mobile Look Joystick") as GameObject;
            mobileLookJoystick.SetActive(true);

            decalBank = GameObject.Find("Decal Bank") as GameObject;
            decalBank.SetActive(false);

            placeableDecal = GameObject.Find("tmp_decal") as GameObject;
            placeableDecal.SetActive(false);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W) || deltaPosition.y > 0)
        {
            player.transform.position += new Vector3(camera.transform.forward.x, 0.0f, camera.transform.forward.z);
        }

        if (Input.GetKey(KeyCode.A) || deltaPosition.x < 0)
        {
            player.transform.position -= new Vector3(camera.transform.right.x, 0.0f, camera.transform.right.z);
        }

        if (Input.GetKey(KeyCode.S) || deltaPosition.y < 0)
        {
            player.transform.position -= new Vector3(camera.transform.forward.x, 0.0f, camera.transform.forward.z);
        }

        if (Input.GetKey(KeyCode.D) || deltaPosition.x > 0)
        {
            player.transform.position += new Vector3(camera.transform.right.x, 0.0f, camera.transform.right.z);
        }
    }

    private void Look()
    {
        if (global.platform == false) {
            cameraRotation.y += Input.GetAxis("Mouse X");
            cameraRotation.x += Input.GetAxis("Mouse Y") * -1.0f;
        } else
        {
            cameraRotation.y += deltaPosition.x < 0.0f ? -1.0f : 1.0f;
            cameraRotation.x += deltaPosition.y < 0.0f ? 1.0f : -1.0f;
        }
        camera.transform.eulerAngles = cameraRotation;
    }

    private void Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                deltaPosition = touch.deltaPosition;
                if (RectTransformUtility.RectangleContainsScreenPoint(mobileLookJoystick.GetComponent<RectTransform>(), touch.position)) {
                    Look();
                }
                debugger.text = "" + deltaPosition;
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(mobileLookJoystick.GetComponent<RectTransform>(), touch.position))
                {
                    Look();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Canceled)
            {

            }
        }
    }


}
