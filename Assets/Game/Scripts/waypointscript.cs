using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the waypoints found around the games stages. Its primarry function is to increment the indices in the player and AIs waypoint
//arrays to ensure they're moving towards the right one and to keep track of where they are placed in the race

public class waypointscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//some waypoints are shared by the two AI's and some are not. the first if statement applies to the first AI only
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "AI1" && (gameObject.tag == "waypoint1" || gameObject.tag == "waypoint12")) { 
			AIscript aicont = other.GetComponent<AIscript>();
			Debug.Log(aicont.target[aicont.current].name);
			Debug.Log(gameObject.name);
			//upon collision with the waypoint, change the target waypoint to the next one in the waypoint list
			if (gameObject.transform.position == aicont.target[aicont.current].position) {
				if (aicont.current ==  aicont.target.Length-1) {
					aicont.current = 0;
					Debug.Log("reached");
				} else {
					aicont.current++;
					Debug.Log("sekiro shadows die " + aicont.target.Length);
				}
			}
		}
		//the same as above is true for AI2 and its waypoints
		if (other.gameObject.tag == "AI2" && (gameObject.tag == "waypoint2" || gameObject.tag == "waypoint12")) { 
			AIscript aicont = other.GetComponent<AIscript>();
			Debug.Log(aicont.target[aicont.current].name);
			Debug.Log(gameObject.name);
			if (gameObject.transform.position == aicont.target[aicont.current].position) {
				if (aicont.current ==  aicont.target.Length-1) {
					aicont.current = 0;
					Debug.Log("reached");
				} else {
					aicont.current++;
					Debug.Log("sekiro shadows die " + aicont.target.Length);
				}
			}
		}
		//if the player drives past one of the waypoints, the index of their current waypoint is moved to the next waypoint
		if (other.gameObject.tag == "Player") { 
			PlayerController playcont = other.GetComponent<PlayerController>();
			Debug.Log("Player reached a waypoint for " + gameObject.name[0]);
			//this if statement is called if the waypoint is on AI1's path
			//the two are AI's have their waypoints seperated into two so as to not
			//make them follow the same path
			if (gameObject.tag == "waypoint1") { //waypoint for 1
				if (playcont.current ==  playcont.checkpoint1.Length-1) {
					playcont.current = 0;
					playcont.finalwaypoint1 = true;
					Debug.Log("player reached");					
				} else {
					playcont.current++;
					Debug.Log("player reached waypoint");
				}
			//this if statement is called if the waypoint is on AI 2's path
			} else if (gameObject.tag == "waypoint2") {//waypoint for 2
				if (playcont.current2 ==  playcont.checkpoint2.Length-1) {
					playcont.current2 = 0;
					playcont.finalwaypoint2 = true;
					Debug.Log("player reached");					
				} else {
					playcont.current2++;
					Debug.Log("player reached waypoint");
				}
			//this if statement is triggered if the waypoint is shared by the two AI's
			} else if (gameObject.tag == "waypoint12") {//both of them
				if (playcont.current ==  playcont.checkpoint1.Length-1) {
					playcont.current = 0;
					playcont.finalwaypoint1 = true;
					Debug.Log("player reached");					
				} else {
					playcont.current++;
					Debug.Log("player reached waypoint");
				}
				if (playcont.current2 ==  playcont.checkpoint2.Length-1) {
					playcont.current2 = 0;
					playcont.finalwaypoint2 = true;
					Debug.Log("player reached");					
				} else {
					playcont.current2++;
					Debug.Log("player reached waypoint");
				}
			}
		}
		
	}
}
