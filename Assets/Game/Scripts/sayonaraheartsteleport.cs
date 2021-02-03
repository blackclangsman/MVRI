using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is triggered upon collision with a blue box. It teleports the player back to a checkpoint positon

public class sayonaraheartsteleport : MonoBehaviour {

	public Transform checkpoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("teleported");
			other.gameObject.GetComponent<PlayerController>().son.crash();
			other.transform.position = checkpoint.position;
		}
	}
}
