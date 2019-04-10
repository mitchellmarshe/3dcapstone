using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller2 : MonoBehaviour
{
    [Header("Scripts")]
    public Global global;
    public Actions actions;
    public Menu menu;

    [Header("Player")]
    public GameObject player;
    public GameObject camera;
    public float walkSpeed;
    public float lookSpeed;

    [Header("Decal")]
    public GameObject tmpDecal;

    [Header("Mobile")]
    public Joystick moveJoystick;
    public Joystick lookJoystick;

    [Header("Overlay")]
    public Color fade;

    private CharacterController characterController;
    private CollisionFlags collisionFlags;
    private Vector3 playerDirection;
    private Vector3 cameraRotation;

    private DynamicButtonUpdater myButtonUpdater;

    private void Awake()
    {
        myButtonUpdater = gameObject.GetComponent<DynamicButtonUpdater>();
        characterController = player.GetComponent<CharacterController>();
        playerDirection = new Vector3(0.0f, 0.0f, 0.0f);
        cameraRotation = camera.transform.eulerAngles;

        if (global.platform == false)
        {
            walkSpeed = 10.0f;
            lookSpeed = 5.0f;
        }
        else
        {
            walkSpeed = 10.0f;
            lookSpeed = 5.0f;
        }
    }

    private void Start()
    {
        actions.Action0();
    }

    private void Update()
    {
        Move();
        //CheckMove();
        Look();

        actions.Action1(false);
        actions.Action2(false);
        actions.Action3(false);
        actions.Action4(false);

        menu.ShowMenu(false);
    }

    private void FixedUpdate()
    {
        // Get direction along the camera's orientation.
        Vector3 position = camera.transform.forward * playerDirection.y + camera.transform.right * playerDirection.x;

        // Get a normal for the surface that is being touched to move along it.
        RaycastHit hitInfo;
        Physics.SphereCast(player.transform.position, characterController.radius, Vector3.down, out hitInfo,
                           characterController.height / 2.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        position = Vector3.ProjectOnPlane(position, hitInfo.normal).normalized;

        position.x *= walkSpeed;
        position.z *= walkSpeed;

        collisionFlags = characterController.Move(position * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (hit.collider.tag == "Ignore")
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), hit.collider);
        }

        // Don't move the rigidbody if the character is on top of it.
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

    private void Move()
    {
        if (global.currentScene == global.mainScene)
        {
            playerDirection = new Vector2(0.0f, 0.0f);

            Vector2 coordinate = new Vector2(0.0f, 0.0f);
            if (global.platform == true)
            {
                coordinate = moveJoystick.Coordinate();
            }

            if (Input.GetKey(KeyCode.W) || coordinate.y > 0)
            {
                playerDirection.y = 1.0f;
            }

            if (Input.GetKey(KeyCode.A) || coordinate.x < 0)
            {
                playerDirection.x = -1.0f;
            }

            if (Input.GetKey(KeyCode.S) || coordinate.y < 0)
            {
                playerDirection.y = -1.0f;
            }

            if (Input.GetKey(KeyCode.D) || coordinate.x > 0)
            {
                playerDirection.x = 1.0f;
            }
        }
    }

    private void CheckMove()
    {
        if (player.transform.position.z < -28.0f || 
            player.transform.position.z > 28.0f ||
            player.transform.position.x < -26.0f ||
            player.transform.position.x > 26.0f)
        {
            fade = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            Debug.Log("Hi!");
        }

        fade = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    private void Look()
    {
        if (Input.GetMouseButton(1) || global.platform == true)
        {
            
            Cursor.visible = false;
            if (global.platform == false) // PC
            {
                cameraRotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                cameraRotation.x += Input.GetAxis("Mouse Y") * -lookSpeed;
            }
            else // Mobile
            {
                Vector2 coordinate = lookJoystick.Coordinate();
                cameraRotation.y += coordinate.x; // * lookSpeed;
                cameraRotation.x += -coordinate.y; // * -lookSpeed;
            }

            if (global.currentScene == global.startScene)
            {
                cameraRotation.y = Mathf.Clamp(cameraRotation.y, -180.0f, 0.0f);
                camera.transform.eulerAngles = new Vector3(cameraRotation.y, 270.0f, -90.0f);
            }
            else
            {
                cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90.0f, 90.0f);
                camera.transform.eulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, 0.0f);
            }
        } else
        {
            Cursor.visible = true;
        }
    }
}
