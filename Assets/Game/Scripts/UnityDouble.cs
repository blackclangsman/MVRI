using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//upon finding a stages secret passage the players score is doubled

public class UnityDouble : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			Debug.Log("Found Unity-chan. score doubled");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			playcont.score = playcont.score*2;
			playcont.notice = "Hey! Du fandt mig!";
			playcont.unitychanfound();
			playcont.unitychan = true;
			playcont.van.secret();
			Destroy(this.gameObject);
		}
	}
}
