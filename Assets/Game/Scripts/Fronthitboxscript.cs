using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//originally used when it was possible for the player to turn manually, like the other direction check scripts, it can be disregarded

public class Fronthitboxscript : MonoBehaviour {
	
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
			GameObject bacc = GameObject.Find("BackHitBox");
			backhitboxscript back = bacc.GetComponent<backhitboxscript>();
			if (back.direction == true) { //is it coming from the other direction?
				overlap = true;
				back.direction = false;
			} else {
				direction = true;
			}
		}
		if (other.gameObject.tag == "AI2") {
			entered2 = true;
			GameObject bacc = GameObject.Find("BackHitBox");
			backhitboxscript back = bacc.GetComponent<backhitboxscript>();
			if (back.direction == true) { //is it coming from the other direction?
				overlap = true;
				back.direction = false;
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
				if (playcont.placing != 1) {
					playcont.placing--;
					GameObject bacc = GameObject.Find("BackHitBox");
					backhitboxscript back = bacc.GetComponent<backhitboxscript>();
					back.direction = false;
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
				if (playcont.placing != 1) {
					playcont.placing--;
					GameObject bacc = GameObject.Find("BackHitBox");
					backhitboxscript back = bacc.GetComponent<backhitboxscript>();
					back.direction = false;
				}
			} else {
				overlap = false;
			}
		}
	}
	
	
}
