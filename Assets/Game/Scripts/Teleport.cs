using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is called when the player or AI enters the passages at the end of the stages
//its function is to teleport the player to the other end of the stage to looop it, as well as
//set the next waypoint target to the one after they passage exit

public class Teleport : MonoBehaviour {

	public Transform destination = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "AI1" || other.gameObject.tag == "AI2") {
			other.transform.position = destination.position;
			if (other.gameObject.tag == "AI1" || other.gameObject.tag == "AI2") {
				AIscript aicont = other.gameObject.GetComponent<AIscript>();
				aicont.current++;
			} else if (other.gameObject.tag == "Player") {
				PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
				playcont.current++;
				playcont.current2++;
			}
		}
		
	}
}
