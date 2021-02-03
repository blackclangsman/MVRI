using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script governing red coins

public class HealthItemscript : MonoBehaviour {

	float turnSpeed = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		turnSpeed = 100f;
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			Debug.Log("Entered health item object");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			//invincibility was scrapped for the final build
			if (this.tag == "invi") {
				playcont.son.invincibleandpowerups();
				playcont.invincible = true;
				float speedscore = 5*playcont.moveSpeed/2;
				playcont.score = playcont.score+(int)speedscore;
				Destroy(this.gameObject);
			}
			//upon collision with a red coin, increase the players extra hits by 1
			else if (this.tag == "hititem") {
				//playcont.notice = "Ekstra liv!";
				playcont.notice = "1UP";
				playcont.son.extrahit();
				float speedscore = 5*playcont.moveSpeed/2;
				playcont.score = playcont.score+(int)speedscore;
				if (playcont.hits < 10) {
					playcont.hits++;
				} else {
					speedscore = 5*playcont.moveSpeed/2;
					playcont.score = playcont.score+(int)speedscore;
				}
				Destroy(this.gameObject);
				
			}
		}
		
	}
}
