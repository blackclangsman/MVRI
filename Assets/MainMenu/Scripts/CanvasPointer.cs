using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPointer : MonoBehaviour {
	
	public float defaultlength = 3.0f;
	
	public EventSystem events = null;
	public StandaloneInputModule inputModule = null;
	
	
	private LineRenderer line = null;
	
	public void Awake() {
		line = GetComponent<LineRenderer>();
	}
	
	public void Update() {
		UpdateLength();
	}
	
	public void UpdateLength() {
		line.SetPosition(0, transform.position);
		line.SetPosition(1, GetEnd());
	}
	
	private Vector3 GetEnd() {
		float dist = GetCanvasDist();
		Vector3 endPosition = calcEnd(defaultlength);
		
		if (dist != 0.0f) {
			endPosition = calcEnd(dist);
		}
		
		return endPosition;
	}
	
	private float GetCanvasDist() {
		
		//get data
		PointerEventData eventdat = new PointerEventData(events);
		eventdat.position = inputModule.inputOverride.mousePosition;
		
		//raycast using data
		List<RaycastResult> results = new List<RaycastResult>();
		events.RaycastAll(eventdat,results);
		
		//get closest
		RaycastResult closest = FindFirstRaycast(results);
		float dist = closest.distance;
		
		//clamp
		dist = Mathf.Clamp(dist, 0.0f, defaultlength);
		return dist;
	}
	
	private RaycastResult FindFirstRaycast(List<RaycastResult> results) {
		foreach(RaycastResult result in results) {
			if (!result.gameObject) {
				continue;
			}
			return result;
		}
		return new RaycastResult();
	}
	
	private Vector3 calcEnd(float length) {
		return transform.position + (transform.forward*length);
	}
 
}
