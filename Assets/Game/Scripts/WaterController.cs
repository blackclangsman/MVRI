using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

	//when the player enters the water their speed is halved
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("Entered water, speed halved");
			//Destroy(MedicalBox);
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			if (playcont.wateraft != true) {
				//playcont.notice = "Vand saenker farten";
				playcont.notice = "S↓";
				playcont.moveSpeed = playcont.moveSpeed/2;
				playcont.rb.velocity = Vector3.zero;
			}
		}
	}
	
	//when the player exits the water their speed is restored
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("Exited water, speed normal");
			//moveSpeed=1;
			//Destroy(MedicalBox);
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			if (playcont.wateraft != true) {
				playcont.moveSpeed = playcont.moveSpeed*2;
			}
		}
	}
	
	//as long as the player stays in the water the aquaplaning sound will play
	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			if (playcont.rb.velocity.magnitude < 15) {
				playcont.son.aquaplaningslow();
			} else {
				playcont.son.aquaplaning();
			}
		}
	}
}
