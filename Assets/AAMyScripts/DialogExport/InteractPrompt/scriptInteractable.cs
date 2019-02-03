using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptInteractable : MonoBehaviour {

	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
	public GameObject interactUI;
	public MonoBehaviour Activated;
	private bool inZone;

	void Start(){
		inZone = false;
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

	void Update(){
		if (inZone) {
			if (Input.GetKey ("e")) {
				Activated.enabled = true;
			}
				
		}
	}
}
