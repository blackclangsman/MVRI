using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//when player collides with wall, the wall function in the player script is called to reduce their speed
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			Debug.Log("Broke wall, -5 speed");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			playcont.son.wall();
			if (playcont.hits < 1) {
				playcont.wall();
			} else {
				if (playcont.invincible != true) {
					playcont.hits--;
				}
			}
			Destroy(this.gameObject);
		}
	}
}
