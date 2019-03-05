using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Note: This script handles EVERY single player action in this game!
// TODO: Smooth rotation.

public class Controller : MonoBehaviour
{
    public Text debugger;

    // Global variables
    public Global global;

    // Player
    public float walkSpeed;
    //public float strafeSpeed; // Difficult to implement with CharacterController.
    public float lookSpeed; // Mouse Sensitity?

    private Slider walkSlider;
    private Slider lookSlider;

    private GameObject player;
    private GameObject camera;
    private Vector3 cameraRotation;
    private Camera cameraComponent;
    private GameObject possessCam;
    private Vector3 possessCamRotation;
    private Camera possessCamComponent;
    private CharacterController characterController;
    private CollisionFlags collisionFlags;
    private Vector2 moveDirection;
    private bool looking;

    // Actions
    private DynamicButtonUpdater dynamicButtonUpdaterScript;
    private ItemActionInterface itemInfo;

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

    Toggle ActionWheelToggle;

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

    private HauntActions myHauntActions;
    private Haunt myHauntScript;

    // Start is called before the first frame update
    private void Awake()
    {
        
        global = GameObject.Find("Global").GetComponent<Global>(); // Shared pointer.

        player = GameObject.Find("Player");
        camera = GameObject.Find("Camera");
        cameraComponent = camera.GetComponent<Camera>();
        possessCam = GameObject.Find("possessionCamera");
        possessCamComponent = camera.GetComponent<Camera>();
        characterController = player.GetComponent<CharacterController>();
        dynamicButtonUpdaterScript = gameObject.GetComponent<DynamicButtonUpdater>();
        myDecalActions = gameObject.GetComponent<DecalActions>();
        myHauntActions = gameObject.GetComponent<HauntActions>();
        myHauntScript = gameObject.GetComponent<Haunt>();
        Platform();
        placeDecalScript = placeableDecal.GetComponent<placeDecal>();
        //lockMouse();

        cameraRotation = new Vector3(0.0f, 0.0f, 0.0f);
         possessCamRotation = new Vector3(0.0f, 0.0f, 0.0f);

        startPosition = new Vector2(0.0f, 0.0f);
        deltaPosition = new Vector2(0.0f, 0.0f);
        endPosition = new Vector2(0.0f, 0.0f);

        looking = false;

        walkSlider = GameObject.Find("GUI/Debugger/Move Slider").GetComponent<Slider>();
        lookSlider = GameObject.Find("GUI/Debugger/Look Slider").GetComponent<Slider>();

        walkSpeed = walkSlider.value;
        lookSpeed = lookSlider.value;

        ActionWheelToggle = GameObject.Find("GUI/Debugger/Action Wheel Toggle").GetComponent<Toggle>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (global.platform == true) {
            Touch();
        } else {
            /*
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
            */
            if (!global.possessing)
            {
                Move();
                Look();
            } else// if(global.possesMove)
            {
                possessedLook();
                try
                {
                    possessedMove();
                } catch
                {

                }
            }
            
        }
        rayCheck();

        Action1(false);
        Action2(false);
        Action3(false);
        Action4(false);

        walkSpeed = walkSlider.value;
        lookSpeed = lookSlider.value;
        //ActionWheel();
    }

