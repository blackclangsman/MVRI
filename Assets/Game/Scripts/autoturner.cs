using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//note that since autoturning was scrapped, this script is effectively not used in the final game
public class autoturner : MonoBehaviour {

	float turnSpeed = 50.0f; //sets the players turning speed
	GameObject play; //the player
	float target; //the target the player turns towards
	float starting;
	bool flag360 = false; //flag for when reaching 360 degrees, once reached it must cound anything above 360 from 0 and anything below from 360
	public bool right; //are we turning left or right? if true, we turn right, if not then left

	// Use this for initialization
	void Start () {
		//right = true;
	}
	
	// Update is called once per frame
	void Update () {
		//player object is added into the play variable upon collision to prevent the from being turned at all times
		if (play != null) {
			if (right == true) {
				if (starting < target && flag360 == false) {
					play.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
					starting = play.transform.rotation.eulerAngles.y;
					Debug.Log("transforming" + starting);
				} else if (flag360 == true && target < starting) {
					float prevstart = play.transform.rotation.eulerAngles.y;
					play.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
					starting = play.transform.rotation.eulerAngles.y;
					if (prevstart > starting) {
						flag360 = false;
					}
				} else {
					play = null;
				}
			} else {
				if (starting > target && flag360 == false) {
					play.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
					starting = play.transform.rotation.eulerAngles.y;
					Debug.Log("transforming" + starting);
				} else if (flag360 == true && target > starting) {
					float prevstart = play.transform.rotation.eulerAngles.y;
					play.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
					starting = play.transform.rotation.eulerAngles.y;
					if (prevstart < starting) {
						flag360 = false;
					}
				} else {
					play = null;
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") { 
			Debug.Log("turning");
			play = other.gameObject;
			PlayerController playcont = play.GetComponent<PlayerController>();
			if (playcont.turning == true) {
				return;
			}
			starting = play.transform.rotation.eulerAngles.y;
			Debug.Log("eul: " + starting);
			if (right == true) {
				playcont.signalright();
				target = starting+45.0f;
				if (target >= 360) {
					target = target-360;
					flag360 = true;
				}
			} else {
				playcont.signalleft();
				target = starting-45.0f;
				if (target <= 0) {
					target = target+360;
					flag360 = true;
				}
				Debug.Log(right);
			}
			Debug.Log("tar: " + target);
			/*for (int i = 0; i < 9; i++) {
				other.transform.Rotate(0.0f, 5.0f, 0.0f, Space.Self);
			}*/
			
			//turning
			//if(Input.GetKey(KeyCode.LeftArrow))
				//transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
			//if(Input.GetKey(KeyCode.RightArrow))
				//transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
			
			
		}
		
	}
	
}
