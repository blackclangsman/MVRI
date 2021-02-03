using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour {

	Text scoretext;
	Save save;
	// Use this for initialization
	void Start () {
		scoretext = GetComponent<Text>();
		string score = scoretext.ToString();
		GameObject titman = GameObject.Find("TitleScreenManager");
		TitleScreenManager scores = titman.GetComponent<TitleScreenManager>();
		save = scores.save;
		string textline = "";
		if (gameObject.CompareTag("score1") && save != null) {
			for(int i = 0; i < 10; i++) {
				if (save.scores0[i] == null || save.names0[i] == null) {
					string line = "Ingen data" +  System.Environment.NewLine;
					textline = textline + line;
				} else {
					int scor = save.scores0[i];
					string name = save.names0[i];
					string line = name +": "+ scor + " points" + System.Environment.NewLine;
					textline = textline + line;
				}
				
			}
			scoretext.text = textline;
		} else if (gameObject.CompareTag("score2") && save != null) {
			for(int i = 0; i < 10; i++) {
				if (save.scores1[i] == null || save.names1[i] == null) {
					string line = "Ingen data" +  System.Environment.NewLine;
					textline = textline + line;
				} else {
					int scor = save.scores1[i];
					string name = save.names1[i];
					string line = name +": "+ scor + " points" + System.Environment.NewLine;
					textline = textline + line;
				}
				
			}
			scoretext.text = textline;
		} else if (gameObject.CompareTag("score3") && save != null) {
			for(int i = 0; i < 10; i++) {
				if (save.scores2[i] == null || save.names2[i] == null) {
					string line = "Ingen data" +  System.Environment.NewLine;
					textline = textline + line;
				} else {
					int scor = save.scores2[i];
					string name = save.names2[i];
					string line = name +": "+ scor + " points" + System.Environment.NewLine;
					textline = textline + line;
				}
				
			}
			scoretext.text = textline;
		} else if (gameObject.CompareTag("score4") && save != null) {
			for(int i = 0; i < 10; i++) {
				if (save.scores3[i] == null || save.names3[i] == null) {
					string line = "Ingen data" +  System.Environment.NewLine;
					textline = textline + line;
				} else {
					int scor = save.scores3[i];
					string name = save.names3[i];
					string line = name +": "+ scor + " points" + System.Environment.NewLine;
					textline = textline + line;
				}
				
			}
			scoretext.text = textline;
		} else if (gameObject.CompareTag("score5") && save != null) {
			for(int i = 0; i < 10; i++) {
				if (save.scores4[i] == null || save.names4[i] == null) {
					string line = "Ingen data" +  System.Environment.NewLine;
					textline = textline + line;
				} else {
					int scor = save.scores4[i];
					string name = save.names4[i];
					string line = name +": "+ scor + " points" + System.Environment.NewLine;
					textline = textline + line;
				}
				
			}
			scoretext.text = textline;
		} else {
			Debug.Log("save not found");
			scoretext.text = "Kunne ikke finde gem filen";
		}
		//get objects and do not show stages that have not been unlocked
		GameObject men;
		GameObject scoremen;
		if (scores.vr == true) {
			men = GameObject.Find("MenuVR");
			scoremen = men.gameObject.transform.GetChild(0).GetChild(6).gameObject;
		} else {
			men = GameObject.Find("Menu");
			scoremen = men.gameObject.transform.GetChild(0).GetChild(6).gameObject;
		}
		if (save.unlocked[1] == false && scoremen != null) {
			scoremen.gameObject.transform.GetChild(5).gameObject.SetActive(false);
			scoremen.gameObject.transform.GetChild(7).gameObject.SetActive(false);
		} else {
			scoremen.gameObject.transform.GetChild(5).gameObject.SetActive(true);
			scoremen.gameObject.transform.GetChild(7).gameObject.SetActive(true);
		}
		if (save.unlocked[3] == false && scoremen != null) {
			scoremen.gameObject.transform.GetChild(9).gameObject.SetActive(false);
			scoremen.gameObject.transform.GetChild(11).gameObject.SetActive(false);
		} else {
			scoremen.gameObject.transform.GetChild(9).gameObject.SetActive(true);
			scoremen.gameObject.transform.GetChild(11).gameObject.SetActive(true);
		}
		if (save.unlocked[4] == false && scoremen != null) {
			scoremen.gameObject.transform.GetChild(10).gameObject.SetActive(false);
			scoremen.gameObject.transform.GetChild(12).gameObject.SetActive(false);
		} else {
			scoremen.gameObject.transform.GetChild(10).gameObject.SetActive(true);
			scoremen.gameObject.transform.GetChild(12).gameObject.SetActive(true);
		}
		if (save.unlocked[4] == false && save.unlocked[3] == false && scoremen != null) {
			scoremen.gameObject.transform.GetChild(3).gameObject.SetActive(false);
		} else {
			scoremen.gameObject.transform.GetChild(3).gameObject.SetActive(true);
		}
		
	}
	
	// Update is called once per frame
	
	void Update () {
		/*GameObject titman = GameObject.Find("TitleScreenManager");
		TitleScreenManager scores = titman.GetComponent<TitleScreenManager>();
		save = scores.save;
		if (gameObject.CompareTag("score1") && save != null) {
			for(int i = 0; i < 10; i++) {
				int scor = save.scores0[i];
				string name = save.names0[i];
				string line = name +": "+ scor + " points" + System.Environment.NewLine;
				scoretext.text = scoretext.text + line;
			}
		} else if (gameObject.CompareTag("score2") && save != null) {
			int scor = save.scores1[0];
			string name = save.names1[0];
			scoretext.text = name +": " + scor + " points";
		} else if (gameObject.CompareTag("score3") && save != null) {
			int scor = save.scores2[0];
			string name = save.names2[0];
			scoretext.text = name +": " + scor + " points";
		} else if (gameObject.CompareTag("score4") && save != null) {
			int scor = save.scores3[0];
			string name = save.names3[0];
			scoretext.text = name +": " + scor + " points";
		} else if (gameObject.CompareTag("score5") && save != null) {
			int scor = save.scores4[0];
			string name = save.names4[0];
			scoretext.text = name +": " + scor + " points";
		} else {
			Debug.Log("save not found");
			scoretext.text = " : " + 0 + " points";
		}*/
		
	}
	
}
