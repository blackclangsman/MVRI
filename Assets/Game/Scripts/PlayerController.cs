using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;


//oculus
using Oculus.Platform;
using Oculus.Platform.Models;

public class PlayerController : MonoBehaviour {
	
	
	
	//settings
	public bool vr = false; //is vr on?
	public int sensitivity = 3; //motion sensitivity
	public bool controller = true; //controller in use?
	public int startspeed = 30;	 //starting speed
	public bool debugging = true; //debugging
	public bool speedloss = false; //permanent or temporary speedloss
	public int camera2 = 1; //camera angle, first or third person
	public int vehicle = 1; //1 = sportscar
	public int duration = 5; //duartion
	public int volume = 5; //game volume
	
	//scores
	public int multiplier = 0; //(for score), bonus multiplier
	public int laps = -1; //amount of laps done
	public int score = 0;//player score
	public string name = ""; //originally players name entry, now only used for cheats and debugging
	public int placing = 1; //placing in race
	public bool unitychan = false; //have you found the stages secret?
	public int coins = 0;//coins collected in a stage
	public bool stickerpicked = false; //picked a sticker once the game has ended
	
	
	//properties
	public float moveSpeed; //currentspeed
    public float turnSpeed; //turning speed
	public float savedspeed;//saved speed, to keep track of what your previous speed was if it is altered temporarily
	public bool started = false; //checks if game has started yet
	public bool ended = false; //checks if game has ended, if so the player presses a button to load scene
	public string notice = null; //used for debugging in vr, see notice handler
	public bool readytostart = false; //is the gaze pointer in the motion field?
	public bool titlescreen = true; //is the game at the title screen?
	public bool tryit = false; //is this the try it level?
	bool noticerefresh = false; //used for debugging in vr, see notice handler script
	public bool turning = false; //is manual turning on or off?
	public bool pause = false; //is the game paused
	int pausetracker = 0; //amount of clicks needed to get back to the title screen
	bool buttoncheck = false; //has the puastracker been pressed?
	public bool pointerincircle = false; //is the gaze pointer in the circle again after movement has been registered
	
	
	
	
	//powerups
	public bool directioncheck = false; //used for direction checking at the goalposts, not used in the final build
	public bool wateraft = false; //has the player obtained the waterraft?
	public bool tempspeed = false;//used for temporary speed increases, not used in the final game
	public bool invincible = false; //not used in the final game
	public int hits = 0; //extra hits the player has
	public bool aibuster = false; //if true destroy ai when driving into them
	bool perminvincible = false; //used for a cheat code
	
	public Rigidbody rb; //rigidbofy component
	float fallZone = -10f; //should the player fall off the stage they will be teleported back
	public Transform playerSpawnPoint = null;
	Quaternion rotationreset; //unused
	
	//for placing
	public AIscript aicont1; //the first AI
	public AIscript aicont2; //the second AI
	public Transform[] checkpoint1; //waypoint arrays to keep track of whether the player has reached the waypoint
	public Transform[] checkpoint2; //the AI is moving towards, used to calculate distance
	public int current; //current waypoint for AI 1
	public int current2; //current waypoint for AI 2
	public bool finalwaypoint1 = false; //used to alleviate a small bug, there is probably a more clever solution though
	public bool finalwaypoint2 = false;
	
	//sound
	public soundmanager son; //sound manager
	//public enginesounds enson;
	public vamanager van; //voice acting manager
	bool soundchange = false; //used as a time flag for an enumerator function
	public PhysicMaterial Wall; //not used
    public PhysicMaterial bluegate; //not used
	
	
	//vr objects, used to initialize controller and headset and such
	public Vector2 joystick;
	public GameObject centerEye;
	public GameObject PObject;
	public OVRInput.Button trigger = OVRInput.Button.PrimaryIndexTrigger;
	public OVRInput.Controller control = OVRInput.Controller.All;
	
	
	
	
	

