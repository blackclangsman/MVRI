using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//this script ensures that the UI didplays the right values

public class UIHandler : MonoBehaviour {
	Text UItext; //the text component
	public int fuel; //fuel/duration
	public float startfuel; //start fuel
	int checksec = 0; //check for if a second has passed
	GameObject play; //the player
	public float fuelpercent;
	PlayerController cont; //the player controller
	
	void Start() {
		//first find the game manager and add the proper values to duration, if the game manager cannot be found duration is set to 1 minute
		GameObject play = GameObject.Find("Player");
		cont = play.GetComponent<PlayerController>();
		
		UItext = GetComponent<Text>();
		string label = UItext.ToString();
		try {
			GameObject setlist = GameObject.Find("SetingsList");
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			if (settlist != null) {
				fuel = settlist.duration*60;
				
				
			}
		} catch (Exception e) {
			fuel = 60;
		}
		startfuel = fuel+0.1f;
		if (label[0] == 'F') {
			UItext.text = "F: 100%";
		}
		
	}
	
	void Update() {
		//stringsplit
		UItext = GetComponent<Text>();
		string label = UItext.ToString();
		//velocity
		//all the UI components work identically, they retrieve the label of the text string and then update it to ensure it is up to date
		//with the values in the player controller
		if (label[0] == 'V') {
			Debug.Log("yup");
			float vel = 0.0f;
			vel = cont.rb.velocity.magnitude;
			int vel2 = (int)vel;
			//UItext.text = "Velocity: " + vel2;
			//UItext.text = "Hastighed: " + vel2;
			UItext.text = "S: " + vel2;
			
		//fuel
		} else if (label[0] == 'F') {
			//Fuel works differently from the others, as it is counted in percentage. it counts down every second, as kept track of via the enumerator function			
			if (checksec == 0 && cont.started == true) {
				StartCoroutine(ExampleCoroutine());
			}
			
			//do percentage calculations here
			
			
			
		//Movement, scrapped
		} else if (label[0] == 'M') {
			Debug.Log("yup");
			int vel = 0;
			//GameObject play = GameObject.Find("GameManager");
			//gamemaanager cont = play.GetComponent<gamemaanager>();
			//figure out how to measure movement
			//UItext.text = "Movement: " + vel;
			vel = cont.hits;
			//UItext.text = "Hits: " + vel;
			//UItext.text = "Liv: " + vel;
			UItext.text = "X: " + vel;
		//hits
		} else if (label[0] == 'H') {
			Debug.Log("yuppie");
			int vel = 0;
			vel = cont.hits;
			//figure out how to measure movement
			//UItext.text = "Hits: " + vel;
			//UItext.text = "Liv: " + vel;
			UItext.text = "X: " + vel;
		//score
		} else if (label[0] == 'S') {
			Debug.Log("yup");
			int vel = 0;
			vel = cont.score;
			//UItext.text = "Score: " + vel;
			//UItext.text = "Point: " + vel;
			UItext.text = "P: " + vel;
		//laps
		} else if (label[0] == 'L') {
			Debug.Log("yup");
			int vel = 0;
			vel = cont.laps;
			if (vel < 0) {
				vel = 0;
			}
			//UItext.text = "Laps: " + vel;
			//UItext.text = "Runder: " + vel;
			UItext.text = "R: " + vel;
		//place
		} else if (label[0] == 'P') {
			Debug.Log("yup");
			int vel = 0;
			vel = cont.placing;
			//UItext.text = "Placing: " + vel;
			//UItext.text = "Nummer: " + vel;
			UItext.text = "N: " + vel;
		}
	}
	
	
	//the enumertor function displays the amount of fuel left in percentage and counts down every second
    IEnumerator ExampleCoroutine() {
		checksec = 1;
        yield return new WaitForSeconds(1.0f);
		if (fuel > 0) {
			fuel--;
		} else {
			//find goalpost and end game
		}
		int vel = fuel;
		fuelpercent = ((float)fuel/(float)startfuel)*100.0f;
		//do percent calcluations here
		//UItext.text = "Fuel: " + (int)fuelpercent + "%";
		//UItext.text = "Tank: " + (int)fuelpercent + "%";
		UItext.text = "F: " + (int)fuelpercent + "%";
		checksec = 0;

    }
}