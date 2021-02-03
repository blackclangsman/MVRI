using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gamemaanager : MonoBehaviour {

	//default settings
	public int duration = 1; //duration of the race
	int extraduration = 0; //added extra duration if movement is detected
	public int volume = 5; //game volume
	public bool AI = true; //is the AI on?
	public bool vr = true; //is the game being played in VR?
	public int sensitivity = 5; //motion field sensitivity
	public bool debugging = true; //debug mode
	public int level = 0; //level played
	public float movement = 0.0f; //note: highest amount of movement registered, counts towards points
	bool check = true; //checkmarks for when to trigger enumerator functions
	bool check2 = true;
	int whenmovement = 1; //what happens when movement is detected?
	public bool machsound; //are machone sounds on?

	//variables for use
	public bool ended = false; //has the race ended?
	int totalscore = 0; //final score once the race is over
	bool moved = false; //has movement been registered?
	bool timeflag = false; //flasg for enumerator function
	bool bonus = false; //is it time for the player to receive a bonus? works as a flag in an enumerator function
	int bonuspoints = 0; //bonus points awarded
	int multiplier = 0; //multiplier for the bonus points
	int maxval = 100; //time it will take in seconds before a bonus is awarded
	public bool tutorial = false; //is this the tutorial stage? if so hand control to the tutorial manager
	Transform bonusbar; //used to alter size of the bonus bar
	public bool[] stickersunlocked = {false, false, false, false}; //available stickers at the end, those that are true are selectable
	
	
	//gyroscope, ultimately not used
	private Gyroscope gyro;
	private Quaternion rotation;
	private bool gyroActive;
	private Quaternion startrotation;
	
	//accelerometer, ultimately not used
	private Vector3 startposition;
	private Vector3 currentposition;
	private Vector3 currentacceleration;
	private Vector3 initialacceleration;
	
	//vr
	public GameObject centerEye;
	public float x = 16.0f;//0.2f;//size of motion field (x coordinate)
	public float y = 3.0f;//1.0f;//size of motion field (y coordiante)
	bool button1 = true;
	bool button2 = true;
	
	//pointcloud
	
	
	
	
	//for debugging, detects movement with mouse movements
	Vector3 lastMouseCoordinate;
	
	GameObject player;
	PlayerController playcont;
	
	
	// Use this for initialization
	void Start () {
		//find player and settinglist
		GameObject setlist = GameObject.Find("SetingsList");
		player = GameObject.Find("Player");
		playcont = player.GetComponent<PlayerController>();
		if (playcont.titlescreen == true) {
			return;
		}
		//initialize the bonusbar
		bonusbar = player.transform.GetChild(12).GetChild(0).GetChild(1).GetChild(1);
		soundmanager son = player.transform.GetChild(7).gameObject.GetComponent<soundmanager>();
		
		if (OVRManager.tiledMultiResSupported) {
			//vr = true;
		}
		vr = true;
		//vr = false;
		debugging = false;
		//apply the settings found in the settlings list
		try {
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			//GameObject player = GameObject.Find("Player");
			//PlayerController playcont = player.GetComponent<PlayerController>();
			//get settings list from menu
			if (settlist != null) {
				duration = settlist.duration;
				volume = settlist.volume;
				AI = settlist.AI;
				vr = settlist.vr;
				level = settlist.stage;
				sensitivity = settlist.sensitivity;
				whenmovement = settlist.whenmovement;
				level = settlist.level;
				GameObject goal2 = GameObject.Find("Goal");
				Goalpost goal = goal2.GetComponentInChildren<Goalpost>();
				goal.tryit = settlist.tryit;
				son.duration = settlist.duration;
				machsound = settlist.machsound;
				tutorial = settlist.tutorial;
				if (level != 0 || level != -1) {
					GameObject ai1 = GameObject.Find("AI1");
					AIscript aicont1 = ai1.GetComponent<AIscript>();
					GameObject ai2 = GameObject.Find("AI2");
					AIscript aicont2 = ai2.GetComponent<AIscript>();
					int sped = settlist.speed;
					int gamesped = sped*10;
					/*int gamesped = 20;
					for (int i = 0; i < sped+1; i++) {
						gamesped += 10;
					}*/
					aicont1.vehicle = settlist.ai1;
					aicont2.vehicle = settlist.ai2;
					aicont1.moveSpeed = gamesped-5;
					aicont2.moveSpeed = gamesped-10;
					playcont.titlescreen = false;
				}
				//playcont.setCamera();
				/*if (level != 0) {
					GameObject stag = GameObject.Find("Stagemanager");
					StageManager stagcont = stag.GetComponent<StageManager>();
					stagcont.startspeed = gamesped;
				}*/
				if (tutorial == true) { //tutorial
					playcont.startspeed = 30;
					duration = 0;
				}
				
				
			}
		//use default settings if settingslist is not found
		} catch (Exception e) {
			playcont.startspeed = 30;
			Debug.Log("Settings list not found");
			duration = 1;
			if (tutorial == true) {
				duration = 0;
			}
			volume = 5;
			AI = true;
			//comment if statement below out once done testing ai
			level = 1;
			son.duration = 1;
			if (level != 0 && tutorial == false) {
					GameObject ai1 = GameObject.Find("AI1");
					AIscript aicont1 = ai1.GetComponent<AIscript>();
					GameObject ai2 = GameObject.Find("AI2");
					AIscript aicont2 = ai2.GetComponent<AIscript>();
					aicont1.moveSpeed = 25;
					aicont2.moveSpeed = 20;
					playcont.aicont1 = aicont1;
					playcont.aicont2 = aicont2;
			}
			machsound = false;
		}
		
		
		lastMouseCoordinate = Input.mousePosition;
		//level = 1;
		if (level != 0) {
			playcont.titlescreen = false;
			//playcont.setstage();
			playcont.notice = " ";
			//StartCoroutine(waitsec2());
			playcont.notice = " ";
			//StartCoroutine(waitsec2());
			playcont.notice = " ";
			//StartCoroutine(waitsec2());
			if (vr == true) {
				playcont.notice = "Kig på den blå kasse";
			}
		}
		
		
		if (level == -1) { //tutorial
			playcont.startspeed = 30;
		}
		
		//set the size of the motion field based on sensitivity
		setfieldsize();
		adjustsize();
		maxval = (int)UnityEngine.Random.Range(10.0f, 30.0f);
	}
	
	
	void setfieldsize() {
		//x and y values gotten from measurements
		//note: og measured values: 1mm = y: 0.25, x: 1, mm = y: 2.5, x: 10
		

		//new measured values (forehead): 1mm = y: 0.5, x: 2, 10mm = y: 5, x: 20

		//new new measured values (forehead with melanies formulas): 1mm = y: 1.3, x: 5.3, 5mm = y: 7.1, x: 26.5
		x = 0.0f;
		y = 0.0f;
		for (int i = 0; i < sensitivity*5; i++) {
			x = x+1f;
			y = y+0.25f;
		}
		Debug.Log("x is: " + x + ", y is: " + y);
		
	}
	
	
	void Awake() {
		
	}
	
	
	// Update is called once per frame
	void Update() {
		
	}
	
	
	void FixedUpdate () {
		//if at title screen do nothing
		if (playcont.titlescreen == true) {
			
		} else {
			
			//if started start the duration timer
		if (playcont.started == true && check == true) {
			check = false;
			StartCoroutine(durationtime(playcont));
		}
		
		//if ended and the player crosses the finish line end the game
		if (ended == true && check2 == true) {
			//end race, calulate score
			Debug.Log("race ended");
			check2 = false;
			try {
				GameObject ai1 = GameObject.Find("AI2");
				AIscript cont1 = ai1.GetComponent<AIscript>();
				cont1.ended = true;
				GameObject ai2 = GameObject.Find("AI2");
				AIscript cont2 = ai2.GetComponent<AIscript>();
				cont2.ended = true;
			} catch (Exception e) {
				Debug.Log("No AI players");
			}
			endgame();
		}
		
		//check if the player has moved or earned a bonus yet
		CheckMovement();
		checkbonus();
		}
	}
	
	//keep track of the duration, if machine sounds are on a new one will play every minute
	IEnumerator durationtime(PlayerController playcont) {
		//do machinesound implementation here
		if (machsound == true) {
			int x = (duration*60)/6;
			//yield return new WaitForSeconds(60*duration);
			for (int i = 0; i <= x; i = i+2) {
				playcont.son.t1tse();
				yield return new WaitForSeconds(2);
			}
			for (int i = 0; i <= x; i = i+2) {
				playcont.son.t2tse();
				yield return new WaitForSeconds(2);
			}
			for (int i = 0; i <= x; i = i+2) {
				playcont.son.t2darkfluid();
				yield return new WaitForSeconds(2);
			}
			for (int i = 0; i <= x; i = i+2) {
				playcont.son.t1tse();
				yield return new WaitForSeconds(2);
			}
			for (int i = 0; i <= x; i = i+2) {
				playcont.son.t1tse();
				yield return new WaitForSeconds(2);
			}
			for (int i = 0; i <= x; i = i+2) {
				playcont.son.t2darkfluid();
				yield return new WaitForSeconds(2);
			}
/*t1_tirm_sag_dark-fluid_p2 (t1tse())
t2_tse_tra_512(TE 115 ms or TE 153ms) (t2tse())
t2_tirm_cor_dark-fluid_4mm  (t2darkfluid())
t1_mpr_3d_cor_p2_iso_Siemens (t1tse())
Diff_resolve_trace_tra_192_p2_diff_P
t1_tir_tra (t1tse())
t1_tirm_tra_dark-fluid_p2 (t2darkfluid())
t2_swi3d_tra_p2_2mm_4 sæt billeder (t2tse())*/
		
		
		//if the player moves andthe settings are set to extend the scan, the duration will be extended here
			if (extraduration > 0) {
				//yield return new WaitForSeconds(60);
				playcont.son.t2tse();
				yield return new WaitForSeconds(20);
				playcont.son.t2tse();
				yield return new WaitForSeconds(20);
				playcont.son.t2tse();
				yield return new WaitForSeconds(20);
				extraduration--;
			}
		//if machine sounds arent on, just start the timer normally
		} else {
			yield return new WaitForSeconds(60*duration);
			if (extraduration > 0) {
				yield return new WaitForSeconds(60);
				extraduration--;
			}
		}
        //ended = true;
		//once the duration is up, set the goal posts ended to true so that when the player drives through the game ends
		GameObject goal2 = GameObject.Find("Goal");
		Goalpost goal = goal2.GetComponentInChildren<Goalpost>();
		goal.ended = true;
		Debug.Log("time over");
		GameObject gplay = GameObject.Find("Player");
		PlayerController end = gplay.GetComponentInChildren<PlayerController>();
		end.notice = "Skifter til reservetank";
		end.son.switchtank();
	}
	
	//checks if movement has been registered by the raycast functions
	void CheckMovement() {
		//check if patient has moved their head
		GameObject gplay = GameObject.Find("Player");
		PlayerController end = gplay.GetComponentInChildren<PlayerController>();
		if (vr == true) {
			//select gyro or accelerometer
			//moved = gyrosco();
			//moved = accelerationcheck();
		} else {
			//use pointcloud
			if (debugging == true) {
				//use mouse
				moved = mouse();
			} else {
				//use pointcloud
				moved = pointCloud();
			}
		}
		if (moved == true && end.started == true && end.ended != true) {
			
			//register the movement and make the player lose their bonus
			moved = false;
			movement++;
			bonuspoints = 0;
			multiplier = 0;
			end.score = end.score-100/duration;
			

			//get tp canvas
			//set cross and revert to default color of star, the player loses their bonus
			GameObject cav = gplay.transform.GetChild(18).gameObject;
			cav.transform.GetChild(2).gameObject.SetActive(false);
			cav.transform.GetChild(3).gameObject.SetActive(false);
			cav.transform.GetChild(4).gameObject.SetActive(false);
			cav.transform.GetChild(5).gameObject.SetActive(false);
			cav.transform.GetChild(6).gameObject.SetActive(false);
			bonusbar.localScale = new Vector3(0.0f,0.2f,1);
			GameObject barcolor = bonusbar.transform.GetChild(0).gameObject;
			SpriteRenderer sprit = barcolor.GetComponent<SpriteRenderer>();
			sprit.color = new Color(0.06666667f, 0.7647059f, 0.09803922f, 1.0f); //set back to green
			//Image pic = img.GetComponent<Image>();
			//set image back to neutral
			
			//flash red cross
			StartCoroutine(flash(cav));
				
			//end.notice = "movement detected";
			//check what the settings are set to, game over, penalize or nothing
			if (whenmovement == 1) { //extend scan
				Debug.Log("movement detected");
				if (timeflag == false) {
					timeflag = true;
					end.notice = "For meget bevaegelse";
					StartCoroutine(waitsec(end));
					penalize();
				}
				Debug.Log("movement detected");
			} else if (whenmovement == 2) { //gameover
				if (timeflag == false) {
					timeflag = true;
					end.notice = "For meget bevaegelse";
					StartCoroutine(waitsec(end));
					gameover();
				}
				Debug.Log("movement detected");
			} else { //practice & debugging
				//movement detected
			}
				
		}
		
	}
	
	//checks if the player has earned a bonus
	void checkbonus() {
		if (bonus == false && playcont.started == true && playcont.ended != true) {
			bonus = true;
			//bonus timer counts up until it reaches maxvals value
			bonuspoints++;
			//the bonus bars size is altered to make it "fill up"
			float bonusvisual = 14.0f/maxval;
			bonusbar.localScale = new Vector3(bonusvisual*bonuspoints,0.2f,1);
			GameObject cav = player.transform.GetChild(18).gameObject;
			StartCoroutine(waitsec2(cav));
			//once the bonus counter is equal to maxval, the player receives their bonus
			if (bonuspoints == maxval) {
				int[] bonusval = {30, 35, 40, 45, 50, 60, 60};
				//Random random = new Random(); 
				//maxval = (int)UnityEngine.Random.Range(30.0f, 60.0f);
				//and the next maxval is set as the new tier
				maxval = bonusval[multiplier];
				GameObject barcolor = bonusbar.transform.GetChild(0).gameObject;
				SpriteRenderer sprit = barcolor.GetComponent<SpriteRenderer>();
				//multiplier is incremented as well but cannot go above 6, bonus points are then awarded based on multiplier
				multiplier++;
				if (multiplier < 6) {
					//playcont.score += bonuspoints*multiplier;
					playcont.score += (int)playcont.moveSpeed*multiplier+(bonuspoints/duration);
				} else {
					//playcont.score += bonuspoints*5;
					playcont.score += (int)playcont.moveSpeed*5+(bonuspoints/duration);
					multiplier = 5;
				}
				playcont.notice = "Bonus: " + bonuspoints;
				bonuspoints = 0;
				//change to cheering
				playcont.son.star();
				//change graphic here
				//set image
				
				//show the star as visual feedback
				Debug.Log("mult is" + multiplier);
				if (multiplier == 1) {
					cav.transform.GetChild(2).gameObject.SetActive(true);
					sprit.color = new Color(0.7169812f, 0.4081853f, 0.3145247f, 1.0f);//change color
				} else if (multiplier == 2) {
					cav.transform.GetChild(2).gameObject.SetActive(false);
					cav.transform.GetChild(3).gameObject.SetActive(true);
					sprit.color = new Color(0.8584906f, 0.5354025f, 0.2146226f, 1.0f);//change color
				} else if (multiplier == 3) {
					cav.transform.GetChild(3).gameObject.SetActive(false);
					cav.transform.GetChild(4).gameObject.SetActive(true);
					sprit.color = new Color(0.6981132f, 0.6981132f, 0.6981132f, 1.0f);//change color
				} else if (multiplier == 4) {
					cav.transform.GetChild(4).gameObject.SetActive(false);
					cav.transform.GetChild(5).gameObject.SetActive(true);
					sprit.color = new Color(0.7075472f, 0.6187537f, 0.1768868f, 1.0f);//change color
				} else if (multiplier == 5) {
					cav.transform.GetChild(5).gameObject.SetActive(false);
					cav.transform.GetChild(6).gameObject.SetActive(true);
				}
;			}
		}
	}
	
	
	
	//adjusts size of the motion zone based on the x and y values calculated for it
	public void adjustsize() {
		GameObject play = GameObject.Find("Player");
		PlayerController playcont = play.GetComponent<PlayerController>();
		//tp hud
		//divided by two since the x and y values are the range, and the face starts in the middle of the range
		GameObject field = play.transform.GetChild(12).GetChild(0).gameObject;
		field.GetComponent<RectTransform>().sizeDelta = new Vector2 (x/2, y/2);
		//fp hud
		GameObject field2 = play.transform.GetChild(13).GetChild(0).gameObject;
		field2.GetComponent<RectTransform>().sizeDelta = new Vector2 (x/2, y/2);
		//playcont.notice = "size: x: " + x + ", y: " + y;
	}
	
	
	//function for when the player moves the gaze out of the field
	public void testRaycast() {
		GameObject pa = GameObject.Find("Player");
		PlayerController play = pa.GetComponentInChildren<PlayerController>();
		play.pointerincircle = false;
		//play.rb.velocity = Vector3.zero;
		vamanager van = pa.transform.GetChild(7).GetChild(1).gameObject.GetComponent<vamanager>();
		if (debugging == true) {
			play.notice = "Point reticle at the field";
			if (play.ended == false) {	
				van.startrace();
			}
		}
		//register movement if the game has started
		if (play.started == true && play.pause != true) {
			moved = true;
		//if not tell the player to point the reticle at the field
		} else {
			play.readytostart = false;
			play.son.turnoff();
			GameObject go = GameObject.Find("GoAndFinish");
			Text desc = go.transform.GetChild(0).GetChild(4).gameObject.GetComponent<Text>();
			if (vr == true) {
				desc.text = "";
			} else {
				desc.text = "";
			}
		} 
	}
	
	//clicking button, for debugging only
	public void testRaycast2() {
		GameObject pa = GameObject.Find("Player");
		PlayerController play = pa.GetComponentInChildren<PlayerController>();
		vamanager van = pa.transform.GetChild(7).GetChild(1).gameObject.GetComponent<vamanager>();
		van.va.Stop();
		if (debugging == true) {
			play.notice = "clicked button";
		}
		if (play.tryit == true) {
			StartCoroutine(LoadYourAsyncScene("SampleScene"));
		}
		if (tutorial == true) {
			//play.started = true;
		}
	}
	
	//function for when player points the gaze pointer at the motion zone
	public void testRaycast3() {
		GameObject pa = GameObject.Find("Player");
		PlayerController play = pa.GetComponentInChildren<PlayerController>();
		play.pointerincircle = true;
		vamanager van = pa.transform.GetChild(7).GetChild(1).gameObject.GetComponent<vamanager>();
		//if the player hasnt started start engine
		if (play.started != true) {
			if (debugging == true) {
				play.notice = "Ready to start";
			}
			//if its not the tutorial, play the ready to start va clip
			if (play.ended == false && tutorial == false) {
				van.readytostart();
			}
			play.son.startup();
			GameObject go = GameObject.Find("GoAndFinish");
			Text desc = go.transform.GetChild(0).GetChild(4).gameObject.GetComponent<Text>();
			if (vr == true) {
				desc.text = "";
			} else {
				desc.text = "";
			}
		} else {
			//butoon2 = true;
			if (play.pause != true) {
				moved = true;
			}
		}
		play.readytostart = true;
	}
	/*
	public void test2Raycast() {
		GameObject pa = GameObject.Find("Player");
		PlayerController play = pa.GetComponentInChildren<PlayerController>();
		play.notice = "exited button2";
	}
	
	public void test2Raycast2() {
		GameObject pa = GameObject.Find("Player");
		PlayerController play = pa.GetComponentInChildren<PlayerController>();
		play.notice = "clicked button2";
	}
	
	public void test2Raycast3() {
		GameObject pa = GameObject.Find("Player");
		PlayerController play = pa.GetComponentInChildren<PlayerController>();
		play.notice = "entered button2";
	}**/
	
	
	//mouse motion detection, only used for debugging
	bool mouse() {
		bool moved = false;
		Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
		if(mouseDelta.x < 0 || mouseDelta.x > 0 || mouseDelta.y < 0 || mouseDelta.y > 0) {
			moved = true;
		}
		lastMouseCoordinate = Input.mousePosition;
		return moved;
	}
	
	bool pointCloud() {
		return false;
	}
	
	
	/*void alternateMovecheck() {
		transform.eulerAngles = new Vector3(centerEye.transform.localEulerAngles.x,centerEye.transform.localEulerAngles.y, centerEye.transform.localEulerAngles.z);
	}*/
	
	
	//game over function, ends the game upon registering movement
	void gameover() {
		GameObject player = GameObject.Find("Player");
		PlayerController playcont = player.GetComponent<PlayerController>();
		//playcont.notice = "Movement detected";
		playcont.ended = true;
		playcont.son.excess();
		playcont.rb.velocity = Vector3.zero;
		playcont.rb.angularVelocity = Vector3.zero;
		//set gameoversceen to active
	}
	
	
	//penalize function, lowers the players speed
	void penalize() {
		//extend scan and penalize speed
		GameObject player = GameObject.Find("Player");
		PlayerController playcont = player.GetComponent<PlayerController>();
		//playcont.notice = "Movement detected";
		playcont.son.excess();
		playcont.moveSpeed = playcont.moveSpeed -= 10;
		playcont.savedspeed = playcont.savedspeed -= 10;
		playcont.startspeed = playcont.startspeed -= 10;
		playcont.duration = playcont.duration++;
		duration++;
		extraduration++;
		//stops the player when movement is registered
		playcont.rb.velocity = Vector3.zero;
		playcont.rb.angularVelocity = Vector3.zero;
	}
	
	
	
	//ends the game
	void endgame() {
		//calls up the results screen
		GameObject go = GameObject.Find("GoAndFinish");
		go.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
		GameObject end = go.transform.GetChild(0).GetChild(6).gameObject;
		gamendUI endUI = end.GetComponent<gamendUI>();
		GameObject player = GameObject.Find("Player");
		PlayerController playcont = player.GetComponent<PlayerController>();
		playcont.van.stickerselect();
		//set game to ended on playercontroller
		playcont.ended = true;
		playcont.started = false;
		playcont.notice = "Turen er ovre";
		//calculate score and show the game is over on ui
		totalscore = endUI.showendscreen();
		//load savedata
		LoadGame();
		//load sticker menu
		GameObject stickermenu = player.transform.GetChild(12).GetChild(1).gameObject;
		if (level != 0) {
			stickermenu.SetActive(true);
		} else {
			playcont.stickerpicked = true;
		}
		//determine which stickers are available
		stickersunlocked[0] = true; //complete stage
		//change sprite depending on level
		if (playcont.placing < 2) { //finish first place
			stickersunlocked[1] = true;
			stickermenu.transform.GetChild(1).gameObject.SetActive(true);
			stickermenu.transform.GetChild(2).gameObject.SetActive(false);
		} else {
			stickermenu.transform.GetChild(2).gameObject.SetActive(true);
			stickermenu.transform.GetChild(1).gameObject.SetActive(false);
		}
		if (playcont.coins > 29) { //collect 30 coins
			stickersunlocked[2] = true;
			stickermenu.transform.GetChild(3).gameObject.SetActive(true);
			stickermenu.transform.GetChild(4).gameObject.SetActive(false);
		} else {
			stickermenu.transform.GetChild(4).gameObject.SetActive(true);
			stickermenu.transform.GetChild(3).gameObject.SetActive(false);
		}
		if (movement < 1.0f) { //make no excess movements
			stickersunlocked[3] = true;
			stickermenu.transform.GetChild(5).gameObject.SetActive(true);
			stickermenu.transform.GetChild(6).gameObject.SetActive(false);
			//change sprite depending on level
		} else {
			stickermenu.transform.GetChild(6).gameObject.SetActive(true);
			stickermenu.transform.GetChild(5).gameObject.SetActive(false);
		}
				//stickerpicked = true;
				
				
		
		
		//have player press button to go back to title screen in playercontroller
		
	}
	
	
	//handles loading and saving the game
	public void LoadGame() { 
		// 1
		GameObject player = GameObject.Find("Player");
		PlayerController playcont = player.GetComponent<PlayerController>();
		//check if save file exists, if not it creates it
		if (File.Exists(Application.persistentDataPath + "/gamesave.json")) {
			// 2
			string json = File.ReadAllText(Application.persistentDataPath + "/savedata.json"); // loading all the text out of the file into a string, assuming the text is all JSON

			Save save = JsonUtility.FromJson<Save>(json);

			Debug.Log("Game Loaded");

			//stores the high scores for the level, first it checks what level was played
			if (level == 1) { //wild west & ez wild west
				//vari = save.scores0;
				//go through and compare if the score was higher than each score in the highscorelist
				for (int i = 0; i < save.scores0.Length; i++) {
					if (save.scores0[i] < totalscore) {
						//if a score was beat shift all scores one index to the right, ensuring the lowest score on the previous list disapears
						for (int j = save.scores0.Length; i < 0; i--) {
							if (j == i) {
								save.scores0[i] = totalscore;
								save.names0[i] = playcont.name;
								break;
							} else {
								if (j == 0) {
									save.scores0[j] = totalscore;
									save.names0[j] = playcont.name;
									break;
								}
								save.scores0[j-1] = save.scores0[j];
								save.names0[j-1] = save.names0[j];
							}
						}
					}
				}
				//check if the secret passage in the stage was found
				if (playcont.unitychan == true) {
					save.unitychan[0] = true;
				}
				//rpeat process for the other levels
			} else if (level == 2) { //mountains
				for (int i = 0; i < save.scores1.Length; i++) {
					if (save.scores1[i] < totalscore) {
						for (int j = save.scores1.Length; i < 0; i--) {
							if (j == i) {
								save.scores1[i] = totalscore;
								save.names1[i] = playcont.name;
								break;
							} else {
								if (j == 0) {
									save.scores1[j] = totalscore;
									save.names1[j] = playcont.name;
									break;
								}
								save.scores1[j-1] = save.scores1[j];
								save.names1[j-1] = save.names1[j];
							}
						}
					}
				}
				if (playcont.unitychan == true) {
					save.unitychan[1] = true;
				}
			} else if (level == 3) { //wild west hard
				for (int i = 0; i < save.scores2.Length; i++) {
					if (save.scores2[i]< totalscore) {
						for (int j = save.scores2.Length; i < 0; i--) {
							if (j == i) {
								save.scores2[i] = totalscore;
								save.names2[i] = playcont.name;
								break;
							} else {
								if (j == 0) {
									save.scores2[j] = totalscore;
									save.names2[j] = playcont.name;
									break;
								}
								save.scores2[j-1] = save.scores2[j];
								save.names2[j-1] = save.names2[j];
							}
						}
					}
				}
				if (playcont.unitychan == true) {
					save.unitychan[2] = true;
				}
			} else if (level == 4) { //mountains hard
				for (int i = 0; i < save.scores3.Length; i++) {
					if (save.scores3[i] < totalscore) {
						for (int j = save.scores3.Length; i < 0; i--) {
							if (j == i) {
								save.scores3[i] = totalscore;
								save.names3[i] = playcont.name;
								break;
							} else {
								if (j == 0) {
									save.scores3[j] = totalscore;
									save.names3[j] = playcont.name;
									break;
								}
								save.scores3[j-1] = save.scores3[j];
								save.names3[j-1] = save.names3[j];
							}
						}
					}
				}
				if (playcont.unitychan == true) {
					save.unitychan[3] = true;
				}
				//level 5 was scrapped
			} else if (level == 5) { //zelda
				for (int i = 0; i < save.scores4.Length; i++) {
					if (save.scores4[i]< totalscore) {
						for (int j = save.scores4.Length; i < 0; i--) {
							if (j == i) {
								save.scores4[i] = totalscore;
								save.names4[i] = playcont.name;
								break;
							} else {
								if (j == 0) {
									save.scores4[j] = totalscore;
									save.names4[j] = playcont.name;
									break;
								}
								save.scores4[j-1] = save.scores4[j];
								save.names4[j-1] = save.names4[j];
							}
						}
					}
				}
				
			} else {
				//error
			}
			//save the data
			string namae = playcont.name;
			unlocklevel(save, namae);
			//write to system
			string tojson = JsonUtility.ToJson(save);
			System.IO.File.WriteAllText(Application.persistentDataPath + "/savedata.json", tojson);
		
			
			
			
			//Unpause();
			//if no save file was found create one
		} else {
			//create it
			Save save = new Save();
			//in the new save file the new score is the highest score by default
			if (level == 1) {
				save.names0[0] = playcont.name;
				save.scores0[0] = totalscore;
				//check if the secret path was found
				if (playcont.unitychan == true) {
					save.unitychan[0] = true;
				}
			} else if (level == 2) {
				save.names1[0] = playcont.name;
				save.scores1[0] = totalscore;
				if (playcont.unitychan == true) {
					save.unitychan[1] = true;
				}
			} else if (level == 3) {
				save.names2[0] = playcont.name;
				save.scores2[0] = totalscore;
				if (playcont.unitychan == true) {
					save.unitychan[2] = true;
				}
			} else if (level == 4) {
				save.names3[0] = playcont.name;
				save.scores3[0] = totalscore;
				if (playcont.unitychan == true) {
					save.unitychan[4] = true;
				}
			} else if (level == 5) {
				save.names4[0] = playcont.name;
				save.scores4[0] = totalscore;
			} else {
				//errror
			}
			string namae = playcont.name;
			unlocklevel(save, namae);
			//write to system
			string tojson = JsonUtility.ToJson(save);
			System.IO.File.WriteAllText(Application.persistentDataPath + "/savedata.json", tojson);
			
			Debug.Log("Data created and saved.");
		}
	}
	
	//unlock a new level if the secret passage was found
	void unlocklevel(Save save, string namae) {
		int count = 0;
		for (int i = 0; i < 4; i++) {
			if (save.unitychan[i] == true) {
				count++;
			}
		}
		if (count == 4) {
			//save.unlocked[4] = true;
			//playsound from zelda
		}
		
		//new
		if (level == 1 && save.unitychan[0] == true) {
			save.unlocked[1] = true; //unlock mountains
		}
		if (level == 3 && save.unitychan[2] == true) {
			save.unlocked[3] = true; //unlock mountains hard
		}
		//scrapped
		if (string.Equals(namae, "Zelda")) {
			save.unlocked[4] = true;
			//playsound from zelda
		
		} 
		if (string.Equals(namae, "DebugLord")) {
			if (level == 3) {
				save.unlocked[5] = true;
			}
		}
	}
	
	//called once the player selectes a sticker
	public void stickerpicked(int sel) {
		GameObject player = GameObject.Find("Player");
		PlayerController playcont = player.GetComponent<PlayerController>();
		//loads the save file once more
		if (File.Exists(Application.persistentDataPath + "/gamesave.json")) {
			// 2
			string json = File.ReadAllText(Application.persistentDataPath + "/savedata.json"); // loading all the text out of the file into a string, assuming the text is all JSON

			Save save = JsonUtility.FromJson<Save>(json);
			//determines which sticker was unlocked and saves the data into the sticker array
			Debug.Log("Stickers Loaded");
			save.stickers[level-1, sel] = true;
			string tojson = JsonUtility.ToJson(save);
			System.IO.File.WriteAllText(Application.persistentDataPath + "/savedata.json", tojson);
			
		}
		//sets sticker picked to true so that the player can returnto the title screen
		playcont.stickerpicked = true;
		playcont.van.endrace();
	}
	
	
	
	/*private Save CreateSaveGameObject() {
		Save save = new Save();
		if (level == 0) {
			
		} else if (level == 1) {
			
		} else if (level == 2) {
			
		} else if (level == 3) {
			
		} else if (level == 4) {
			
		} else {
			return save;
		}

		return save;
	}
	
	
	
	public void SaveGame() {
		// 1
		Save save = CreateSaveGameObject();
		if (save == null) {
			Debug.Log("game couldn't be saved...");
			return;
		}
		
		// 2
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();

		// 3
		hits = 0;
		shots = 0;
		shotsText.text = "Shots: " + shots;
		hitsText.text = "Hits: " + hits;

		ClearRobots();
		ClearBullets();
		Debug.Log("Game Saved");
	}*/
	
	//enumerator function used to flash cross for 2 seconds
	IEnumerator flash (GameObject cav) {
		//flash cross for 2 seconds
		for (int i = 0; i < 4; i++) {
			//set image
			cav.transform.GetChild(1).gameObject.SetActive(true);
			yield return new WaitForSeconds(0.25f);
			//set image back to neutral
			cav.transform.GetChild(1).gameObject.SetActive(false);
			yield return new WaitForSeconds(0.25f);
		}
		
	}
	
	
	//used to give some downtime after movement has been registered so that the game doesnt register movements every frame
	IEnumerator waitsec (PlayerController playcont) {
		yield return new WaitForSeconds(1);
		timeflag = false;
		
	}
	
	IEnumerator waitsec2 (GameObject cav) {
		yield return new WaitForSeconds(1.5f);
		bonus = false;
		cav.transform.GetChild(2).gameObject.SetActive(false);
		cav.transform.GetChild(3).gameObject.SetActive(false);
		cav.transform.GetChild(4).gameObject.SetActive(false);
		cav.transform.GetChild(5).gameObject.SetActive(false);
		cav.transform.GetChild(6).gameObject.SetActive(false);
		
	}



	//load title scene
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
