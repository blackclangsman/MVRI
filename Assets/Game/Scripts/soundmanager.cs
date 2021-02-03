using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the sound manager plays the sound clips in the game and its functions are called from other scripts.
//all functions in this script are set to play a sound

public class soundmanager : MonoBehaviour {

	AudioSource sound;
	float volu = 1.0f;
	int enginestate = -1;
	bool playonce = false;
	bool waiting = false;
	public int duration;
	bool machinwait = false;


	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//machine sounds go here
	}
	
	//engine idle
	public void caridle() {
		sound.volume = volu;
		if (enginestate != 0) {
			enginestate = 0;
			sound.clip = Resources.Load<AudioClip>("engineidle");
			sound.Play();
		}
	}
	
	//engine running
	public void cardriving() {
		sound.volume = volu;
		if (enginestate != 1) {
			enginestate = 1;
			sound.clip = Resources.Load<AudioClip>("engine5");
			sound.Play();
		}
	}
	
	//engine started
	public void startup() {
		AudioClip on = Resources.Load<AudioClip>("enginestart");
		//sound.PlayOneShot(on, 1.0f);
		sound.PlayOneShot(on, volu);
		caridle();
	}
	
	//engine shut off
	public void turnoff() {
		//sound.clip = Resources.Load<AudioClip>("engineoff");
		AudioClip off = Resources.Load<AudioClip>("engineoff");
		//sound.PlayOneShot(off, 1.0f);
		sound.PlayOneShot(off, volu);
		enginestate = -1;
		sound.Stop();
	}
	
	//countdown 321
	public void countdown() {
		AudioClip count = Resources.Load<AudioClip>("countdown");
		//sound.PlayOneShot(count, 1.0f);
		sound.PlayOneShot(count, volu);
	}
	
	//go on countdown
	public void countdowngo() {
		AudioClip count = Resources.Load<AudioClip>("countdownhipitch");
		//sound.PlayOneShot(count, 1.0f);
		sound.PlayOneShot(count, volu);
	}
	
	//coin
	public void coin() {
		AudioClip coin = Resources.Load<AudioClip>("coin");
		//sound.PlayOneShot(coin, 0.5f);
		sound.PlayOneShot(coin, volu/2);
	}
	
	
	//extra hit
	public void extrahit() {
		AudioClip extra = Resources.Load<AudioClip>("extra hit");
		//sound.PlayOneShot(extra, 0.5f);
		sound.PlayOneShot(extra, volu);
		AudioClip extra2 = Resources.Load<AudioClip>("extra");
		//sound.PlayOneShot(extra2, 1.0f);
		sound.PlayOneShot(extra2, volu);
	}
	
	//invincibility
	public void invincibleandpowerups() {
		AudioClip powr = Resources.Load<AudioClip>("extra");
		//sound.PlayOneShot(powr, 1.0f);
		sound.PlayOneShot(powr, volu);
	}
	
	
	//switch hit
	public void switchturn() {
		AudioClip swith = Resources.Load<AudioClip>("switch2");
		//sound.PlayOneShot(swith, 1.0f);
		sound.PlayOneShot(swith,volu);
		//StartCoroutine(waitsec());
		AudioClip door = Resources.Load<AudioClip>("door2");
		//sound.PlayOneShot(door, 1.0f);
		sound.PlayOneShot(door, volu);
	}
	

	//excessive movement
	public void excess() {
		if (playonce == false) {
			playonce = true;
			AudioClip excess = Resources.Load<AudioClip>("excess");
			sound.PlayOneShot(excess, 1.0f);
			//sound.PlayOneShot(excess, volu);
			StartCoroutine(waitsec());
		}
	}
	
	//switch to regular tank
	public void switchtank() {
		AudioClip engin = Resources.Load<AudioClip>("engineswitch");
		sound.PlayOneShot(engin, 1.0f);
		//sound.PlayOneShot(engin, volu);
	}
	
	//collide with wall
	public void wall() {
		AudioClip tankswitch = Resources.Load<AudioClip>("crash");
		//sound.PlayOneShot(tankswitch, 1.0f);
		sound.PlayOneShot(tankswitch, volu);
	}
	
	
	//go into wall or door
	public void wallcrash() {
		AudioClip tankswitch = Resources.Load<AudioClip>("wallcrash");
		//sound.PlayOneShot(tankswitch, 1.0f);
		sound.PlayOneShot(tankswitch, volu);
	}
	
	//complete a lap
	public void lap() {
		AudioClip lapclear = Resources.Load<AudioClip>("center reticle placement");
		//sound.PlayOneShot(lapclear, 1.0f);
		sound.PlayOneShot(lapclear, volu);
	}
	
	//clear race
	public void complete() {
		AudioClip comp = Resources.Load<AudioClip>("lapclear");
		//sound.PlayOneShot(comp, 1.0f);
		sound.PlayOneShot(comp, volu);
	}
	
	//crash with players
	public void crash() {
		AudioClip crash = Resources.Load<AudioClip>("crash2");
		//sound.PlayOneShot(crash, 1.0f);
		sound.PlayOneShot(crash, volu);
	}
	
	//cheering, happens if you finish first place
	public void cheering() {
		AudioClip cheer = Resources.Load<AudioClip>("clap");
		//sound.PlayOneShot(cheer, 1.0f);
		sound.PlayOneShot(cheer, volu);
	}
	
	public void aquaplaning() {
		if (waiting == false) {
			waiting = true;
			AudioClip wasser = Resources.Load<AudioClip>("waterse2");
			//sound.PlayOneShot(wasser, 1.0f);
			sound.PlayOneShot(wasser, volu);
			StartCoroutine(wait4sec());
		}
	}
	
	public void aquaplaningslow() {
		if (waiting == false) {
			waiting = true;
			AudioClip wasser = Resources.Load<AudioClip>("waterse1");
			//sound.PlayOneShot(wasser, 1.0f);
			sound.PlayOneShot(wasser, volu);
			StartCoroutine(wait4sec());
		}
	}
	
	
	//bonus points for lay still for a while
	public void star() {
		AudioClip coin = Resources.Load<AudioClip>("star");
		//sound.PlayOneShot(coin, 0.5f);
		sound.PlayOneShot(coin, volu/2);
	}
	
	
	//menu sounds
	
	public void selectbutton() {
		AudioClip picked = Resources.Load<AudioClip>("menu");
		//sound.PlayOneShot(picked, 1.0f);
		sound.PlayOneShot(picked, volu);
	}
	
	public void confirmbutton() {
		AudioClip conf = Resources.Load<AudioClip>("confirmbutton");
		//sound.PlayOneShot(conf, 1.0f);
		sound.PlayOneShot(conf, volu);
	}
	
	public void denied() {
		AudioClip den = Resources.Load<AudioClip>("pointatcenter");
		//sound.PlayOneShot(den, 1.0f);
		sound.PlayOneShot(den, volu);
	}
	
	public void volume(float vol) {
		if (vol == 0) {
			vol = 0.0f;
		} else {
			vol = vol/10;
		}
		AudioClip volum = Resources.Load<AudioClip>("engine2");
		sound.PlayOneShot(volum, vol);
		volu = vol;

	}
	
	//machinesounds
	public void machine(int duration) {
		StartCoroutine(machinetime(duration));
	}
	
	public void dwi() {
		AudioClip excess = Resources.Load<AudioClip>("dwi2");
		//sound.PlayOneShot(excess, 1.0f);
		sound.PlayOneShot(excess, volu);
	}
	
	public void mprage() {
		AudioClip excess = Resources.Load<AudioClip>("mprage2");
		//sound.PlayOneShot(excess, 1.0f);
		sound.PlayOneShot(excess, volu);
	}
	
	public void swi() {
		AudioClip excess = Resources.Load<AudioClip>("swi2");
		//sound.PlayOneShot(excess, 1.0f);
		sound.PlayOneShot(excess, volu);
	}
	
	public void t1tse() {
		AudioClip excess = Resources.Load<AudioClip>("t1tse2");
		//sound.PlayOneShot(excess, 1.0f);
		sound.PlayOneShot(excess, volu);
	}
	
	public void t2darkfluid() {
		AudioClip excess = Resources.Load<AudioClip>("t2darkfluid2");
		//sound.PlayOneShot(excess, 1.0f);
		sound.PlayOneShot(excess, volu);
	}
	
	public void t2tse() {
		AudioClip excess = Resources.Load<AudioClip>("t2tse2");
		//sound.PlayOneShot(excess, 1.0f);
		sound.PlayOneShot(excess, volu);
	}	
	
	//coroutines
	IEnumerator waitsec () {
		yield return new WaitForSeconds(1);
		playonce = false;
	}
	
	IEnumerator machinewait(int x) {
		yield return new WaitForSeconds(x);
		machinwait = false;
	}
	
	IEnumerator wait4sec () {
		yield return new WaitForSeconds(4);
		waiting = false;
	}
	
	IEnumerator machinetime(int duration) {
		yield return new WaitForSeconds(1);
		playonce = false;
	}
	
	
}