	// Use this for initialization
	void Start () {
		
		/*GameObject setlist = GameObject.Find("SetingsList");
		SettingsList settlist = setlist.GetComponent<SettingsList>();
		sensitivity = settlist.sensitivity;
		vr = settlist.vr;
		controller = settlist.controller;
		startspeed = settlist.speed;
		currentspeed = startspeed;*/
		
		//initialize VR support
		if (OVRManager.tiledMultiResSupported) {
			//vr = true;
		}
		vr = true;
		//vr = false;
		debugging = false;
		//debugging = true;
		GameObject setlist = GameObject.Find("SetingsList");
		//receive settings from settings list, if settings list is not found use default values
		try {
			SettingsList settlist = setlist.GetComponent<SettingsList>();
			//GameObject player = GameObject.Find("Player");
			//PlayerController playcont = player.GetComponent<PlayerController>();
			//get settings list from menu
			if (settlist != null) {
				vehicle = settlist.vehicle;
				camera2 = settlist.cmera;
				int sped = settlist.speed;
				int gamesped = sped*10;
				/*int gamesped = 20;
				for (int i = 0; i < sped+1; i++) {
					gamesped += 10;
				}*/
				startspeed = gamesped;
				savedspeed = (float)gamesped;
				moveSpeed = (float)gamesped;
				vr = settlist.vr;
				sensitivity = settlist.sensitivity;
				duration = settlist.duration;
				
				controller = settlist.controller;
				speedloss = settlist.speedloss;
				name = settlist.name;
				hits = settlist.hits;
				tryit = settlist.tryit;
				
				
				
			}
		} catch (Exception e) {
			startspeed = 30;
			Debug.Log("Settings list not found for player");
		}
		
		
		
		
		
		//set the game up based on the settingslist
		rb = GetComponent<Rigidbody> ();
		float val2 = (float)startspeed;
		
		
		moveSpeed = val2;
		turnSpeed = 50.0f;
		savedspeed = val2;
		
		if (vr == true) {
			controller = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);
			joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		} else {
			//find pointcloud controller
			//controller = //code to initialize pointcloud controller
		}
		
		if (controller == false) {
			Debug.Log("controller not found");
		}
		
		//initialize the AI
		if (aicont1 != null && aicont2 != null) {
			checkpoint1 = aicont1.target;
			checkpoint2 = aicont2.target;
			Debug.Log("checkpoints set");
		} else {
		
			try {
				GameObject ai1 = GameObject.Find("AI1");
				aicont1 = ai1.GetComponent<AIscript>();
				GameObject ai2 = GameObject.Find("AI2");
				aicont2 = ai2.GetComponent<AIscript>();
				checkpoint1 = aicont1.target;
				checkpoint2 = aicont2.target;
				Debug.Log("checkpoints set");
			} catch (Exception e) {
				Debug.Log("no ais");
			}
		}
		
		//initialize the sound managers
		son = transform.GetChild(7).gameObject.GetComponent<soundmanager>();
		//enson = transform.GetChild(7).GetChild(0).gameObject.GetComponent<enginesounds>();
		van = transform.GetChild(7).GetChild(1).gameObject.GetComponent<vamanager>();
		
		
		//set camera, dont invoke on title screen
		//titlescreen = false;
		//camera2 = 3;
		
