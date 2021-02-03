using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//script that controls the AI behavior

public class AIscript : MonoBehaviour {
	
	public int ai; //variable denoting which ai it is
	
	//settings
	public int startspeed = 1;	//starting speed
	public bool speedloss = false; //permanent speedloss?
	public int vehicle = 0; //selected vehicle, 1 = sportscar
	
	//scores
	public int laps = -1; //amount of laps done
	public int placing = 2; //placing in race
	
	
	//properties
	//int currentspeed;
	public float moveSpeed; //current speed
    public float turnSpeed; //turning speed
	public float savedspeed; //svaed speed, used to save the intial speed after a speedloss
	public bool started = false; //checks if game has started yet
	public bool ended = false; //checks if game has ended, if so the player presses a button to load scene
	bool speedcheck = false; //used as a flag to check if the speedup function can be called again yet
	//bool speedup = false; //
	
	//powerups
	public bool directioncheck = false; //check if pointing the the right direction, originally used for goalposts but unused int he final game
	public bool wateraft = false; //checks if the watteraft has been obtained
	public bool tempspeed = false; //originally used for temporary speedboosts, scrapped in final build
	public bool invincible = false; //originally used for brief invincibility, scrapped in final build
	public int hits = 0; //amount of hits left
	
	public Rigidbody rb; //rigidbody unity component
	float fallZone = -10f; //for if the object should fall off the stage, mostly used for debugging
	public Transform playerSpawnPoint = null; //the specified spawn point if the object falls off
	
	//int check = 0;
	
	public Transform[] target; //waypoint array, contains the points the AI drives towards, the waypoints are added to the array in the unity editor
	public Vector3 posit; //keeps track of where the object is
	public int current; //the current target waypoint's array index
	//private bool init = true;
	
	//player controller
	GameObject play;
	PlayerController cont;
	

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody> ();
		
		//retrieve the settings stored in settingslist
		GameObject setlist = GameObject.Find("SetingsList");
		
		try {
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			//GameObject player = GameObject.Find("Player");
			//PlayerController playcont = player.GetComponent<PlayerController>();
			//get settings list from menu
			if (settlist != null) {
				
				if (gameObject.CompareTag("AI1")) {
					vehicle = settlist.ai1;
				} else {
					vehicle = settlist.ai2;
				}
				//set the appropriate vehicle to active
				if (vehicle == 1) {
					//use sportscar
					vehicle = 2;
					gameObject.transform.GetChild(0).gameObject.SetActive(true);
					gameObject.transform.GetChild(1).gameObject.SetActive(false);
					gameObject.transform.GetChild(2).gameObject.SetActive(false);
				} else if (vehicle == 2) {
					//use postcar
					vehicle = 3;
					gameObject.transform.GetChild(0).gameObject.SetActive(false);
					gameObject.transform.GetChild(1).gameObject.SetActive(true);
					gameObject.transform.GetChild(2).gameObject.SetActive(false);
				} else if (vehicle == 3){ //3
					//use truck
					vehicle = 1;
					gameObject.transform.GetChild(0).gameObject.SetActive(false);
					gameObject.transform.GetChild(1).gameObject.SetActive(false);
					gameObject.transform.GetChild(2).gameObject.SetActive(true);
				} else {
					Debug.Log("undefined. using default");
				}
				
			}
		} catch (Exception e) {
			Debug.Log("Settings list not found. using default vehicles");
		}
		
		if (gameObject.CompareTag("AI1")) {
			placing = 2;
		} else {
			placing = 3;
		}
		//save the specified speed into savedspeed
		turnSpeed = 50.0f;
		savedspeed = moveSpeed;
		//store the player into the object
		play = GameObject.Find("Player");
		cont = play.GetComponent<PlayerController>();
		
	
	}
	
	// Update is called once per frame
	void Update () {
		posit = transform.position;
		//check if race has started, do nothing if it hasn't
		if (started == false) {
			if (cont.started == true) {
				started = true;
				Debug.Log("AI started");
			}
		//if race has ended stop driving
		} else if (ended == true) {
			rb.velocity = Vector3.zero;
		} else {
			//follow the path
			Debug.Log("Searching for point " + current);
			if (transform.position != target[current].position) {
				//move towards the waypoint
				Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, moveSpeed*Time.deltaTime);
				rb.MovePosition(pos);
				Vector3 targetDirection = target[current].position - transform.position;

				// The step size is equal to speed times frame time.
				float singleStep = turnSpeed * Time.deltaTime;

				// Rotate the forward vector towards the target direction by one step
				Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

				// Draw a ray pointing at the target
				Debug.DrawRay(transform.position, newDirection, Color.red);

				// Calculate a rotation a step closer to the target and applies rotation to this object
				transform.rotation = Quaternion.LookRotation(newDirection);
				
				
			} else {
				//current = (current + 1) % target.Length;
			}
			
		}
		//check if enough time has passed for the vehicle to speed up
		if (speedcheck = false && cont.placing != 1) {
			speedcheck = true;
			int duration = 0;
			//roll a random number to determine if the vehicle can speed up
			int rng = (int)UnityEngine.Random.Range(0.0f, 100.0f);
			if (rng > 95) {
				moveSpeed = moveSpeed*2;
			} else {
				moveSpeed = savedspeed;
			}
			//the duration of the speed up depends on the AI, making one slightly tougher
			if (gameObject.CompareTag("AI1")) {
				duration = 10;
				StartCoroutine(speeduptime(duration));
			} else {
				duration = 20;
				StartCoroutine(speeduptime(duration));
			}
			//get a random number
			
		}
		
		
	}
	
	
	//if player hit AI player, they lose points, unless they have a cheat enabled
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			PlayerController playcont = collision.gameObject.GetComponent<PlayerController>();
			if (playcont.aibuster == true) {
				playcont.notice = "Odelagde en spiller!";
				playcont.score += 500;
				if (GameObject.Find("AI1") != null || GameObject.Find("AI12") != null) {
					if (playcont.placing != 1) {
						playcont.placing++;
					}
				}
				Destroy(this.gameObject);
			} else {
				//playcont.notice = "Ramte en spiller!";
				playcont.notice = "P: -500";
				playcont.score -= 500;
			}
			playcont.son.crash();
		}
        
    }
	
	//enumerator function that keeps track of how much time has passed before rerolling random number for speedup
	IEnumerator speeduptime(int duration) {
		yield return new WaitForSeconds(duration);
		speedcheck = false;
	}
	
	
	
	
}
