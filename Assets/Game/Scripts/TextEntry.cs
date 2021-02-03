using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script was originally used for allowing the player to enter their name at the title screen, but was scrapped

public class TextEntry : MonoBehaviour {
	
	public InputField iField;
	string text = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void TextEnt() {
		Debug.Log(iField.text);
		text = iField.text;
		if (text != null) {
			GameObject setlist = GameObject.Find("SetingsList");
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			settlist.name = text;
		} else {
			GameObject setlist = GameObject.Find("SetingsList");
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			settlist.name = " ";
		}
		
	}
	
}