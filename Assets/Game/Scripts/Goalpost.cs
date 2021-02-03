using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goalpost : MonoBehaviour {
	
	public bool ended = false; //is this the final lap? if true, th game will end when the player crosses the finish line
	public bool tryit = false; //is the player in the tutorial or try it mode?

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (tryit == true) {
			ended = false;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		//upon collision with the finish line the effects will resolve on the player
		if (other.gameObject.tag == "Player") {
			Debug.Log("Goal reached");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			//increase the players lap count if theyre coming from the right direction
			if (playcont.directioncheck == true) {
				Debug.Log("yeh");
				playcont.laps = playcont.laps+=1;
				//string notice = "Ny runde!";
				string notice = "R: " + playcont.laps;
				playcont.notice = notice;
				playcont.directioncheck = false;
				playcont.finalwaypoint1 = false;
				playcont.finalwaypoint2 = false;
				//if it is the final lap end the race
				if (ended == true) {
					GameObject manage = GameObject.Find("GameManager");
					gamemaanager man = manage.GetComponent<gamemaanager>();
					man.ended = true;
					playcont.son.complete();
					if (playcont.placing == 1) {
						StartCoroutine(waitsec(playcont));
					}
				} else {
					playcont.son.lap();
				}
				//if it is the tutorial immediately take th eplayer back to the title screen
				if (tryit == true) {
					GameObject play = GameObject.Find("Player");
					tryit = false;
					playcont.tryit = false;
					GameObject setlist = GameObject.Find("SetingsList");
					SettingsList settlist = setlist.GetComponent<SettingsList>();
					settlist.tryit = false;
					settlist.level = 0;
					StartCoroutine(LoadYourAsyncScene("SampleScene"));
				}
			} else {
				playcont.laps = playcont.laps-=1;
			}				
			
			
			
		}
		//if the AI players cross, increase their lap count
		if (other.gameObject.tag == "AI1") {
			AIscript aicont = other.gameObject.GetComponent<AIscript>();
			aicont.laps = aicont.laps+1;
		}
		if (other.gameObject.tag == "AI2") {
			AIscript aicont = other.gameObject.GetComponent<AIscript>();
			aicont.laps = aicont.laps+1;
		}
		
	}
	
	//if the player arrived in first place, play a cheering sound effect
	IEnumerator waitsec (PlayerController playcont) {
		yield return new WaitForSeconds(1);
		playcont.son.cheering();
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
