using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script was used to calculate the placing of the player in the race before the distance script was used, it is not utilized in the final game

public class behindAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		AIscript al = gameObject.GetComponentInParent<AIscript>();
		if (other.gameObject.tag == "Player") {
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			if (al.placing > playcont.placing) {
				if (playcont.placing != 3) {
					playcont.placing++;
				}
				if (al.placing != 1) {
					al.placing--;
				}
				Debug.Log("went down to " + playcont.placing + "place");
			}
		}
		if (other.gameObject.tag == "AI1" || other.gameObject.tag == "AI2") {
			AIscript cont = other.gameObject.GetComponent<AIscript>();
			if (al.placing > cont.placing) {
				if (cont.placing != 3) {
					cont.placing++;
				}
				if (al.placing != 1) {
					al.placing--;
				}
			}
			Debug.Log(other.gameObject.tag + " went down to " + cont.placing + "place");
		}
		
	}
	
}
