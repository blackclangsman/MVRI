using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallsound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log("wal2");
		if (collision.gameObject.tag == "Player") {
			PlayerController playcont = collision.gameObject.GetComponent<PlayerController>();
			playcont.son.wallcrash();
			Debug.Log("walltime");
		}
	}
}
