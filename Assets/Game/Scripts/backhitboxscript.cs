using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is a remnant from when the player could turn at will, it was to ensure the player couldn't rack up laps by driving in the wrong direction
//in the final game this is a non issue and as such the goal object can be simplified to just a collider since there is no way to go through the goal the wrong way
//effectively making this script redundant

public class backhitboxscript : MonoBehaviour {
	
	public bool direction = false; //which direction is the vehicle coming from? true = its coming from this direction
	public bool overlap = false; //is the object in both colliders at once?
	public bool entered1;
	public bool entered2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "AI1") {
			entered1 = true;
			GameObject fron = GameObject.Find("FrontHitBox");
			Fronthitboxscript front = fron.GetComponent<Fronthitboxscript>();
			if (front.direction == true) { //is it coming from the other direction?
				overlap = true;
				front.direction = false;
			} else {
				direction = true;
			}
		}
		if (other.gameObject.tag == "AI2") {
			entered2 = true;
			GameObject fron = GameObject.Find("FrontHitBox");
			Fronthitboxscript front = fron.GetComponent<Fronthitboxscript>();
			if (front.direction == true) { //is it coming from the other direction?
				overlap = true;
				front.direction = false;
			} else {
				direction = true;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "AI1") {
			entered1 = false;
			if (direction != true && overlap != true) {
				PlayerController playcont = gameObject.GetComponentInParent<PlayerController>();
				//go up one placing
				if (playcont.placing != 3) {
					playcont.placing++;
					GameObject fron = GameObject.Find("FrontHitBox");
					Fronthitboxscript front = fron.GetComponent<Fronthitboxscript>();
					front.direction = false;
				}
			} else {
				overlap = false;
			}
		}
		if (other.gameObject.tag == "AI2") {
			entered2 = false;
			if (direction != true && overlap != true) {
				PlayerController playcont = gameObject.GetComponentInParent<PlayerController>();
				//go up one placing
				if (playcont.placing != 3) {
					playcont.placing++;
					GameObject fron = GameObject.Find("FrontHitBox");
					Fronthitboxscript front = fron.GetComponent<Fronthitboxscript>();
					front.direction = false;
				}
			} else {
				overlap = false;
			}
		}
	}
	
	
	
	
}
