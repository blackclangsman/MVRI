using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//upon colliding with a tutorial point, increment the variable keeping track of the reached tutorial by 1

public class tutscript : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("Tutorial entered");
			//Destroy(MedicalBox);
			PlayerController playcont = other.gameObject.GetComponent<PlayerController>();
			playcont.started = false;
			playcont.rb.velocity = Vector3.zero;
			GameObject tut = transform.parent.gameObject;
			tutorial tutori = tut.gameObject.GetComponent<tutorial>();
			tutori.curtut++;
			
		}
	}
}
