using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for the coins

public class ItemScript : MonoBehaviour {



	float turnSpeed = 100f;
	AudioSource sound;
	
	
	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//spin
		transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		sound.clip = Resources.Load<AudioClip>("coin");
		turnSpeed = 100f;
	}
	
	//upon collision increase the players score
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			Debug.Log("Got coin, score +100");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			float speedscore = (3*playcont.moveSpeed)/2;
			//if the players speed has been reduced from obstaces, increase it slightly
			if (playcont.moveSpeed < playcont.savedspeed) {
				playcont.moveSpeed++;
			}
			playcont.score = playcont.score+=(int)speedscore;
			playcont.coins++;
			//give player an extra life if theyve collected 30 coins
			for (int i = 1; i < 10; i++) {
				if (playcont.coins < i*30+1) {
					if (playcont.coins == i*30) {
						playcont.hits++;
						playcont.son.extrahit();
					}
				} else {
					break;
				}
			}
			//sound.Play();
			playcont.son.coin();
			//sound.PlayOneShot();
			Destroy(this.gameObject);
		}
	}
}
