using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerTest : MonoBehaviour {

	private NPC targetNPC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 詢問調查範圍內 NPC 是否存在
	public bool IsTargetNPCExists()
	{
		// 如果 targetNPC 不為 null 表示存在；以 null 表示不存在
		return targetNPC != null;
	}

	// 獲取調查範圍內的 NPC 用
	public NPC GetTargetNPC()
	{
		return targetNPC;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果撞到的是可以觸發互動的 NPC 那麼應該有 NPCTriggerMaid 用來聯繫上 NPC
		NPCTriggerMaid maid = collision.GetComponent<NPCTriggerMaid>();
		if (maid != null)
		{
			targetNPC = maid.Master;
			if (targetNPC != null)
			{
				// 如果有抓到，就把圖示設為顯示
				targetNPC.TriggerTestIcon.enabled = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 如果撞到的是可以觸發互動的 NPC 那麼應該有 NPCTriggerMaid 用來聯繫上 NPC
		NPCTriggerMaid maid = collision.GetComponent<NPCTriggerMaid>();
		if (maid != null)
		{
			targetNPC = maid.Master;
			if (targetNPC != null)
			{
				// 如果有抓到，就把圖示設為不顯示
				targetNPC.TriggerTestIcon.enabled = false;
			}
		}
		// 一律清空 targetNPC 表示範圍內無 NPC 可調查
		targetNPC = null;
	}
}
