using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	//upon collision with the switch, the associated door disappears
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			Debug.Log("Hit switch");
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			playcont.score += 300/playcont.duration;
			playcont.notice = "En blå dør har åbnet sig...";
			playcont.son.switchturn();
			GameObject doorswitch = this.transform.parent.gameObject;
			GameObject door = doorswitch.transform.GetChild(0).gameObject;
			//door.transform.Rotate(0, 90, 0);
			Destroy(door);
			
			Destroy(this.gameObject);
			
			
		}
		
	}
}
