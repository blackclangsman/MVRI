using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a teleporter used for the easy stage with turning, it was meant to have the player be teleported to a secret area
//if they drove at a certain part with a certain speed
//as stages with turning are scrapped in the final build, this script has no function

public class teleporter : MonoBehaviour {

	public Transform telepo;
	bool check = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && this.gameObject.CompareTag("88miles")) { 
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			playcont.son.startup();
			//if (playcont.rb.velocity.magnitude > 88.0f && check == false) {
			if (playcont.rb.velocity.magnitude > 60.0f && check == false) {
				Debug.Log("triggered");
				check = true;
				other.transform.position = telepo.position;
			}
		}
	}
}
