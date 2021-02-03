using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script was mainly used to debug in VR. since the headset does not have a console to inform the programmer of errors, they were instead printed
//via a UI component called the notice handler. This would print the errors to the UI while in vr, to ensure proper debugging

public class noticehandler : MonoBehaviour {

	GameObject notice1;
	GameObject notice2;
	GameObject notice3;
	Text notice1text;
	Text notice2text;
	Text notice3text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void handlenotice(string notice) {
		notice1 = gameObject.transform.GetChild(0).gameObject;
		notice2 = gameObject.transform.GetChild(1).gameObject;
		notice3 = gameObject.transform.GetChild(2).gameObject;
		notice1text = notice1.GetComponent<Text>();
		notice2text = notice2.GetComponent<Text>();
		notice3text = notice3.GetComponent<Text>();
		notice3text.text = notice2text.text;
		notice2text.text = notice1text.text;
		notice1text.text = notice;
		Debug.Log("notice handled");
		
		
	}

}
