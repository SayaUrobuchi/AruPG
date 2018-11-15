using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 按固定順序決定對話
public class NextTalkNPC : TalkNPC {

	private int currentIdx;

	// Use this for initialization
	void Start () {
		currentIdx = 0;
	}

	// Update is called once per frame
	void Update()
	{
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
		OnNextTalkStart();
	}

	public void OnNextTalkStart()
	{
		Debug.Log("主角嘗試調查 " + gameObject.name + " 試圖與之對話！目前對話編號：" + currentIdx.ToString());
		IsEvent = true;
		// 禁止主角行動
		WorldMaid.Summon.IsEvent = true;
		// 找出目前對話
		string[] currentDialog = { Dialog[currentIdx] };
		DialogSystemMaid.Summon.SetDialogList(currentDialog);
		// 前進下一句的同時，把對話編號限制在不超出最後一句，最後一句在 Dialog.Length - 1
		currentIdx = Mathf.Min(currentIdx + 1, Dialog.Length - 1);
		DialogSystemMaid.Summon.DialogStart();
	}
}
