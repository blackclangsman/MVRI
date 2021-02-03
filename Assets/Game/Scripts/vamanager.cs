using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the voice acting script is similar to the sound manager, in that its functions are called from other scripts when a voice clip needs to be played

public class vamanager : MonoBehaviour {

	public AudioSource va;
	float volu = 1.0f;

	// Use this for initialization
	void Start () {
		va = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//startup, added
	public void startup() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("startup");
		va.Play();
		
	}
	
	//added
	public void mainmenu() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("mainmenu");
		va.Play();
		
	}
	
	//added
	public void stageselect() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("stageselect");
		va.Play();
		
	}
	
	//added
	public void settingslist() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("settings");
		va.Play();
		
	}
	
	//added
	public void highscore() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("highscore");
		va.Play();
		
	}
	
	//added
	public void three() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("3");
		va.Play();
		
	}
	
	//added
	public void two() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("2");
		va.Play();
		
	}
	
	//added
	public void one() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("1");
		va.Play();
		
	}
	
	//added
	public void go() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("go");
		va.Play();
		
	}
	
	//added
	public void endrace() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("endrace");
		va.Play();
		
	}
	
	//added
	public void readytostart() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("readytostart");
		va.Play();
		
	}
	
	//added
	public void secret() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("secret");
		va.Play();
		
	}
	
	//added
	public void startrace() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("startrace");
		va.Play();
		
	}
	
	//added
	public void stickerselect() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("stickerselect");
		va.Play();
		
	}
	
	public void stopva() {
		va.Stop();		
	}
	
	//added
	public void tut1() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial1");
		va.Play();
		
	}
	
	//added
	public void tut2() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial2");
		va.Play();
		
	}
	
	//added
	public void tut3() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial3");
		va.Play();
		
	}
	
	//added
	public void tut4() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial4");
		va.Play();
		
	}
	
	//added
	public void tut5() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial5");
		va.Play();
		
	}
	
	//added
	public void tut6() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial6");
		va.Play();
		
	}
	
	//added
	public void tut7() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial7");
		va.Play();
		
	}
	
	//added
	public void tut8() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial8");
		va.Play();
		
	}
	
	//added
	public void tut9() {
		va.Stop();
		va.volume = volu;
		va.clip = Resources.Load<AudioClip>("tutorial9");
		va.Play();
		
	}
	
}
