using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for if the player takes the shortcut in the bridges stage
//its function is identical to the dummywaypointwildwest script

public class dummywaypointbrigdes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			PlayerController playcont = other.GetComponent<PlayerController>();
			Debug.Log("Player reached a dummy waypoint");
			playcont.current = 16;
			playcont.current2 = 13;
		}
		
	}
}
