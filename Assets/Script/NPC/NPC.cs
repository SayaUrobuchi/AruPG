using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	// 存放測試是否進入 Trigger 範圍用的圖示
	public SpriteRenderer TriggerTestIcon;
	public bool IsEvent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void OnEventStart()
	{
		Debug.Log("NPC.OnEventStart();");
	}
}
