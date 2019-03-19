using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{
    [Header("Scripts")]
    public Global global;
    public Actions actions;

    [Header("Player")]
    public GameObject player;
    public GameObject camera;
    public float walkSpeed;
    public float lookSpeed;

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
        cameraRotation = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void Start()
    {
        actions.Action0();
    }

    private void Update()
    {
        
        Move();
        Look();

        actions.Action1(false);
        actions.Action2(false);
        actions.Action3(false);
        actions.Action4(false);
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
        playerDirection = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))
        {
            playerDirection.y = 1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerDirection.x = -1.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerDirection.y = -1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerDirection.x = 1.0f;
        }
    }


    private void Look()
    {
        if (Input.GetMouseButton(1))
        {
            if (global.platform == false) // PC
            {
                cameraRotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                cameraRotation.x += Input.GetAxis("Mouse Y") * -lookSpeed;
            }
            else // Mobile
            {
                //cameraRotation.y += (deltaPosition.x < 0.0f ? -1.0f : 1.0f) * lookSpeed;
                //cameraRotation.x += (deltaPosition.y < 0.0f ? 1.0f : -1.0f) * lookSpeed;
            }

            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90.0f, 90.0f);
            camera.transform.eulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, 0.0f);
        }
    }

    /*
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
                if (RectTransformUtility.RectangleContainsScreenPoint(mobileLookJoystick.GetComponent<RectTransform>(), touch.position))
                {
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
    */
}
