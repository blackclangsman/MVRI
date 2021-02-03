using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsList : MonoBehaviour {
	
	//Slider slider = Instantiate(SF Slider) as Slider;

	//settings
	public bool vr = false;
	public int speed;
	public int volume;
	public bool controller = true;
	public bool speedloss = false;
	public bool AI = true;
	public int cmera = 3; //1 = first, 2 = overhead, 3 = third
	
	//stage and player setttings
	public string name = "";
	public int stage;
	public int vehicle = 1; //1 = sportscar, 2 = cartoony car, 3 = truck
	
	//doctor settings
	public int duration;
	public int sensitivity;
	public int whenmovement = 1;
	public int hits = 0;
	public bool machsound = false;
	
	//game variables
	public int score;
	public bool debugging;
	public int level = 0;
	public int ai1;
	public int ai2;
	public bool tryit = false;
	public bool tutorial = false;
	
	void Start() {
		speed = 5;
		volume = 5;
		sensitivity = 5;
		duration = 5;
		debugging = false;
		score = 0;
		//cmera = 3;
		if (OVRManager.tiledMultiResSupported) {
			// This is an Oculus Go
			vr = true;
		} else {
			// This is a phone
			vr = false;
		}
		
		
		DontDestroyOnLoad(this.gameObject);
	}
	/*
	void Awake() {
        DontDestroyOnLoad(this.gameObject);
	}
    */
	
}
