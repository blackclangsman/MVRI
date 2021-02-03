using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {

	//levels unlocked
	public bool[] unlocked = {true, false, true, false, false, false};
	public bool[] unitychan = {false, false, false, false}; //have you found the secret path in this level? (only first four levels count)
	public bool[,] stickers = {{false, false, false, false}, {false, false, false, false}, {false, false, false, false}, {false, false, false, false}};
	//{{complete stage, finish 1st, collect 100 coins, complete with 0 moves}}
	//0 = wild west level
	//1 = bridge level
	//2 = wild west hard (beat this as debuglord to unlock alpha level)
	//3 = mountains hard
	//4 = zelda level,scrapped
	//5 = alpha level
	
	//high scores, each array keeps track of the players score and if theyve used any cheats
	public string[] names0 = new string[10];
	public int[] scores0 = new int[10];
	public string[] names1 = new string[10];
	public int[] scores1 = new int[10];
	public string[] names2 = new string[10];
	public int[] scores2 = new int[10];
	public string[] names3 = new string[10];
	public int[] scores3 = new int[10];
	public string[] names4 = new string[10];
	public int[] scores4 = new int[10];
	//note:debuglevel isnt playable so it cant keep track of score
	public score[] scores5 = new score[10];
	
	
}
