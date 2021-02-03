using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {

	public int curtut = 0; //the current tutorial
	public int prevtut = -1; //the previous tutorial, if not equal to current tutorial, play the tutorial sound
	PlayerController playcont; //the player
	vamanager van; //the voice manager


	// Use this for initialization
	void Start () {
		curtut = 0;
		prevtut = -1;
		//set the tutorial to tru in the game manager
		GameObject gam = GameObject.Find("GameManager");
		gamemaanager manag = gam.gameObject.GetComponent<gamemaanager>();
		manag.tutorial = true;
		GameObject play = GameObject.Find("Player");
		playcont = play.gameObject.GetComponent<PlayerController>();
		van = play.transform.GetChild(7).GetChild(1).gameObject.GetComponent<vamanager>();
		//by turning debug mode on the countdown is skipped when starting
		playcont.debugging = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		//when colliding with a tutorial point, current tutorial is incremented and the tutorial sound file is played
		//at the end the previous tutorial is set to the current tutorial to ensure the tutorial doesn't loop
		if (curtut != prevtut) {
			//do the tutorials
			switch (curtut) {
			case 0:
				//playcont.readytostart = false;
				//start coroutine
				//explain basics, car diving by itself, keep reticle at center, bonuses, explain two other cars, etc.
				Debug.Log("First tutorial");
				van.tut1();
				break;
			case 1:
				//explain coins
				Debug.Log("Second tutorial");
				van.tut2();
				break;
			case 2:
				//explain walls
				Debug.Log("Fourth tutorial");
				van.tut3();
				break;
			case 3:
				//explain regaining speed
				Debug.Log("Fifth tutorial");
				van.tut4();
				break;
			case 4:
				//explain water
				Debug.Log("Sixth tutorial");
				van.tut5();
				break;
			case 5:
				//explain waterraft
				Debug.Log("Seventh tutorial");
				van.tut6();
				break;
			case 6:
				//explain extra lives
				Debug.Log("Eighth tutorial");
				van.tut7();
				break;
			case 7:
				//explain switches and doors
				Debug.Log("Ninth tutorial");
				van.tut8();
				break;
			case 8:
				//explain goal post, secrets and what youre scored on at end of game, explain fuel tank and race end when its empty
				Debug.Log("Tenth tutorial");
				van.tut9();
				break;
			case 9:
				//drive through goalpost to end game, also explain unitychan
				Debug.Log("Final tutorial");
				GameObject setlist = GameObject.Find("SetingsList");
				SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.tutorial = false; 
				break;
			}
			//increment prevtut to keep track of which tutorial we are at
			prevtut++;
		}
	}
	
	
	IEnumerator waitt(float duration) {
		yield return new WaitForSeconds(duration);
	}
	
	
}
