using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TitleScreenManager : MonoBehaviour {

	public bool vr = false;
	public Save save;
	public bool[] unlocked = {true, false, true, false, false, false};
	
	// Use this for initialization
	public Transform controller;
   
    public static bool leftHanded { get; private set; }
 
    System.IO.StreamWriter recording;
	
	void Start () {
		GameObject play = GameObject.Find("Player");
		PlayerController playcont = play.GetComponent<PlayerController>();
		playcont.titlescreen = true;
		if (OVRManager.tiledMultiResSupported) {
			vr = true;
		}
		//vr = false;
		GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		if (vr == true) {
			// This is an Oculus Go
			GameObject men = GameObject.Find("Menu");
			men.transform.GetChild(0).gameObject.SetActive(false);
			GameObject va = GameObject.Find("VamanagerVR");
			vamanager van = va.GetComponent<vamanager>();
			GameObject pccam = GameObject.Find("Main Camera");
			pccam.GetComponent<Camera>().enabled = false;
			GameObject eventman = GameObject.Find("EventSystem");
			eventman.SetActive(false);
			settlist.vr = true;
			if (settlist.tryit == true) {
				//start menu back at tryit
			}
			van.startup();
		} else {
			// This is a phone
			GameObject men = GameObject.Find("MenuVR");
			men.transform.GetChild(0).gameObject.SetActive(false);
			GameObject va = GameObject.Find("Vamanager");
			vamanager van = va.GetComponent<vamanager>();
			GameObject vrcam = GameObject.Find("VR");
			vrcam.transform.GetChild(0).gameObject.SetActive(false);
			GameObject eventman = GameObject.Find("EventSystemVR");
			eventman.SetActive(false);
			GameObject pointe = GameObject.Find("OVRGazePointer");
			pointe.SetActive(false);
			settlist.vr = false;
			if (settlist.tryit == true) {
				//start menu back at tryit
				
			}
			van.startup();
		}
		//GameObject pccam = GameObject.Find("Main Camera");
		//pccam.GetComponent<Camera>().enabled = false;
		
		
		//create save file if not present
		if (!File.Exists(Application.persistentDataPath + "/gamesave.save")) {
			createSaveFile();
		}
		
		
		//load savefile
		loadsavefile();
		if (save != null) {
			for (int i = 0; i < unlocked.Length; i++) {
				unlocked[i] = save.unlocked[i];
			}
		}

	}
	
 
    void Awake() {
		if (vr == true) {
			#if UNITY_EDITOR
			leftHanded = false;        // (whichever you want to test here)
			#else
			leftHanded = OVRInput.GetControllerPositionTracked(OVRInput.Controller.LTouch);
			#endif
		}
    }
 
    void Update() {
		if (vr == true) {
			OVRInput.Controller c = leftHanded ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
			if (OVRInput.GetControllerPositionTracked(c)) {
				controller.localRotation = OVRInput.GetLocalControllerRotation(c);
				controller.localPosition = OVRInput.GetLocalControllerPosition(c);
			}
		}
    }  
		
	void createSaveFile() {
		save = new Save();
		string tojson = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savedata.json", tojson);
	}
	
	void loadsavefile() {
		if (File.Exists(Application.persistentDataPath + "/savedata.json")) {

			string json = File.ReadAllText(Application.persistentDataPath + "/savedata.json"); // loading all the text out of the file into a string, assuming the text is all JSON

			Save save2 = JsonUtility.FromJson<Save>(json);
			
			save = save2;
			

    
		} else {
			Debug.Log("No game saved!");
			createSaveFile();
		}

	}
			
}
