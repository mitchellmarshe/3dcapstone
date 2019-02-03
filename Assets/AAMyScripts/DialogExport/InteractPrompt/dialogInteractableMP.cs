using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class dialogInteractableMP : NetworkBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerScript;
    public GameObject character;
    public CharacterController controller;
    public GameObject interactUI;
    public GameObject Activated;
    private bool inZone;
    public bool locked;

    void Start()
    {
        locked = false;
        inZone = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller.enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            character = other.gameObject;
            controller = other.GetComponent<CharacterController>();
            playerScript = other.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            interactUI.SetActive(true);
            inZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interactUI.SetActive(false);
            inZone = false;
        }
    }

    void Update()
    {
        if (inZone)
        {
            if (Input.GetKey("e"))
            {
                locked = true;
                Activated.SetActive(true);
            }

            if (Input.GetKey("q"))
            {
                locked = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerScript.enabled = true;
                controller.enabled = true;

                Activated.SetActive(false);
            }
            if (locked)
            {
                playerScript.enabled = false;
                controller.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
