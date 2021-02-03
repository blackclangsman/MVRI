using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is a remnant from when the player could turn at will, it is not used in the final game
public class directioncheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "AI") {
			Debug.Log("direction ok");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			playcont.directioncheck = true;
		}
		
	}
	
}
