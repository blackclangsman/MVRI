using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is for upgrade items

public class FloorItems : MonoBehaviour {

	// Use this for initialization
	float turnSpeed = 50f;//the speed of which the item is turning on itself
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime); //rotate the item with the defined turning speed
		turnSpeed = 50f;
	}
	
	void OnTriggerEnter(Collider other) {
		//upon player collision activate the items effect depending on what the items tag is
		if (other.gameObject.tag == "Player") { 
			Debug.Log("Entered floor Object");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			//permanent speedboosts, these only appear if the player has lost some speed compared to what they started with
			if (this.tag == "perm") {
				if (playcont.moveSpeed < 135) {
					playcont.moveSpeed += 5;
					//playcont.notice = "Hastigheden er oget igen";
					playcont.notice = "S: +5";
					playcont.son.invincibleandpowerups();
					playcont.speedboostobtain();
				} else {
					float speedscore =3*playcont.moveSpeed/2;
					playcont.score += (int)speedscore;
				}
				
				Destroy(this.gameObject);
			}
			//temporary speed boosts were scrapped for the final game
			else if (this.tag == "temp") {
				playcont.tempspeed = true;
				
			}
			//obtaining the waterraft
			else if (this.tag == "raft") {
				float speedscore = 5*playcont.moveSpeed/2;
				if (playcont.wateraft == true) {
					playcont.score += (int)speedscore;
				}
				playcont.son.invincibleandpowerups();
				playcont.notice = "Anti-vand modul tilsluttet"; //add voice here
				playcont.score += (int)speedscore;
				playcont.wateraft = true;
				playcont.watterraftobtain();
				Destroy(this.gameObject);
			}
		}
		
	}
}
