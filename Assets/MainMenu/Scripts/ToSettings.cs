using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToSettings : MonoBehaviour {

	public void foo() {
		GameObject mainpanel = GameObject.Find("MainMenuPanel");
		GameObject settingspanel = GameObject.Find("SettingsMenu");
		if (mainpanel.activeSelf) {
			Debug.Log("Active Self: " + mainpanel.activeSelf);
			
		}
	}
}
