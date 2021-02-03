using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script exists to adjust the players waypoint in the wild west stage
//once the player uses the shortcut, they do not need to take the long road and as such should not have their current waypoint
//for their distance be set to one they won't reach unless they take the long road. Thus the waypoint index in the players
//script is increased to 3, to signify these waypoints have been skipped

public class dummywaipointwildwest : MonoBehaviour {

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
			playcont.current = 3;
			playcont.current2 = 3;
		}
		
	}
}
