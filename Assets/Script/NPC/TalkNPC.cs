using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkNPC : NPC {

	public string[] Dialog = { "作業寫得如何呀？隊友還有呼吸嗎？" };

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 如果標注為此 NPC 被調查的事件執行中，則等待對話系統結束後解放
		if (IsEvent)
		{
			if (DialogSystemMaid.Summon.IsFinished())
			{
				IsEvent = false;
				// 稍微延遲一些時間，避免在同一次 Update 對話結束，同時又再次調查到並展開對話，導致無限反覆
				// 這會讓函數 OnEventEnd 在 0.1 秒之後被執行
				Invoke("OnEventEnd", .1f);
			}
		}
	}

	private void OnEventEnd()
	{
		WorldMaid.Summon.IsEvent = false;
	}

	public override void OnEventStart()
	{
		base.OnEventStart();
		OnTalkStart();
	}

	public void OnTalkStart()
	{
		Debug.Log("主角嘗試調查 " + gameObject.name + " 試圖與之對話！");
		IsEvent = true;
		// 禁止主角行動
		WorldMaid.Summon.IsEvent = true;
		// 指定對話之後啟動對話系統，等待結束
		DialogSystemMaid.Summon.SetDialogList(Dialog);
		DialogSystemMaid.Summon.DialogStart();
	}
}