		//set the vehicle and such
		if (titlescreen != true) {
			setstage();
		}

		
	}
	
	public void setstage() {
		setCamera();
		//check name, for cheats
		checkname();
		setvehicle();
		van.startrace();
	}
	
	
	void setvehicle() {
		//set the selected vehicle as active and the others as inactive
		Debug.Log("vehicle is " + vehicle);
		if (vehicle == 1) {
			transform.GetChild(11).gameObject.SetActive(false);
			transform.GetChild(10).gameObject.SetActive(false);
			transform.GetChild(8).gameObject.SetActive(true);
		} else if (vehicle == 2) { //post car
			transform.GetChild(11).gameObject.SetActive(false);
			transform.GetChild(10).gameObject.SetActive(true);
			transform.GetChild(8).gameObject.SetActive(false);
		} else if (vehicle == 3) { //delivery truck
			transform.GetChild(11).gameObject.SetActive(true);
			transform.GetChild(10).gameObject.SetActive(false);
			transform.GetChild(8).gameObject.SetActive(false);
		} else if (vehicle == 4) { //police car
			transform.GetChild(11).gameObject.SetActive(false);
			transform.GetChild(10).gameObject.SetActive(false);
			transform.GetChild(8).gameObject.SetActive(false);
			transform.GetChild(20).gameObject.SetActive(true);
		} else if (vehicle == 5) { //pink car
			transform.GetChild(11).gameObject.SetActive(false);
			transform.GetChild(10).gameObject.SetActive(false);
			transform.GetChild(8).gameObject.SetActive(false);
			transform.GetChild(19).gameObject.SetActive(true);
		} else { //default to sports car
			transform.GetChild(11).gameObject.SetActive(false);
			transform.GetChild(10).gameObject.SetActive(false);
			transform.GetChild(8).gameObject.SetActive(true);
		}
	}
	
	public void setCamera() {
		
		/*GameObject cam1 = GameObject.Find("FPCamera");
		Camera cam1mera = cam1.GetComponentInChildren<Camera>();
		GameObject cam2 = GameObject.Find("OverheadCamera");
		Camera cam2mera = cam2.GetComponentInChildren<Camera>();
		GameObject cam3 = GameObject.Find("TPCamera");
		Camera cam3mera = cam3.GetComponentInChildren<Camera>();*/
		Debug.Log("cam: " + camera2);
		int truecam = camera2;
		//truecam = 1;
		//transform positionreset;
		camera2 = 3;
		//set the camera angle properly, originally it was made with multiple cameras but was changed to simply have the same camera change position
		try {
			//first person camera
		if (camera2 == 1) {
			if (vr == true) {
				transform.GetChild(5).gameObject.SetActive(true);
				transform.GetChild(13).gameObject.SetActive(true);
				transform.GetChild(15).gameObject.SetActive(true);
				//set tp to false
				transform.GetChild(3).gameObject.SetActive(false);
				transform.GetChild(12).gameObject.SetActive(false);
				transform.GetChild(14).gameObject.SetActive(false);
				//set gaze pointer
				transform.GetChild(5).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);
				Debug.Log("setcam");
				//manager.Cursor = transform.GetChild(5).GetChild(0).GetChild(1).GetChild(2).gameObject;
			} else {
				transform.GetChild(2).gameObject.SetActive(true);
			}
			//cam2.SetActive(false);
			//cam3.SetActive(false);
			
			//overhead camera, obscurd too much, not used
		} else if (camera2 == 2) {
			if (vr == true) {
				transform.GetChild(4).gameObject.SetActive(true);
				transform.GetChild(14).gameObject.SetActive(true);
			} else {
				transform.GetChild(1).gameObject.SetActive(true);
			}
			
			/*cam1mera.enabled = false;
			cam2mera.enabled = true;
			cam3mera.enabled = false;
			//cam1.SetActive(false);
			//cam3.SetActive(false);*/
		
		//third person camera, default
		} else {
			if (vr == true) {
				transform.GetChild(3).gameObject.SetActive(true);
				transform.GetChild(12).gameObject.SetActive(true);
				transform.GetChild(14).gameObject.SetActive(true);
				//set fp to false
				transform.GetChild(5).gameObject.SetActive(false);
				transform.GetChild(13).gameObject.SetActive(false);
				transform.GetChild(15).gameObject.SetActive(false);
				//set gaze pointer
				transform.GetChild(3).GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);
			} else {
				transform.GetChild(0).gameObject.SetActive(true);
			}
			camera2 = 3;
			/*
			cam1mera.enabled = false;
			cam2mera.enabled = false;
			cam3mera.enabled = true;
			//cam1.SetActive(false);
			//cam2.SetActive(false);*/
		}
		if (truecam == 1) {
			transform.GetChild(3).position = transform.GetChild(5).position;
			transform.GetChild(12).position = transform.GetChild(13).position;
			transform.GetChild(12).GetChild(0).position = transform.GetChild(13).GetChild(0).position;
		}
		//positionreset = transform.GetChild(3).position;
		rotationreset = transform.GetChild(3).rotation;
		} catch (Exception e) {
			
		}
		
	}

	
	//respawns player if they drive off the stage
	void respawn() {
		if(this.transform.position.y < fallZone) {
			this.transform.position = playerSpawnPoint.position;
		}
	}
	
	//function called when driving into a wall
	public void wall() {
		rb.velocity = Vector3.zero;
		//son.wall();
		//speed is lowered for some time
		if (speedloss == false) {
			moveSpeed -= 5;
			startspeed -=5;
			float speedscore = (100/moveSpeed)*duration;
			score -= (int)speedscore;
			//notice =  "Ramte en mur. Hastigheden blev sat ned";
			notice =  "S: -5";
			StartCoroutine(walltime());
		//speed is loweredpermanently, not used in final game
		} else {
			notice = "S: -5";
			moveSpeed -= 5;
			startspeed -=5;
			score -= 20;
		}
	}
	
	//gives visual feedback when ibtaining waterraft, called when the raft is obtained
	public void watterraftobtain() {
		int select = 0;
		StartCoroutine(uiitem(select));
	}
	
	//gives visual feedbackwhen finding the secret path, called when the path is found
	public void unitychanfound() {
		int select = 1;
		StartCoroutine(uiitem(select));
	}
	
	//gives visual feedback when obtaining a speedboost, called when a boost is obtained
	public void speedboostobtain() {
		int select = 2;
		StartCoroutine(uiitem(select));
	}
	
	//gives visual feedback when turning right, called when turning right
	public void signalright() {
		int select = 3;
		StartCoroutine(uiitem(select));
	}
	
	//gives visual feedback when turning left, called when turning left
	public void signalleft() {
		int select = 4;
		StartCoroutine(uiitem(select));
	}
	
	//communicates notices in vr, mostly used as a debugging tool for when testing vr. for more information see the notice handler script
	void noticehandler() {
		if (notice != null) {
			//find approriate UI and send the notice to it
			if (vr == true) {
				try {
				//check for the appropriate camera
				if (camera2 == 1) {
					GameObject noticeUI = this.gameObject.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(1).GetChild(1).gameObject;
					noticeUI.GetComponent<noticehandler>().handlenotice(notice);
					
				}
				if (camera2 == 2) {
					GameObject noticeUI = this.gameObject.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(1).GetChild(1).gameObject;
					noticeUI.GetComponent<noticehandler>().handlenotice(notice);
					
				}
				if (camera2 == 3) {
					GameObject noticeUI = this.gameObject.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(1).GetChild(1).gameObject;
					noticeUI.GetComponent<noticehandler>().handlenotice(notice);
					
				}
				} catch (Exception e) {
					
				}
			} else {
				if (camera2 == 1) {
					GameObject noticeUI = this.gameObject.transform.GetChild(2).GetChild(0).GetChild(1).gameObject;
					noticeUI.GetComponent<noticehandler>().handlenotice(notice);
					
				} else if (camera2 == 2) {
					GameObject noticeUI = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;
					noticeUI.GetComponent<noticehandler>().handlenotice(notice);
					
				} else {
					Debug.Log("made here");
					GameObject noticeUI = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
					noticeUI.GetComponent<noticehandler>().handlenotice(notice);
					
				}
			}
			notice = null;
		}
	}
	
	
	
	//updates
	
	// Update is called once per frame
	void Update () {
		OVRInput.Update(); //receive input from controller
		respawn(); //only called if fallen off stage
		//notice = "camera: " + camera2;
		noticehandler();
		updatesound();//update the engine sound if needed
		if (titlescreen != true) {//game cannot be paused in the title screen
			pausegame(); //pause if pausebutton is hit
		}
		
			
	}
	

	
	//used for testing 
	void vrinputtest() {
		//init
		OVRInput.Controller activeController = OVRInput.GetActiveController();
		
		//battery
		byte battery = OVRInput.GetControllerBatteryPercentRemaining();
		
		//get input from touchpad and index finger
		Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
		float indexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		string not = "x = " + primaryTouchpad.x+ ", y = " + primaryTouchpad.y + " trigger: " + indexTrigger;
		notice = not;
		Debug.Log(not);
		Debug.Log(vr);
		noticehandler();
	}
	
	
	void FixedUpdate() {
		OVRInput.FixedUpdate();
		//vrinputtest();
		//get input
		OVRInput.Controller activeController = OVRInput.GetActiveController();
		if (noticerefresh != true) {
			noticerefresh = true;
			StartCoroutine(refreshnotice());
		}
		//battery
		byte battery = OVRInput.GetControllerBatteryPercentRemaining();
		//display battery power if low
		if (titlescreen != true || tryit == true) {
			//the code below is the code that runs when the race has started
		if (started == true) {
			Debug.Log("started");
			Debug.Log("place: " + placing);
			//notice = "tarted";
			turn();
			brake();
			//checks placing to feed info to UI
			if (aicont1 != null && aicont2 != null) {
				checkplacing();
			}
			
			
			savedspeed = startspeed;
			//used for temporary speedboosts, not used in final game
			if (tempspeed == true) {
				tempspeed = false;
				moveSpeed += 10;
				StartCoroutine(speedTime());
			}
		
			//invincibility, not used in final game
			if (invincible == true && perminvincible != true) {
				StartCoroutine(invincibletime());
			} else if (perminvincible == true && invincible == true) {
				
			}
			
			//if debug mode is on go to debugging controls
			if (debugging == true) {
				debug();
			}
		//this code runs if the game has ended
		} else if (ended == true) {
			//wait for player to press trigger and then load title screen scene
			//notice = "Ræs slut";
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			GameObject setlist = GameObject.Find("SetingsList");
			if (setlist != null) {
				SettingsList settlist = setlist.GetComponent<SettingsList>();
				settlist.level = 0;
			}
			//set stickers to active, then wait for the player to pick one (handled by game manager)
			//once one has been picked, go to the title screen after theplayer presses the trigger
			if (vr == true) {
				bool brake = OVRInput.Get(trigger);
				if (brake == true && stickerpicked == true) {
					//load title screen scene
					StartCoroutine(LoadYourAsyncScene("SampleScene"));
				}
			} else {
				if (Input.GetKeyDown("space") && stickerpicked == true) {
					//load title screen scene
					StartCoroutine(LoadYourAsyncScene("SampleScene"));
				}
					
				//pointcloud controller
				if (debugging == false && Input.GetKeyDown("space") && stickerpicked == true) {
					//load title screen scene
					StartCoroutine(LoadYourAsyncScene("SampleScene"));
				}
			}
		} else {
			//wait for player to press start and then start a co routine counting down
			if (vr == true) {
				bool brake = OVRInput.Get(trigger);
				if (brake == true) {
					if (readytostart == true) {
						startcountdown();
					}
				}
			} else {
				if (Input.GetKeyDown("space")) {
					startcountdown();
				}
					
				//pointcloud controller
				if (Input.GetKeyDown("space")) {
					startcountdown();
				}
				
				
				
				
				//input for fine tuning movement, temporary
			}
		}
		}
			
        
    }
	
	
	//start
	//countdown, uses an enumerator function to count the seconds down, if debug mode is on the countdown is skipped
	void startcountdown() {
		if (debugging == true) {
			GameObject go = GameObject.Find("GoAndFinish");
			GameObject gocanv = go.transform.GetChild(0).gameObject;
			started = true;
			son.countdowngo();
			van.stopva();
			gocanv.transform.GetChild(4).gameObject.SetActive(false);
			gocanv.transform.GetChild(5).gameObject.SetActive(false);
		} else {
			StartCoroutine(starttime());
		}
	}
	
	//pauses the game
	void pausegame() {
		OVRInput.Controller activeController = OVRInput.GetActiveController();
		bool inp = false;
		bool trig = false;
		//initialize the back and trigger buttons
		if (vr == true) {
			inp = OVRInput.Get(OVRInput.Button.Back);
			trig = OVRInput.Get(trigger);
		} else {
			inp = Input.GetKeyDown(KeyCode.P);
			trig = Input.GetKeyDown(KeyCode.B);
		}
		
		//pauses the game if the game isnt paused
		if (inp == true && pause == false) {
			Time.timeScale = 0;
			//18 setActive(true);
			transform.GetChild(18).GetChild(0).gameObject.SetActive(true);
			son.turnoff();
			pause = true;
		//unpauses the game if the back button is pressed and the game is paused
		} else if (inp == true && pause == true) {
			Time.timeScale = 1;
			//son.startup();
			//son.cardriving();
			//18 setActive(false);
			transform.GetChild(18).GetChild(0).gameObject.SetActive(false);
			pause = false;
			pausetracker = 0;
		}
		//press the trigger 10 times to return to the title screen
		if (trig == true && buttoncheck == false && pause == true) {
			pausetracker++;
			transform.GetChild(3).rotation = rotationreset;
			if (pausetracker > 10) {
				Time.timeScale = 1;
				StartCoroutine(LoadYourAsyncScene("SampleScene"));
			}
			buttoncheck = true;
		} else if (trig == false && buttoncheck == true) {
			buttoncheck = false;
		}
		
		
	}
	
	
	
	
	//movements
	//takes the players movements
	void turn() {
		float moveHorizontal = 0.0f;
		float moveVertical = 0.0f;
		if (debugging == false) {
			moveVertical = 3.0f;
		}
		//vr mode
		if (vr == true) {
			turnSpeed = 0.0f;
			float vrturnSpeed = turnSpeed;
			OVRInput.Controller activeController = OVRInput.GetActiveController();
			//Vector2 move = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
			//Vector2 move = joystick;
			Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
			float indexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
			string not = "x = " + primaryTouchpad.x+ ", y = " + primaryTouchpad.y + " trigger: " + indexTrigger;
			//move right
			if (primaryTouchpad.x > 0.5f) {
				if (turning == true) {
					transform.Rotate(Vector3.up, vrturnSpeed * Time.deltaTime);
				}
				//rb.AddRelativeTorque(Vector3.up * vrturnSpeed * Time.deltaTime);
				//moveHorizontal = 0.3f;
				moveHorizontal = 3.0f;
			//move left
			} else if (primaryTouchpad.x < -0.5f) {
				if (turning == true) {
					transform.Rotate(Vector3.up, -vrturnSpeed * Time.deltaTime);
				}
				//rb.AddRelativeTorque(Vector3.up * -vrturnSpeed * Time.deltaTime);
				//moveHorizontal = -0.3f;
				moveHorizontal = -3.0f;
			} else {
				moveHorizontal = 0.0f;
			}
			
			//move forward and backwards, only used in debug mode
			if (primaryTouchpad.y > 0.5f && debugging == true) {
				moveVertical = 3.0f;
			} else if (primaryTouchpad.y < -0.5f && debugging == true) {
				moveVertical = -5.0f;
			}
			//move the player forward automatically
			moveVertical = 2.0f;
			//Vector3 movement = new Vector3 (moveHorizontal*20, 0.0f, moveVertical*moveSpeed);
			Vector3 movement = new Vector3 (0.0f, 0.0f, moveVertical*moveSpeed);
			//Debug.Log(movement);
			
			//moving to the side
			if (moveHorizontal > 0) {
				rb.MovePosition(rb.position+transform.right*8*Time.deltaTime);
			} else if (moveHorizontal < 0) {
				rb.MovePosition(rb.position-transform.right*8*Time.deltaTime);
			}
  
			//keeps the players velocity below the defined move speed
			if (rb.velocity.magnitude < moveSpeed && pointerincircle == true) {
                rb.AddRelativeForce(movement);
			}
			
			
		//pointcloud controller
		} else {
			//pointcloud controller
		}
		
	}
	
	//allows
	void brake() {
		OVRInput.Controller activeController = OVRInput.GetActiveController();
		bool indexTrigger = OVRInput.Get(trigger);
		//bool brake = OVRInput.Get(trigger);
		
		//braking by holding the trigger, reduced movespeed to 0
		if (indexTrigger == true) {
			moveSpeed = 0;
		} else {
			moveSpeed = savedspeed;
			//prevents speed from going below 15 from crashing into objects
			if (moveSpeed < 15) {
				moveSpeed = 15;
				savedspeed = 15;
			}
		}
		
		//reversing, was scrapped as it was considered too complicated
		if (rb.velocity.magnitude < 1) {
			//reverse if speed is zero
			//Vector3 reverse = new Vector3 (0.0f, -1.0f, 0.0f);
			//Debug.Log(movement);
 
			//rb.AddRelativeForce(movement * moveSpeed, ForceMode.Acceleration);
			if (rb.velocity.magnitude < 30) {
                //rb.AddRelativeForce(reverse * savedspeed);
			}
		}
	}
	
	//determines placing of the player in the race
	void checkplacing() {
		bool ahead1 = true;
		bool ahead2 = true;
		Debug.Log("checking placing");
		Debug.Log("ai laps " + aicont1.laps);
		Debug.Log("player laps " + aicont1.laps);
		//first checks if the laps are equal for AI 1
		if (aicont1.laps == laps) {
			Debug.Log("laps clear");
			//if they are, checks if the target the AI is moving towards is the same as the one the player is
			if (aicont1.current == current) {
				Debug.Log("current clear");
				if (aicont1.target[aicont1.current] == checkpoint1[current]) {
					//check distances to waypoint and compare them to each other
					Debug.Log("target clear");
					float distai = Vector3.Distance(aicont1.posit, aicont1.target[aicont1.current].position);
					GameObject distcalc = this.gameObject.transform.GetChild(7).gameObject;
					float distplay = Vector3.Distance(distcalc.transform.position, checkpoint1[current].position);
					if (distai > distplay) {
						ahead1 = true;
					} else {
						ahead1 = false;
					}
				}
			} else if (aicont1.current > current && finalwaypoint1 == false) {
				ahead1 = false;
			} else {
				ahead1 = true;
			}
		} else {
			if (aicont1.laps > laps) {
				ahead1 = false;
			} else {
				ahead1 = true;
			}
		} 
		
		//the same procedure as for AI1 takes place for AI2 as well
		if (aicont2.laps == laps) {
			Debug.Log("laps clear");
			if (aicont2.current == current2) {
				Debug.Log("current clear");
				if (aicont2.target[aicont2.current] == checkpoint2[current2]) {
					//check distance
					Debug.Log("target clear");
					float distai = Vector3.Distance(aicont2.posit, aicont2.target[aicont2.current].position);
					GameObject distcalc = this.gameObject.transform.GetChild(7).gameObject;
					float distplay = Vector3.Distance(distcalc.transform.position, checkpoint2[current2].position);
					if (distai > distplay) {
						ahead2 = true;
					} else {
						ahead2 = false;
					}
				}
			} else if (aicont1.current > current2 && finalwaypoint2 == false) {
				ahead2 = false;
			} else {
				ahead2 = true;
			}
		} else {
			if (aicont2.laps > laps) {
				ahead2 = false;
			} else {
				ahead2 = true;
			}
		} 
		
		//finally, placing is determined
		int plac = 3;
		if (ahead1 == true) {
			plac--;
		}
		if (ahead2 == true) {
			plac--;
		}
		placing = plac;
	}
	
	
	//co routines & time functions
	
	//enumerator function that shows visual feedback icons for waterraft, secrets, etc for 2 seconds when theyre obtained
	IEnumerator uiitem(int select) {
		if (select == 0) { //waterraft
			transform.GetChild(18).GetChild(7).gameObject.SetActive(true);
		} else if (select == 1) { //unitychan
			transform.GetChild(18).GetChild(9).gameObject.SetActive(true);
		}  else if (select == 3) { //turning right
			transform.GetChild(18).GetChild(10).gameObject.SetActive(true);
		}  else if (select == 4) { //turning left
			transform.GetChild(18).GetChild(11).gameObject.SetActive(true);
		} else { //speedboost
			transform.GetChild(18).GetChild(8).gameObject.SetActive(true);
		}
		yield return new WaitForSeconds(2);
		transform.GetChild(18).GetChild(7).gameObject.SetActive(false);
		transform.GetChild(18).GetChild(8).gameObject.SetActive(false);
		transform.GetChild(18).GetChild(9).gameObject.SetActive(false);
		transform.GetChild(18).GetChild(10).gameObject.SetActive(false);
		transform.GetChild(18).GetChild(11).gameObject.SetActive(false);
	}
	
	//used for temporary speedups, not in final game
	IEnumerator speedTime () {
		yield return new WaitForSeconds(3);
        moveSpeed = savedspeed;
  
	}
	
	//notice refreshes, used by noticehandler for debugging in vr
	IEnumerator refreshnotice () {
		yield return new WaitForSeconds(2);
        notice = " ";
		noticerefresh = false;
  
	}
	
	//not used in final game
	IEnumerator invincibletime () {
		notice = "Uovervindelighed opnået";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 10";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 9";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 8";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 7";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 6";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 5";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 4";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 3";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 2";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig i 1";
		yield return new WaitForSeconds(1);
		notice = "Uovervindelig overstået";
        invincible = false;
  
	}
	
	//determines for how long the player is slowed by a crash with an obstacle
	IEnumerator walltime () {
		yield return new WaitForSeconds(10);
        moveSpeed += 5;
		startspeed += 5;
		savedspeed += 5;
  
	}
	
	//the countdown for the start of the race
	IEnumerator starttime () {
		GameObject go = GameObject.Find("GoAndFinish");
		GameObject gocanv = go.transform.GetChild(0).gameObject;
		//disable hint and press start
		gocanv.transform.GetChild(4).gameObject.SetActive(false);
		gocanv.transform.GetChild(5).gameObject.SetActive(false);
		//set 3
		gocanv.transform.GetChild(3).gameObject.SetActive(true);
		//van.three();
		son.countdown();
		yield return new WaitForSeconds(1);
		//set 2
		gocanv.transform.GetChild(3).gameObject.SetActive(false);
		gocanv.transform.GetChild(2).gameObject.SetActive(true);
		//van.two();
		son.countdown();
        yield return new WaitForSeconds(1);
		//set 1
		gocanv.transform.GetChild(2).gameObject.SetActive(false);
		gocanv.transform.GetChild(1).gameObject.SetActive(true);
		//van.one();
		son.countdown();
		yield return new WaitForSeconds(1);
		//set go
		gocanv.transform.GetChild(1).gameObject.SetActive(false);
		gocanv.transform.GetChild(0).gameObject.SetActive(true);
		started = true;
		//van.go();
		son.countdowngo();
		son.startup();
		yield return new WaitForSeconds(1);
		//set go to false
		gocanv.transform.GetChild(0).gameObject.SetActive(false);
	}
	
	
	//sound
	
	//updates the sound the engine emmits depending on its movement speed
	void updatesound() {
		soundchange = true;
		if (started == true) {
			if (rb.velocity.magnitude < 5.0f) {
				Debug.Log("aud");
				son.caridle();
			} else {
				if (pause != true) {
					son.cardriving();
				}
			}
		} else {
			if (readytostart == true) {
				son.caridle();
			}
		}
	}
	
	
	/*
	void OnCollisionEnter(Collision collision) {
		Debug.Log("wal2");
		if (collision.gameObject.GetComponent<BoxCollider>().material == Wall || collision.gameObject.GetComponent<BoxCollider>().material == bluegate) {
			son.wallcrash();
			Debug.Log("walltime");
		}
	}*/
	
	
	
	
	//unlocks
	
	
	//cheat codes and such, enable various aspects, such as starting the game with 30 hits or being able to crash the other cars
	void checkname() {
		if (string.Equals(name, "UUDDLRLRBAS")) {
			hits = 30;
			notice = "Starter md 30 liv";
		} else if (string.Equals(name, "ABACABB")) {
			//can take the ai out by driving into them
			aibuster = true;
		} else if (string.Equals(name, "Justin Bailey")) {
			notice = "Anti-vand modul tilsluttet";
			wateraft = true;
		} else if (string.Equals(name, "Zelda")) {
			//unlocks zelda level upon beating a stage
		} else if (string.Equals(name, "Apache")) {
			//makes you invincible
			perminvincible = true;
			invincible = true;
			notice = "Permanent uovervindelig";
		} else if (string.Equals(name, "Turn")) {
			//makes you invincible
			turning = true;
			notice = "Manuel Draejning Til";
		} else if (string.Equals(name, "DebugLord")) {
			debugging = true;
			notice = "Debug mode on";
			GameObject gamman = GameObject.Find("gamemaanager");
			gamemaanager gam = gamman.GetComponent<gamemaanager>();
			gam.debugging = true;
			//unlocks title screen level and aplha level upon beating a stage
		}
		
		//cheat that gives you god mode (all powerups), leeroy jenkins
	}
	
	
	
	
	
	//debugging function, mainly used to test on pc
	void debug() {
		//change camera
			
			if (Input.GetKeyDown(KeyCode.A)){
				GameObject setlist;
				GameObject cam1 = GameObject.Find("Main Camera");
				GameObject cam2 = GameObject.Find("Main Camera");
				Camera cam1mera = cam1.GetComponent<Camera>();
				Camera cam2mera = cam2.GetComponent<Camera>();
				setlist = GameObject.Find("SetingsList");
				if (setlist != null) {
					SettingsList settlist = setlist.GetComponent<SettingsList>();
					camera2 = settlist.cmera;
				}
				if (camera2 == 1) {
					//change to third person
					cam1 = GameObject.Find("FPCamera");
					cam1mera = cam1.GetComponentInChildren<Camera>();
					cam2 = GameObject.Find("TPCamera");
					cam2mera = cam2.GetComponentInChildren<Camera>();
					camera2 = 3;
				} else if (camera2 == 3) {
					//change to overhead
					cam1 = GameObject.Find("TPCamera");
					cam1mera = cam1.GetComponentInChildren<Camera>();
					cam2 = GameObject.Find("OverheadCamera");
					cam2mera = cam2.GetComponentInChildren<Camera>();
					camera2 = 2;
				} else if (camera2 == 2) {
					//change to first person
					cam1 = GameObject.Find("OverheadCamera");
					cam1mera = cam1.GetComponentInChildren<Camera>();
					cam2 = GameObject.Find("FPCamera");
					cam2mera = cam2.GetComponentInChildren<Camera>();
					camera2 = 1;
				} else {
					cam1 = GameObject.Find("Main Camera");
					cam1mera = cam1.GetComponentInChildren<Camera>();
					cam2 = GameObject.Find("FPCamera");
					cam2mera = cam2.GetComponentInChildren<Camera>();
					camera2 = 1;
				}
				
				//SettingsList settlist = setlist.GetComponent<SettingsList>();
				cam1mera.enabled = false;
				cam2mera.enabled = true;
				//Debug.Log(settlist.AI);
				
				Debug.Log("camera changed");
			
			
			
			}
			//turning
			//if(Input.GetKey(KeyCode.LeftArrow))
				//transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
			//if(Input.GetKey(KeyCode.RightArrow))
				//transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
			
			
			
			
			//moveforward
			
			//older
			/*if(Input.GetKey(KeyCode.UpArrow))
				//transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
				rb.MovePosition(-Vector3.forward*moveSpeed);
				
        
			if(Input.GetKey(KeyCode.DownArrow))
				transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);*/
			
			//old
					
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			//Debug.Log("vert: " + moveVertical);
 
			Vector3 movement = new Vector3 (moveHorizontal*2, 0.0f, moveVertical*2);
			//Debug.Log(movement);
 
			//rb.AddRelativeForce(movement * moveSpeed, ForceMode.Acceleration);
			if (rb.velocity.magnitude < moveSpeed) {
                rb.AddRelativeForce(movement * moveSpeed);
			}
			
			//if(Input.GetKey(KeyCode.UpArrow))
				//rb.AddRelativeForce(transform.forward * moveSpeed * Time.deltaTime);
				//rb.MovePosition(-Vector3.forward*moveSpeed);
				
        
			if(Input.GetKey(KeyCode.DownArrow))
				transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
			
			
			
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
