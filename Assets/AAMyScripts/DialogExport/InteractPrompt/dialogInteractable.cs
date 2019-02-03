using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogInteractable : MonoBehaviour {
    public GameObject controller;
    //public UnityStandardAssets.Characters.FirstPerson.playerScriptSP controller;
    public GameObject interactUI;
	public GameObject Activated;
	private bool inZone;
    private GameObject lastCanvas;

	void Start(){
		inZone = false;
        lastCanvas = null;

	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			interactUI.SetActive (true);
			inZone = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			interactUI.SetActive (false);
			inZone = false;
		}
	}

    public void RegisterNewCanvas(GameObject other)
    {
        lastCanvas = other;
    }

	void Update(){
		if (inZone) {
			if (Input.GetKey ("e")) {
                controller.GetComponent<mouseLocker>().unlockMouse();
                //controller.enabled = false;
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
                Activated.SetActive (true);
                lastCanvas = Activated;
			}

			if (Input.GetKey ("q")) {
                controller.GetComponent<mouseLocker>().lockMouse();
                //Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;
                //controller.enabled = true;
                lastCanvas.SetActive (false);

            }
		}
	}
}
