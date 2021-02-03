using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliderhandler : MonoBehaviour {

	public void changeSpeed(float speed) {
		int speed2 = (int) speed;
		GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		Debug.Log ( "speed is: " + speed);
		settlist.speed = speed2;
	}
	
	public void changeSensitivity(System.Single sensitivity) {
		int sensitivity2 = (int) sensitivity;
		GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		Debug.Log ( "sensitivity is: " + sensitivity);
		settlist.sensitivity = sensitivity2;
	}
	
	public void changeDuration(System.Single duration) {
		int duration2 = (int) duration;
		GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		Debug.Log ( "duration is: " + duration);
		settlist.duration = duration2;
	}
	
	
	public void changeHits(System.Single hits) {
		int hits2 = (int) hits;
		GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		Debug.Log ( "hits is: " + hits2);
		settlist.hits = hits2;
	}
	
	
	public void changeVolume(System.Single vol) {
		int vol2 = (int) vol;
		GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		Debug.Log ( "vol is: " + vol);
		settlist.volume = vol2;
		GameObject soundvr = GameObject.Find("SoundmanagerVR");
		soundmanager sonvr = soundvr.GetComponent<soundmanager>();
		GameObject sound = GameObject.Find("Soundmanager");
		soundmanager son = sound.GetComponent<soundmanager>();
		son.volume(vol);
		sonvr.volume(vol);
	}
}