    private void FixedUpdate()
    {
        // Get direction along the camera's orientation.
        Vector3 position = camera.transform.forward * moveDirection.y + camera.transform.right * moveDirection.x;

        // Get a normal for the surface that is being touched to move along it.
        RaycastHit hitInfo;
        Physics.SphereCast(player.transform.position, characterController.radius, Vector3.down, out hitInfo,
                           characterController.height / 2.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        position = Vector3.ProjectOnPlane(position, hitInfo.normal).normalized;

        position.x *= walkSpeed;
        position.z *= walkSpeed;

        collisionFlags = characterController.Move(position * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (collisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(characterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

    // The default state of the action wheel is to have no action buttons triggered.
    public void Action0()
    {
        global.decided = false;
        global.action = Global.Action.None;
        action1Button.animator.SetTrigger("Normal");
        action2Button.animator.SetTrigger("Normal");
        action3Button.animator.SetTrigger("Normal");
        action4Button.animator.SetTrigger("Normal");
    }

    // Triggerable script for the action wheel button named Action 1.
    // When calling from the Unity Editor, set trigger to true.
    public void Action1(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || trigger == true)
        {
            global.decided = true;
            global.action = Global.Action.One;

            // Button colors are private; therefore, 
            // we must control the state via animation.
            if (trigger == false)
            {
                action1Button.animator.SetTrigger("Pressed");
            }

            // Call specialized action (based on context).
            if(itemInfo != null)
            {
                itemInfo.callAction1();
            }
        }

        // Reset button animations.
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            action1Button.animator.SetTrigger("Normal");
        }

        if (trigger == true) {
            action1Button.OnDeselect(null);
        }
    }

    // See Action1() for comments.
    public void Action2(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) || trigger == true)
        {
            global.decided = true;
            global.action = Global.Action.Two;

            if (trigger == false)
            {
                action2Button.animator.SetTrigger("Pressed");
            }

            if (itemInfo != null)
            {
                itemInfo.callAction2();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            action2Button.animator.SetTrigger("Normal");
        }

        if (trigger == true)
        {
            action2Button.OnDeselect(null);
        }
    }

    // See Action1() for comments.
    public void Action3(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) || trigger == true)
        {
            global.decided = true;
            global.action = Global.Action.Three;

            if (trigger == false)
            {
                action3Button.animator.SetTrigger("Pressed");
            }

            if (itemInfo != null)
            {
                itemInfo.callAction3();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            action3Button.animator.SetTrigger("Normal");
        }

        if (trigger == true)
        {
            action3Button.OnDeselect(null);
        }
    }

    // See Action1() for comments.
    public void Action4(bool trigger)
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) || trigger == true)
        {
            global.decided = true;
            global.action = Global.Action.Four;

            if (trigger == false)
            {
                action4Button.animator.SetTrigger("Pressed");
            }

            if (itemInfo != null)
            {
                itemInfo.callAction4();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            action4Button.animator.SetTrigger("Normal");
        }

        if (trigger == true)
        {
            action4Button.OnDeselect(null);
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
        //global.inMenus = false;
        //lockMouse();
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


    public void setItemInfo(ItemActionInterface itemInterface)
    {
        itemInfo = itemInterface;
    }

    public void ActionWheel()
    {
        if (!ActionWheelToggle.isOn)
        {
            pcActions.transform.localPosition = new Vector3(0, -300, 0);
        }
        else
        {
            pcActions.transform.localPosition = new Vector3(800, -300, 0);
        }
    }

    private void rayCheck()
    {
        if (!global.possessing)
        {
            Vector3 rayOrigin = cameraComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit rayHit;
            if (Physics.Raycast(rayOrigin, cameraComponent.transform.forward, out rayHit, 3)) // 3 is the length of the ray drawn
            {
                GameObject other = rayHit.collider.gameObject;
                if (other.tag == "Item")
                {
                    //Call Dynamic button system

                    ItemActionInterface tmp = other.GetComponent<ItemActionInterface>();
                    itemInfo = myHauntActions;
                    myHauntScript.prepForHaunt(other, tmp);
                    //dynamicButtonUpdaterScript.receiveItemObject(gameObject, myHauntActions);
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

            //pcDialogue = GameObject.Find("PC Dialogue") as GameObject;
            //pcDialogue.SetActive(true);

            //dialogueText = GameObject.Find("PC Dialogue/Dialogue Text").GetComponent<Text>();

            mobileActions = GameObject.Find("Mobile Actions") as GameObject;
            mobileActions.SetActive(false);

            //mobileDialogue = GameObject.Find("Mobile Dialogue") as GameObject;
            //mobileDialogue.SetActive(false);

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

            //mobileDialogue = GameObject.Find("Mobile Dialogue") as GameObject;
            //mobileDialogue.SetActive(true);

            //dialogueText = GameObject.Find("Mobile Dialogue/Dialogue Text").GetComponent<Text>();

            pcActions = GameObject.Find("PC Actions") as GameObject;
            pcActions.SetActive(false);

            //pcDialogue = GameObject.Find("PC Dialogue") as GameObject;
            //pcDialogue.SetActive(false);

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
        moveDirection = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W) || deltaPosition.y > 0)
        {
            moveDirection.y = 1.0f;
        }

        if (Input.GetKey(KeyCode.A) || deltaPosition.x < 0)
        {
            moveDirection.x = -1.0f;
        }

        if (Input.GetKey(KeyCode.S) || deltaPosition.y < 0)
        {
            moveDirection.y = -1.0f;
        }

        if (Input.GetKey(KeyCode.D) || deltaPosition.x > 0)
        {
            moveDirection.x = 1.0f;
        }
    }

    private void Look()
    {
        if (Input.GetMouseButton(0))
        {
            //looking = true;
            //lockMouse();
                if (global.platform == false) // PC
                {
                    cameraRotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                    cameraRotation.x += Input.GetAxis("Mouse Y") * -lookSpeed;
                }
                else // Mobile
                {
                    cameraRotation.y += (deltaPosition.x < 0.0f ? -1.0f : 1.0f) * lookSpeed;
                    cameraRotation.x += (deltaPosition.y < 0.0f ? 1.0f : -1.0f) * lookSpeed;
                }

                cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90.0f, 90.0f);
                camera.transform.eulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, 0.0f);
            
        }
            
        /*
        if (Input.GetMouseButtonUp(0) && looking == true)
        {
            unlockMouse();
            looking = false;
        }
        */
    }

    private void possessedLook()
    {
        if (Input.GetMouseButton(0))
        {
            if (global.platform == false) // PC
            {
                possessCamRotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                possessCamRotation.x += Input.GetAxis("Mouse Y") * -lookSpeed;
            }
            else // Mobile
            {
                possessCamRotation.y += (deltaPosition.x < 0.0f ? -1.0f : 1.0f) * lookSpeed;
                possessCamRotation.x += (deltaPosition.y < 0.0f ? 1.0f : -1.0f) * lookSpeed;
            }

            possessCamRotation.x = Mathf.Clamp(possessCamRotation.x, -90.0f, 90.0f);
            possessCam.transform.eulerAngles = new Vector3(possessCamRotation.x, possessCamRotation.y, 0.0f);
        }
    }
    private void possessedMove()
    {

        if (Input.GetKeyDown(KeyCode.W) || deltaPosition.y > 0)
        {
            GameObject possessedObj = myHauntScript.lastItemObject;
            Rigidbody objRigidBody = possessedObj.GetComponent<Rigidbody>();
            //Vector3 forcePosition = possessCamComponent.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            //forcePosition = forcePosition - possessCam.transform.forward;
            //Debug.Log("Explode!!!");
            //objRigidBody.AddExplosionForce((float) 10.0, forcePosition, (float) 10.0);
            Vector3 hitForce = possessCam.transform.forward * 1000;
            objRigidBody.AddForce(hitForce);
        }

        if (Input.GetKey(KeyCode.A) || deltaPosition.x < 0)
        {
            GameObject possessedObj = myHauntScript.lastItemObject;
            Rigidbody objRigidBody = possessedObj.GetComponent<Rigidbody>();
            Vector3 hitForce = possessCam.transform.right * -200;
            objRigidBody.AddForce(hitForce);
        }

        if (Input.GetKey(KeyCode.S) || deltaPosition.y < 0)
        {
            GameObject possessedObj = myHauntScript.lastItemObject;
            Rigidbody objRigidBody = possessedObj.GetComponent<Rigidbody>();
            Vector3 hitForce = possessCam.transform.forward * -200;
            objRigidBody.AddForce(hitForce);
        }

        if (Input.GetKey(KeyCode.D) || deltaPosition.x > 0)
        {
            GameObject possessedObj = myHauntScript.lastItemObject;
            Rigidbody objRigidBody = possessedObj.GetComponent<Rigidbody>();
            Vector3 hitForce = possessCam.transform.right * 200;
            objRigidBody.AddForce(hitForce);
        }
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
