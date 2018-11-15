using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMaid : MonoBehaviour {
	// 宣告成 static 讓全世界都能透過 WorldMaid.Summon 找到女僕
	public static WorldMaid Summon;

	// 記錄是否處於事件進行中的狀態
	public bool IsEvent = false;

	// Use this for initialization
	void Start () {
		// 在覺醒後馬上將自己登錄至召喚列表上
		Summon = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
