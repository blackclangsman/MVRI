using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script that governs what the scoreboard at the end of the game should display

public class gamendUI : MonoBehaviour {
	
	Text name;
	Text place;
	Text score;
	Text speed;
	Text laps;
	Text movement;
	Text total;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public int showendscreen() {
		
		//find gamemanager and player to fetch the appropriate information
		GameObject player = GameObject.Find("Player");
		PlayerController playcont = player.GetComponent<PlayerController>();
		GameObject gamman = GameObject.Find("GameManager");
		gamemaanager man = gamman.GetComponent<gamemaanager>();
		name = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		place = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
		score = gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
		speed = gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
		laps = gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
		movement = gameObject.transform.GetChild(5).gameObject.GetComponent<Text>();
		total = gameObject.transform.GetChild(6).gameObject.GetComponent<Text>();
		
		//then award bonus points based on how well it was fulfilled
		int playerscore = playcont.score;
		int speedscore = (int)playcont.moveSpeed*10;
		int lapscore = (playcont.laps*100)/playcont.duration;
		int placescore = (250)/playcont.placing;
		float moves = man.movement;
		int movementscore = 10000/((int)moves+1); //the more you move the less of this extra bonus you get
		int totalscore = playerscore + speedscore + lapscore + placescore + movementscore;
		
		//finally set the text comonent of the scoreboard to the values specified
		name.text = playcont.name;
		place.text = "Nummer "+ " " + playcont.placing;
		score.text = score.text + " " + playerscore;
		place.text = "Nummer " + " " + playcont.placing + " : +" + placescore;
		speed.text = "Hastighed " + " " + playcont.moveSpeed + " : +" + speedscore;
		laps.text = "Runder " + " " + playcont.laps + " : +" + lapscore;
		movement.text = "Bevaegelser " + " " + man.movement + " : +" + movementscore;
		total.text = total.text + " " + totalscore;
		return totalscore;
		
		
	}
	
	
}
