using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour {

	public GameObject go;
	// Use this for initialization
	void Start () {
		go = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		
		transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
		
		if (Physics.Raycast(transform.position, transform.forward, out hit)) {
			if (hit.collider != null) {
				if (go != hit.collider.gameObject) {
					go.transform.SendMessage("OVRExit");
					go = hit.transform.gameObject;
					go.transform.SendMessage("OVREnter");
					Debug.Log("OVR entered");
				}
			}
		} else {
			if (go != null) {
				go.transform.SendMessage("OVRExit");
				Debug.Log("exited");
			}
		}
	}
}
