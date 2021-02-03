using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slidertext : MonoBehaviour {
	Text slitext;
	
	void Start() {
		slitext = GetComponent<Text>();
	}
	
	public void textUpdate(float value) {
		//slitext.text = Mathf.RoundToInt(value) + "";
		if (this.gameObject.tag == "speedslider") {
			//value = value+3;
		}
		slitext.text = Mathf.RoundToInt(value) + "";
	}
	
	public void textUpdateSpeed(float value) {
		//slitext.text = Mathf.RoundToInt(value) + "";
		//value = value+3;
		slitext.text = Mathf.RoundToInt(value) + "";
	}
}