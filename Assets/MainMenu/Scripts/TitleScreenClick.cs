using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//script used for buttons in the menu, the function is called setText due to it originally only altering the text in the settings
//but was since expanded to include other button functionalities as well.

public class TitleScreenClick : MonoBehaviour {
	
	GameObject setlist;
	SettingsList settlist;
	GameObject stages;
	
	void Start() {
		setlist = GameObject.Find("SetingsList");
		settlist = setlist.GetComponent<SettingsList>();
		stages = GameObject.Find("StageSelect");
	}
	
	public void setText(string text) {
		Text txt = transform.Find("Text").GetComponent<Text>();
		Text yo;
		Save save;
		//if (String.Equals(txt.Text, "First Person")) {
		Debug.Log(txt.text);
		TitleScreenManager scores;
		GameObject titman;
		GameObject paren;
		switch(txt.text) {
			case "First Person":
				txt.text = "Third Person";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.cmera = 3;
				break;
			case "Third Person":
				txt.text = "First Person";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.cmera = 1;
				break;
			case "Overhead":
				txt.text = "First Person";
				//Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.cmera = 1;
				break;
			case "Midlertidig":
				txt.text = "Permanent";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.speedloss = true;
				break;
			case "Permanent":
				txt.text = "Midlertidig";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.speedloss = false;
				Debug.Log(settlist.AI);
				break;
			case "Til":
				txt.text = "Fra";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.AI = false;
				break;
			case "Fra": 
				txt.text = "Til";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.AI = true;
				Debug.Log(settlist.AI);
				break;
			case "VR Mode":
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vr = true;
				Debug.Log(settlist.AI);
				break;
			case "Flatscreen Mode":
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vr = false;
				Debug.Log(settlist.AI);
				break;
			case "Laer at spille":
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = -1;
				settlist.tryit = false;
				settlist.tutorial = true;
				StartCoroutine(LoadYourAsyncScene("tutorial"));
				break;
			case "Prov!":
				Debug.Log(txt.text);
				//loadscene
				GameObject play = GameObject.Find("Player");
				//play.transform.GetChild(0).gameObject.SetActive(true);
				GameObject goal2 = GameObject.Find("Goal");
				Goalpost goal = goal2.GetComponentInChildren<Goalpost>();
				goal.tryit = true;
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.tryit = true;
				settlist.level = -1;
				//SceneManager.LoadScene("scene2");
				//check if vr is on
				/*if (settlist.vr == true) {
				GameObject vrcam = GameObject.Find("VR");
				vrcam.transform.GetChild(0).gameObject.SetActive(false);
				GameObject canv = GameObject.Find("Menu");
				canv.transform.GetChild(0).gameObject.SetActive(false);
				} else {
				GameObject cam = GameObject.Find("Main Camera");
				Camera cammy = cam.GetComponent<Camera>();
				GameObject canv = GameObject.Find("Menu");
				canv.transform.GetChild(0).gameObject.SetActive(false);
				cammy.enabled = false;
				}
				PlayerController playcont = play.GetComponent<PlayerController>();
				playcont.tryit = true;
			
				playcont.camera2 = settlist.cmera;
				playcont.setstage();*/
				StartCoroutine(LoadYourAsyncScene("tryitlevel"));
				break;
			case "Spil!":
				titman = GameObject.Find("TitleScreenManager");
				scores = titman.GetComponent<TitleScreenManager>();
				save = scores.save;
				Debug.Log("unlocks are: " + scores.save.unlocked[1]);
				//GameObject stages = GameObject.Find("StageSelect");
				paren = transform.parent.parent.gameObject;
				//GameObject stages;
				if (scores.vr == true) {
					stages = paren.transform.GetChild(1).gameObject;
				} else {
					stages = paren.transform.GetChild(5).gameObject;
				}
				if (save != null) {
					if (save.unlocked[1] == true) {
						stages.transform.GetChild(1).gameObject.SetActive(true);
						stages.transform.GetChild(2).gameObject.SetActive(false);
					} else {
						stages.transform.GetChild(1).gameObject.SetActive(false);
						stages.transform.GetChild(2).gameObject.SetActive(true);
					}
					if (save.unlocked[3] == true) {
						stages.transform.GetChild(4).gameObject.SetActive(true);
						stages.transform.GetChild(5).gameObject.SetActive(false);
					} else {
						stages.transform.GetChild(4).gameObject.SetActive(false);
						stages.transform.GetChild(5).gameObject.SetActive(true);
					}
					//originally zelda level but scrapped
					/*if (save.unlocked[4] == true) {
					stages.transform.GetChild(8).gameObject.SetActive(true);
					stages.transform.GetChild(9).gameObject.SetActive(false);
					} else {
					stages.transform.GetChild(8).gameObject.SetActive(false);
					stages.transform.GetChild(9).gameObject.SetActive(true);
					}*/
					if (save.unlocked[5] == true) {
						Debug.Log(save.unlocked[5]);
						stages.transform.GetChild(6).gameObject.SetActive(true);
					} 
					int count = 0;
					for (int i = 0; i < 4; i++) {
						if (save.unitychan[i] == true) {
						count++;
						}	
					}
					if (count == 4) {
						//bring up cheats option
						stages.transform.GetChild(17).gameObject.SetActive(true);
					}
				}
				break;
			case "Forlaeng Scanningen":
				txt.text = "Afslut Scanningen";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.whenmovement = 2;
				break;
			case "Afslut Scanningen":
				txt.text = "Ingen Feedback";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.whenmovement = 3;
				break;
			case "Ingen Feedback":
				txt.text = "Forlaeng Scanningen";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.whenmovement = 1;
				break;
			case "Afslut":
				Debug.Log("yeena");
				Application.Quit();
				break;
			case "A Trip to the Wall":
				Debug.Log("level3");
				//loadscene
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 3;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("TitleLevel"));
				break;
			case "A Trip to the Seaside":
				Debug.Log("level4");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 4;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("Level1"));
				break;
			case "A Trip to the Rocky Mountains":
				Debug.Log("level2");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 2;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("BridgeLevelE"));
				break;
			case "A Trip to the Wild West":
				Debug.Log("level1");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 1;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("wildwestE"));
				break;
			case "EZ Wild West":
				Debug.Log("level1");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 1;
				settlist.name = "EZ";
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("EXwildwest"));
				break;
			case "The Wild West (True)":
				Debug.Log("level1");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 3;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("wildwest"));
				break;
			case "The Rocky Mountains (True)":
				Debug.Log("level1");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 4;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("BridgeLevel"));
				break;
			case "A Trip to the Test Site":
				Debug.Log("level6");
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 6;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("AlphaLevel"));
				break;
			case "A Trip to the Past":
				Debug.Log("level5");
				/*GameObject setlist = GameObject.Find("SetingsList");
				SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 5;
				settlist.tryit = false;
				StartCoroutine(LoadYourAsyncScene("ZeldaLevel"));*/
				break;
			case "Stickers":
				//stickers
			/*GameObject setlist = GameObject.Find("SetingsList");
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			settlist.level = 5;
			settlist.tryit = false;
			StartCoroutine(LoadYourAsyncScene("ZeldaLevel"));*/
			titman = GameObject.Find("TitleScreenManager");
			scores = titman.GetComponent<TitleScreenManager>();
			save = scores.save;
			//Debug.Log("unlocks are: " + scores.save.unlocked[1]);
			//GameObject stages = GameObject.Find("StageSelect");
			paren = transform.parent.parent.gameObject;
			GameObject stickers;
			stickers = paren.transform.GetChild(8).gameObject;
			for(int i = 0; i < 4; i++) {//levels
				for(int j = 0; j < 4; j++) {//stickers
					Debug.Log("stage: " + i + " sticker: " + j + " value: " + save.stickers[i,j]);
					if (save.stickers[i,j] == true) { //{{},{}}, outer caps are i, inner ones are j, levels = i, stickers = j
						//stickers.transform.GetChild(i+j).gameObject.SetActive(true);
						//stickers.transform.GetChild(i+j+16).gameObject.SetActive(false);
					} else {
						//stickers.transform.GetChild(i+j).gameObject.SetActive(false);
						//stickers.transform.GetChild(i+j+16).gameObject.SetActive(true);
					}
				}
			}
			for(int i = 0; i < 4; i++) {//levels
				if (save.stickers[0,i] == true) { //{{},{}}, outer caps are i, inner ones are j
					stickers.transform.GetChild(i).gameObject.SetActive(true);
					stickers.transform.GetChild(i+16).gameObject.SetActive(false);
				} else {
					stickers.transform.GetChild(i).gameObject.SetActive(false);
					stickers.transform.GetChild(i+16).gameObject.SetActive(true);
				}
			}
			for(int i = 0; i < 4; i++) {//levels
				if (save.stickers[1,i] == true) { //{{},{}}, outer caps are i, inner ones are j
					stickers.transform.GetChild(i+4).gameObject.SetActive(true);
					stickers.transform.GetChild(i+4+16).gameObject.SetActive(false);
				} else {
					stickers.transform.GetChild(i+4).gameObject.SetActive(false);
					stickers.transform.GetChild(i+4+16).gameObject.SetActive(true);
				}
			}
			for(int i = 0; i < 4; i++) {//levels
				if (save.stickers[2,i] == true) { //{{},{}}, outer caps are i, inner ones are j
					stickers.transform.GetChild(i+8).gameObject.SetActive(true);
					stickers.transform.GetChild(i+8+16).gameObject.SetActive(false);
				} else {
					stickers.transform.GetChild(i+8).gameObject.SetActive(false);
					stickers.transform.GetChild(i+8+16).gameObject.SetActive(true);
				}
			}
			for(int i = 0; i < 4; i++) {//levels
				if (save.stickers[3,i] == true) { //{{},{}}, outer caps are i, inner ones are j
					stickers.transform.GetChild(i+12).gameObject.SetActive(true);
					stickers.transform.GetChild(i+12+16).gameObject.SetActive(false);
				} else {
					stickers.transform.GetChild(i+12).gameObject.SetActive(false);
					stickers.transform.GetChild(i+12+16).gameObject.SetActive(true);
				}
			}
			break;
			case "Sportsbil":
				txt.text = "Postbil";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vehicle = 2;
				settlist.ai1 = 1;
				settlist.ai2 = 3;
				//GameObject stages = GameObject.Find("StageSelect");
				stages.transform.GetChild(15).gameObject.SetActive(true);
				stages.transform.GetChild(14).gameObject.SetActive(false);
				//also change image
				break;
				
				
				
				
			case "Postbil":
				txt.text = "Lastbil";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vehicle = 3;
				settlist.ai1 = 2;
				settlist.ai2 = 1;
				//GameObject stages = GameObject.Find("StageSelect");
				stages.transform.GetChild(16).gameObject.SetActive(true);
				stages.transform.GetChild(15).gameObject.SetActive(false);
				break;
				
			case "Lastbil":
				txt.text = "Politibil";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vehicle = 4;
				settlist.ai1 = 3;
				settlist.ai2 = 2;
				//GameObject stages = GameObject.Find("StageSelect");
				stages.transform.GetChild(19).gameObject.SetActive(true);
				stages.transform.GetChild(16).gameObject.SetActive(false);
				break;
			case "Politibil":
				txt.text = "Pink Bil";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vehicle = 5;
				settlist.ai1 = 2;
				settlist.ai2 = 3;
				//GameObject stages = GameObject.Find("StageSelect");
				stages.transform.GetChild(20).gameObject.SetActive(true);
				stages.transform.GetChild(19).gameObject.SetActive(false);
				//also change image
				break;
			case "Pink Bil":
				txt.text = "Sportsbil";
				Debug.Log(txt.text);
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.vehicle = 1;
				settlist.ai1 = 2;
				settlist.ai2 = 3;
				
				stages.transform.GetChild(14).gameObject.SetActive(true);
				stages.transform.GetChild(20).gameObject.SetActive(false);
				//also change image
				break;
				
			case "Unlock All":
				titman = GameObject.Find("TitleScreenManager");
				scores = titman.GetComponent<TitleScreenManager>();
				for(int i = 0; i < 4; i++) {
					scores.save.unlocked[i] = true;
				}
				Debug.Log(scores.save.unlocked[1]);
				break;
				
			case "Maskine Lyd Til":
				txt.text = "Maskine Lyd Fra";
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.machsound = false;
				break;
				
			case "Maskine Lyd Fra":
				txt.text = "Maskine Lyd Til";
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.machsound = true;
				break;
			case "Justin Bailey":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "Justin Bailey";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "Justin Bailey";
				break;
			case "ABACABB":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "ABACABB";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "ABACABB";
				break;
			case "UUDDLRLRBAS":
				txt.text = "Draejning Til";
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "UUDDLRLRBAS";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "UUDDLRLRBAS";
				break;
			case "Apache":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "Apache";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "Apache";
				break;
			case "Manuel Draejning":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "Turn";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "Manuel Draejning";
				break;
				
			case "Zelda":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "Zelda";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "Zelda";
				break;
				
			case "DebugLord":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "DebugLord";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "DebugLord";
				break;
				
			case "Ingen":
				//GameObject setlist = GameObject.Find("SetingsList");
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.name = "";
				yo = transform.parent.gameObject.GetComponent<Text>();
				yo.text = "Ingen";
				break;
			
		}
		
		Debug.Log(txt.text);
		Debug.Log(text);
		
	}
	
	//load scenes asynchronously
	IEnumerator LoadYourAsyncScene(string scenename) {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Single);
		//SceneManager.LoadScene(scenename, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
	
}
