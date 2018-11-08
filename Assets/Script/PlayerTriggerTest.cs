using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果撞到的是可以觸發互動的 NPC 那麼應該有 NPCTriggerMaid 用來聯繫上 NPC
		NPCTriggerMaid maid = collision.GetComponent<NPCTriggerMaid>();
		if (maid != null)
		{
			// 如果有抓到，就把圖示設為顯示
			maid.Master.TriggerTestIcon.enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 如果撞到的是可以觸發互動的 NPC 那麼應該有 NPCTriggerMaid 用來聯繫上 NPC
		NPCTriggerMaid maid = collision.GetComponent<NPCTriggerMaid>();
		if (maid != null)
		{
			// 如果有抓到，就把圖示設為不顯示
			maid.Master.TriggerTestIcon.enabled = false;
		}
	}
}
